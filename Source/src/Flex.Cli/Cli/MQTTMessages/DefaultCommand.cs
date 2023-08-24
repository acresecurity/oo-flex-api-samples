using Flex.Cli.MQTTMessages.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Packets;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.MQTTMessages
{
    internal abstract class DefaultCommand : AsyncCommand<DefaultSettings>
    {
        private readonly IManagedMqttClient _mqttClient;
        private readonly MqttClientOptions _clientOptions;

        protected DefaultCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory, IManagedMqttClient mqttClient, MqttClientOptions clientOptions)
            : base(options, cache, factory)
        {
            _mqttClient = mqttClient;
            _clientOptions = clientOptions;
        }

        protected virtual Task<bool> Initialize(HttpClient client, Table table, Dictionary<int, string> eventDescriptions) => Task.FromResult(true);

        protected abstract void DefineTable(Table table);

        protected abstract bool DisplayPayload(MqttApplicationMessage message, Table table, Dictionary<int, string> eventDescriptions);

        protected abstract void Subscribe(MqttTopicFilterBuilder builder);

        #region Overrides of AsyncCommand<DefaultSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DefaultSettings settings, HttpClient client, UserInfo userInfo)
        {
            var table = new Table().Expand().BorderColor(Color.Grey);
            DefineTable(table);

            AnsiConsole.MarkupLine("Press [yellow]CTRL+C[/] to exit");
            
            var eventDescriptions = await Cache.EventDescriptions();
            if (eventDescriptions == null)
                return CommandLineCacheError;

            if (!await Initialize(client, table, eventDescriptions))
                return CommandLineGeneralError;

            var options = new ManagedMqttClientOptionsBuilder()
                .WithClientOptions(_clientOptions)
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(1))
                .Build();
            
            var subscription = new MqttTopicFilterBuilder();
            Subscribe(subscription);
            await _mqttClient.SubscribeAsync(new List<MqttTopicFilter> { subscription.Build() });

            await AnsiConsole
                .Status()
                .StartAsync("Connecting to MQTT broker...", _ => _mqttClient.StartAsync(options));

            await AnsiConsole.Live(table)
                .AutoClear(false)
                .Overflow(VerticalOverflow.Ellipsis)
                .Cropping(VerticalOverflowCropping.Top)
                .StartAsync(async p =>
                {
                    AnsiConsole.MarkupLine("Waiting for topics");

                    _mqttClient.ApplicationMessageReceivedAsync += e =>
                    {
                        if (DisplayPayload(e.ApplicationMessage, table, eventDescriptions))
                            p.Refresh();

                        return Task.CompletedTask;
                    };

                    while (true)
                    {
                        await Task.Delay(250);
                    }
                    // ReSharper disable once FunctionNeverReturns
                });

            return CommandLineSuccess;
        }

        #endregion
    }
}
