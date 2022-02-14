using HardwareTree.Cli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;

var info = new Grid();
info.AddColumn();
info.AddRow("Retrieves hardware information in a tree format");
info.AddRow("Sample includes filtering hardware (based on hardware type) and flattening the tree.");
info.AddRow("Examples:");
info.AddRow("  --filter door");
info.AddRow("  --flatten true");

AnsiConsole.Write(new Panel(info).Header("Hardware Tree"));

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
            app.SetDefaultCommand<TreeCommand>();
            app.Configure(config =>
            {
                config.AddCommand<TreeCommand>("tree")
                    .WithDescription("Display the hardware tree.")
                    .WithExample(new [] { "--filter", "door" })
                    .WithExample(new [] { "--flatten true --filter", "door" });
            });
            return app;
        });
    })
    .Build();

try
{
    var app = host.Services.GetService<CommandApp>();
    return await app.RunAsync(args);
}
catch (Exception ex)
{
    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
    return 1;
}