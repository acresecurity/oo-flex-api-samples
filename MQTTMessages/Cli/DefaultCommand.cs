using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using Spectre.Console;
using Spectre.Console.Cli;

namespace MQTTMessages.Cli
{
    public abstract class DefaultCommand : Common.Cli.AsyncCommand<DefaultSettings>
    {
        private readonly Common.Configuration.MqttClientOptions _options;

        protected DefaultCommand(IOptions<Common.Configuration.Options> options, OidcClient oidcClient, IOptions<Common.Configuration.MqttClientOptions> mqttOptions)
            : base(options, oidcClient)
        {
            _options = mqttOptions.Value;
        }

        private IMqttClientOptions BuildOptions()
        {
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(_settings.ClientSecret));
            var secret = Convert.ToBase64String(challengeBytes);

            _options.CleanSession = false;
            _options.ClientName = "Mqtt.Sample";

            MqttClientTlsOptions tlsOptions = null;
            if (_options.TlsVersion != SslProtocols.None)
            {
                var parameters = new MqttClientOptionsBuilderTlsParameters
                {
                    UseTls = true
                };

                tlsOptions = new MqttClientTlsOptions
                {
                    UseTls = true,
                    SslProtocol = _options.TlsVersion,
                    AllowUntrustedCertificates = parameters.AllowUntrustedCertificates,
#if WINDOWS_UWP
                    Certificates = _tlsParameters.Certificates?.Select(c => c.ToArray()).ToList(),
#else
                    Certificates = parameters.Certificates?.ToList(),
#endif
#pragma warning disable CS0618 // Type or member is obsolete
                    CertificateValidationCallback = parameters.CertificateValidationCallback,
#pragma warning restore CS0618 // Type or member is obsolete
#if NETCOREAPP3_1 || NET5_0_OR_GREATER
                    ApplicationProtocols = parameters.ApplicationProtocols,
#endif
                    CertificateValidationHandler = parameters.CertificateValidationHandler,
                    IgnoreCertificateChainErrors = parameters.IgnoreCertificateChainErrors,
                    IgnoreCertificateRevocationErrors = parameters.IgnoreCertificateRevocationErrors
                };
            }

            if (_options.Transport == Transport.Tcp)
            {
                _options.ChannelOptions = new MqttClientTcpOptions
                {
                    Server = _options.Host,
                    Port = _options.Port,
                    TlsOptions = tlsOptions
                };
            }
            else
            {
                _options.ChannelOptions = new MqttClientWebSocketOptions
                {
                    Uri = _options.Host,
                    TlsOptions = tlsOptions
                };
            }

            _options.Credentials = new MqttClientCredentials
            {
                Username = _settings.ClientId,
                Password = Encoding.UTF8.GetBytes(secret)
            };

            return _options;
        }

        protected virtual Task<bool> Initialize(HttpClient client, Table table, Dictionary<int, string> eventDescriptions) => Task.FromResult(true);

        protected abstract void DefineTable(Table table);

        protected abstract bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions);

        protected abstract void Subscribe(MqttClientSubscribeOptionsBuilder builder);

        #region Overrides of AsyncCommand<DefaultSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, DefaultSettings settings)
        {
            var table = new Table().Expand().BorderColor(Color.Grey);
            DefineTable(table);

            AnsiConsole.MarkupLine("Press [yellow]CTRL+C[/] to exit");

            var client = await GetClient();
            if (client == null)
                return 1;

            Dictionary<int, string> eventDescriptions;

            //
            // Retrieve the event descriptions just so that we can provide it on the table when displayed.
            //
            var (pagedResponse, descriptions) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving event descriptions ...", p => client.FetchPaged<EventDescription[]>($"{_settings.Api}/api/v2/hardware/event/descriptions"));

            if (pagedResponse.IsSuccess())
                eventDescriptions = descriptions.ToDictionary(p => p.UniqueId, p => p.Description);
            else
            {
                DisplayError(pagedResponse);
                return 1;
            }

            if (!await Initialize(client, table, eventDescriptions))
                return 1;

            var factory = new MqttFactory();

            using var mqttClient = factory.CreateMqttClient();

            var options = BuildOptions();

            await AnsiConsole
                .Status()
                .StartAsync("Connecting to MQTT broker...", p => mqttClient.ConnectAsync(options, CancellationToken.None));

            var subscription = factory.CreateSubscribeOptionsBuilder();
            Subscribe(subscription);

            await AnsiConsole
                .Status()
                .Start("Subscribing to topics...", p => mqttClient.SubscribeAsync(subscription.Build(), CancellationToken.None));

            await AnsiConsole.Live(table)
                .AutoClear(false)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Top)
                .StartAsync(async p =>
                {
                    AnsiConsole.MarkupLine("Waiting for topics");

                    mqttClient.ApplicationMessageReceivedAsync += e =>
                    {
                        if (DisplayPayload(e.ApplicationMessage, table, eventDescriptions))
                            p.Refresh();

                        return Task.CompletedTask;
                    };

                    while (true)
                    {
                        await Task.Delay(250);
                    }
                });

            return 0;
        }

        #endregion
    }
}
