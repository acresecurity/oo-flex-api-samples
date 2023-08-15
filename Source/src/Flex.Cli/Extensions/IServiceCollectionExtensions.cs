using System.IO.Abstractions;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using EasyCaching.Disk;
using Flex.Configuration;
using Flex.Oidc;
using Flex.Services;
using Flex.Services.Abstractions;
using FluentValidation;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
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
        public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration, string[] args)
        {
            var config = configuration.GetSection("FlexApi");
            services.Configure<Flex.Configuration.Options>(config);

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
                    .AddRow("MQTT Transport", config["Mqtt:Transport"] ?? "<Unset>")
                    .AddRow("MQTT Host", config["Mqtt:Host"] ?? "<Unset>");

                // Sloppy margin
                AnsiConsole.Write(new Panel(new Panel(table).Header("Settings")).Border(BoxBorder.None).PadLeft(4));
            }
            
            // OIDC Options
            services.AddSingleton(p =>
            {
                var browser = new SystemBrowser();

                var settings = p.GetService<IOptions<Flex.Configuration.Options>>().Value;
                var options = new OidcClientOptions
                {
                    Authority = settings.Authority,
                    ClientId = settings.ClientId,
                    ClientSecret = settings.ClientSecret,
                    Scope = "offline_access openid profile email flex_api",
                    RedirectUri = $"http://127.0.0.1:{browser.Port}",
                    Browser = browser,
                    Policy = new Policy { Discovery = new DiscoveryPolicy { AllowHttpOnLoopback = true } }
                };

                return new OidcClient(options);
            });

            // MQTT Options
            services.AddTransient(p =>
            {
                var options = p.GetRequiredService<Flex.Configuration.Options>();
                
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
                    builder.WithTls(o =>
                    {
                        o.SslProtocol = options.Mqtt.TlsVersion;
                    });
                }

                return builder.Build();
            });

            services.AddTransient<IFlexHttpClientFactory, FlexHttpClientFactory>();
            services.AddTransient<ICacheStore, CacheStore>();

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
                }, ComputeHash(config["Authority"] ?? "default"));

                p.WithJson();
            });

            services.AddHttpClient();

            return services;
        }

        private static string ComputeHash(string value)
        {
            var data = MD5.HashData(Encoding.UTF8.GetBytes(value));
            var sb = new StringBuilder(64);
            foreach (var item in data)
                sb.Append(item.ToString("x2"));
            return sb.ToString();
        }
    }
}