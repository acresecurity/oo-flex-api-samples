using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
            {
                if (eventArgs.ExceptionObject is Exception exception)
                    AnsiConsole.WriteException(exception.Demystify(), ExceptionFormats.ShortenEverything);
            };

            using var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => 
                {
                    logging
                        .AddFilter("System", context.Configuration.GetValue("Logging:LogLevel:System", LogLevel.Error))
                        .AddFilter("Microsoft", context.Configuration.GetValue("Logging:LogLevel:Microsoft", LogLevel.Error))
                        .AddFilter("Microsoft.AspNetCore", context.Configuration.GetValue("Logging:LogLevel:Microsoft.AspNetCore", LogLevel.None))
                        .AddFilter("Microsoft.Extensions", context.Configuration.GetValue("Logging:LogLevel:Microsoft.Extensions", LogLevel.Error))
                        .AddFilter("Default", context.Configuration.GetValue("Logging:LogLevel:Default", LogLevel.Information))
                        .AddConfiguration(context.Configuration.GetSection("Logging"));
                })
                .ConfigureServices((context, services) =>
                {
                    // Add our services that are shared between all of the samples
                    services.AddDefaultServices(context.Configuration, args);

                    // Setup Spectre.Console.Cli which handles command line arguments
                    services.AddSingleton<ICommandApp>(provider =>
                    {
                        // TODO https://learn.microsoft.com/en-us/dotnet/core/tools/enable-tab-autocomplete
                        var app = new CommandApp(new Cli.Registrar(services, provider));
                        app.Configure(config =>
                        {
                            config.SetApplicationName("flex");
                            //config.CaseSensitivity(CaseSensitivity.None);
#if DEBUG
                            config.PropagateExceptions();
#endif
                            config.AddAlarmCommands();
                            config.AddAccessLevelCommands();
                            config.AddCardholderCommands();
                            config.AddCredentialCommands();
                            config.AddHardwareCommands();
                            config.AddMqttCommands();
                        });

                        return app;
                    });
                })
                .Build();

            try
            {
                var app = host.Services.GetService<ICommandApp>();
                if (app == null)
                    return Environment.ExitCode == CommandLineSuccess ? CommandLineGeneralError : Environment.ExitCode;

                return await app.RunAsync(args);
            }
            catch (CommandRuntimeException ex) when (ex.Message.StartsWith(ValidationErrorHeader))
            {
                AnsiConsole.MarkupLine(ex.Message);
                return Environment.ExitCode == CommandLineSuccess ? CommandLineClientValidationError : Environment.ExitCode;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);
                return Environment.ExitCode == CommandLineSuccess ? CommandLineUnhandledException : Environment.ExitCode;
            }
        }
    }
}