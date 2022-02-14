using Common.Oidc;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDefaultServices(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("FlexApi");
            services.Configure<Common.Configuration.Options>(config);

            var mqtt = configuration.GetSection("FlexApi:Mqtt");
            services.Configure<Common.Configuration.MqttClientOptions>(mqtt);

            var table = new Table()
                .Border(TableBorder.Minimal)
                .BorderColor(Color.Blue)
                .AddColumn("Name")
                .AddColumn("Value")
                .AddRow("Api", config["Api"])
                .AddRow("Authority", config["Authority"])
                .AddRow("ClientId", config["ClientId"])
                .AddRow("ClientSecret", config["ClientSecret"])
                .AddRow("MQTT Transport", mqtt["Transport"])
                .AddRow("MQTT Host", mqtt["Host"]);

            // Sloppy margin
            AnsiConsole.Write(new Panel(new Panel(table).Header("Settings")).Border(BoxBorder.None).PadLeft(4));

            services.AddSingleton(p =>
            {
                var browser = new SystemBrowser();

                var settings = p.GetService<IOptions<Common.Configuration.Options>>().Value;
                var options = new OidcClientOptions
                {
                    Authority = settings.Authority,
                    ClientId = settings.ClientId,
                    ClientSecret = settings.ClientSecret,
                    Scope = "offline_access openid profile email flex_api",
                    RedirectUri = $"http://127.0.0.1:{browser.Port}",
                    Browser = browser
                };

                return new OidcClient(options);
            });

            return services;
        }
    }
}
