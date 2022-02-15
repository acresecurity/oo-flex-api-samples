using AlarmsProcessing.Cli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;

// Output standard console information and run the application
var info = new Grid();
info.AddColumn(new GridColumn().PadLeft(1).PadRight(1));
info.AddRow("Demonstrates how to display and process alarms.");
info.AddRow("Sample includes retrieve operator user rights to ensure ");

AnsiConsole.Write(
    new Panel(info)
        .Header("Alarm Handling"));

// Setup our services
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add our services that are shared between all of the samples
        services.AddDefaultServices(context.Configuration);

        // Setup Spectre.Console.Cli which handles command line arguments
        services.AddSingleton(provider =>
        {
            var app = new CommandApp(new Common.Cli.Registrar(services, provider));
            app.Configure(config =>
            {
                config.AddCommand<DisplayAlarmsCommand>("list").WithDescription("Display the current alarms.");
                config.AddCommand<AcknowledgeCommand>("acknowledge");
                config.AddCommand<ClearCommand>("clear");
                config.AddCommand<DismissCommand>("dismiss");
            });
            return app;
        });
    })
    .Build();

try
{
    var app = host.Services.GetService<CommandApp>();
    if (app == null)
        return 1;
    return await app.RunAsync(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return 1;
}