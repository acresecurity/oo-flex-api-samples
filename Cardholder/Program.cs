using Cardholder.Cli.Cardholder;
using Cardholder.Cli.Credential;
using Cardholder.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console;
using Spectre.Console.Cli;

// Output standard console information and run the application
var info = new Grid();
info.AddColumn(new GridColumn().PadLeft(1).PadRight(1));
info.AddRow("Demonstrates how to add/edit/delete cardholder and credential information.");
info.AddRow("Sample includes supplying data to edit individual cardholders and credentials");
info.AddRow("as well as the full lifecycle.");

AnsiConsole.Write(
    new Panel(info)
        .Header("Cardholder/Credentials Sample"));

// Setup our services
using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Add our services that are shared between all of the samples
        services.AddDefaultServices(context.Configuration);

        services.AddTransient<IValidator<AddCardholderSettings>, AddCardholderValidation>();
        services.AddTransient<IValidator<EditCardholderSettings>, EditCardholderValidation>();
        services.AddTransient<IValidator<CredentialSettings>, CredentialValidation>();

        // Setup Spectre.Console.Cli which handles command line arguments
        services.AddSingleton(provider =>
        {
            var app = new CommandApp(new Common.Cli.Registrar(services, provider));
            app.Configure(config =>
            {
                config.AddBranch<CardholderSettings>("cardholder", p =>
                {
                    p.AddCommand<AddCardholderCommand>("add");
                    p.AddCommand<DeleteCardholderCommand>("delete");
                    p.AddCommand<EditCardholderCommand>("edit");
                    p.AddCommand<ViewCardholderCommand>("view");
                });
                /*
                config.AddBranch<CredentialSettings>("credential", p =>
                {

                });
                */
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