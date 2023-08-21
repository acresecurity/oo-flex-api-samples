using System.IO.Abstractions;
using System.Security.Authentication;
using EasyCaching.Disk;
using Flex.Cli.Setup.Models;
using Flex.Cli.Setup.Views;
using Flex.Configuration;
using Flex.Oidc;
using Flex.Services;
using Flex.Services.Abstractions;
using Flex.Utils;
using FluentValidation;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    internal static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string[] args)
        {
            services
                .AddOptions<Flex.Configuration.Options>()
                .BindConfiguration("FlexApi");

            var config = configuration.GetSection("FlexApi");

            var displayBanner = !args.Any(p => p.Equals("--no-banner", StringComparison.OrdinalIgnoreCase));
            if (displayBanner)
            {
                var table = new Table()
                    .Border(TableBorder.Minimal)
                    .BorderColor(Color.Blue)
                    .AddColumn("Name")
                    .AddColumn("Value")
                    .AddRow("Api", config["Api"] ?? "<Unset>")
                    .AddRow("Authority", config["Authority"] ?? "<Unset>")
                    .AddRow("ClientId", config["ClientId"] ?? "<Unset>")
                    .AddRow("ClientSecret", string.IsNullOrEmpty(config["ClientSecret"]) ? "<Unset>" : "[[REDACTED]]")
                    .AddRow("MQTT Transport", config["Mqtt:Transport"] ?? "<Unset>")
                    .AddRow("MQTT Host", config["Mqtt:Host"] ?? "<Unset>");

                // Sloppy margin
                AnsiConsole.Write(new Panel(new Panel(table).Header("Settings")).Border(BoxBorder.None).PadLeft(4));
            }
            
            // OIDC Options
            services.AddSingleton(p =>
            {
                var provider = p.GetService<IOptionsProvider>();
                var validate = provider.Validate();
                if (!validate.IsValid)
                    return null;

                var browser = new SystemBrowser();

                var settings = provider.Options;
                var options = new OidcClientOptions
                {
                    Authority = settings.Authority,
                    ClientId = settings.ClientId,
                    ClientSecret = settings.ClientSecret,
                    Scope = "offline_access openid profile email flex_api",
                    RedirectUri = $"http://127.0.0.1:{browser.Port}",
                    Browser = browser
                };

                if (environment.IsDevelopment())
                {
                    options.Policy = new Policy
                    {
                        Discovery = new DiscoveryPolicy
                        {
                            AllowHttpOnLoopback = true,
                            RequireHttps = false
                        }
                    };

                    options.BackchannelHandler = new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                }

                return new OidcClient(options);
            });

            // MQTT Options
            services.AddTransient(provider =>
            {
                var options = provider.GetRequiredService<IOptions<Flex.Configuration.Options>>().Value;

                var builder = new MqttClientOptionsBuilder()
                        .WithCleanSession(false)
                        .WithClientId($"{options.Mqtt.ClientName}:{Environment.MachineName}")
                        .WithProtocolVersion(options.Mqtt.ProtocolVersion);

                if (options.Mqtt.ExternalServer)
                {
                    if (!string.IsNullOrEmpty(options.Mqtt.Username) && !string.IsNullOrEmpty(options.Mqtt.Password))
                        builder.WithCredentials(options.Mqtt.Username, options.Mqtt.Password);
                }
                else
                    builder.WithCodeCredentials(options.ClientId, options.ClientSecret);

                if (options.Mqtt.Transport == Transport.Tcp)
                    builder.WithTcpServer(options.Mqtt.Host, options.Mqtt.Port);
                else
                    builder.WithWebSocketServer(options.Mqtt.Host);

                if (options.Mqtt.TlsVersion != SslProtocols.None)
                {
                    builder.WithTls(p =>
                    {
                        p.SslProtocol = options.Mqtt.TlsVersion;
                    });
                }

                return builder.Build();
            });

            services.AddTransient<TerminalScheduler>();

            services.AddTransient<IFlexHttpClientFactory, FlexHttpClientFactory>();
            services.AddTransient<ICacheStore, CacheStore>();
            services.AddTransient<IOptionsProvider, OptionsProvider>();

            services.AddSingleton(_ => new MqttFactory());

            services.AddSingleton<IManagedMqttClient>(p =>
            {
                var factory = p.GetRequiredService<MqttFactory>();
                return new ManagedMqttClient(factory.CreateMqttClient(), factory.DefaultLogger);
            });

            // System IO Abstractions
            services.AddTransient<IFileSystem, FileSystem>();

            // Fluent Validation
            services.AddValidatorsFromAssemblyContaining(typeof(IServiceCollectionExtensions), ServiceLifetime.Transient, includeInternalTypes: true);

            // Since this is a console app we can use this to store frequently requested data so that we aren't having to fetch it with every execution
            services.AddEasyCaching(p =>
            {
                p.UseDisk(options =>
                {
                    options.DBConfig = new DiskDbOptions
                    {
                        BasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "flex.cli", "cache")
                    };
                    options.SerializerName = "json";
                }, (config["Authority"] ?? "default").ComputeHash());

                p.WithJson();
            });

            services.AddHttpClient();

            services.AddTransient<SetupView>();
            services.AddSingleton<SetupModel>();

            return services;
        }
    }
}
