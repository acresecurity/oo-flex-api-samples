using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex
{
    internal class Program
    {
        static Program()
        {
            // Default JSON serializer necessary for the Flex API
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                FloatFormatHandling = FloatFormatHandling.DefaultValue,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                DateParseHandling = DateParseHandling.None,
            };
            settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy(true, true, true)));
            JsonConvert.DefaultSettings = () => settings;

            AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
            {
                if (eventArgs.ExceptionObject is Exception exception)
                    AnsiConsole.WriteException(exception.Demystify(), ExceptionFormats.ShortenEverything);
            };
        }

        static IHost BuildHost(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureLogging((context, logging) =>
            {
                logging
                    .AddFilter("IdentityModel", context.Configuration.GetValue("Logging:LogLevel:IdentityModel", LogLevel.Error))
                    .AddFilter("System", context.Configuration.GetValue("Logging:LogLevel:System", LogLevel.Error))
                    .AddFilter("Microsoft", context.Configuration.GetValue("Logging:LogLevel:Microsoft", LogLevel.Error))
                    .AddFilter("Microsoft.AspNetCore", context.Configuration.GetValue("Logging:LogLevel:Microsoft.AspNetCore", LogLevel.None))
                    .AddFilter("Microsoft.Extensions", context.Configuration.GetValue("Logging:LogLevel:Microsoft.Extensions", LogLevel.Error))
                    .AddFilter("Default", context.Configuration.GetValue("Logging:LogLevel:Default", LogLevel.Information))
                    .AddConfiguration(context.Configuration.GetSection("Logging"));
                logging.AddConsole();
            })
            .ConfigureServices((context, services) =>
            {
                // Add our services that are shared between all of the samples
                services.AddDefaultServices(context.Configuration, context.HostingEnvironment);

                // Setup Spectre.Console.Cli which handles command line arguments
                services.AddSingleton<ICommandApp>(provider =>
                {
                    // TODO https://learn.microsoft.com/en-us/dotnet/core/tools/enable-tab-autocomplete
                    var app = new CommandApp(new Cli.Registrar(services, provider));
                    app.Configure(config =>
                    {
                        config.SetApplicationName("flex");
                        config.CaseSensitivity(CaseSensitivity.None);

                        config.AddBuiltInCommands();
#if DEBUG
                        config.PropagateExceptions();
                        config.ValidateExamples();
#endif
                        config.SetExceptionHandler(ex =>
                        {
                            AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);
                            return Environment.ExitCode == CommandLineSuccess ? CommandLineUnhandledException : Environment.ExitCode;
                        });
                    });

                    return app;
                });
            })
            .Build();

        static async Task<int> Main(string[] args)
        {
            var encoding = Console.OutputEncoding;
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                using var host = BuildHost(args);
                var app = host.Services.GetService<ICommandApp>();
                if (app == null)
                    return Environment.ExitCode == CommandLineSuccess ? CommandLineGeneralError : Environment.ExitCode;

                return await app.RunAsync(args);
            }
            catch (CommandRuntimeException ex) when (ex.Message.StartsWith(ValidationErrorHeader))
            {
                return Environment.ExitCode == CommandLineSuccess ? CommandLineClientValidationError : Environment.ExitCode;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);
                return Environment.ExitCode == CommandLineSuccess ? CommandLineUnhandledException : Environment.ExitCode;
            }
            finally
            {
                Console.OutputEncoding = encoding;
            }
        }
    }
}
