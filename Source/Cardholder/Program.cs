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

        // Add Fluent Validation validators
        services.AddTransient<IValidator<AddCardholderSettings>, AddCardholderValidation>();
        services.AddTransient<IValidator<EditCardholderSettings>, EditCardholderValidation>();
        services.AddTransient<IValidator<AddCredentialSettings>, AddCredentialValidation>();
        services.AddTransient<IValidator<EditCredentialSettings>, EditCredentialValidation>();

        // Setup Spectre.Console.Cli which handles command line arguments
        services.AddSingleton(provider =>
        {
            var app = new CommandApp(new Common.Cli.Registrar(services, provider));
            app.Configure(config =>
            {
                config.AddBranch<CardholderSettings>("cardholder", p =>
                {
                    p.AddCommand<AddCardholderCommand>("add")
                        .WithExample(new[] { "cardholder", "add", "--FirstName", "Sheldon", "--LastName", "Copper" });

                    p.AddCommand<DeleteCardholderCommand>("delete")
                        .WithExample(new[] { "cardholder", "delete", Guid.NewGuid().ToString() });

                    p.AddCommand<EditCardholderCommand>("edit")
                        .WithExample(new[] { "cardholder", "edit", Guid.NewGuid().ToString(), "--FirstName", "Marco", "--LastName", "Polo" });

                    p.AddCommand<GetCardholdersCommand>("get")
                        .WithExample(new[] { "cardholder", "get", "--where", "\"LastName == 'Brown'\"", "--orderBy", "FirstName" })
                        .WithExample(new[] { "cardholder", "get", "--where", "\"LastName.Contains('Brown')\"", "--orderBy", "FirstName"  })
                        .WithExample(new[] { "cardholder", "get", "--where", "\"LastName.StartsWith('Brown')\"", "--orderBy", "FirstName"  })
                        .WithExample(new[] { "cardholder", "get", "--where", "\"LastName.EndsWith('Brown')\"", "--orderBy", "\"FirstName Descending\""  });

                    p.AddCommand<ViewCardholderCommand>("view")
                        .WithExample(new[] { "cardholder", "view", Guid.NewGuid().ToString() })
                        .WithExample(new[] { "cardholder", "view", Guid.NewGuid().ToString(), "--credentials" });
                });

                config.AddBranch<CredentialSettings>("credential", p =>
                {
                    p.AddCommand<AddCredentialCommand>("add")
                        .WithDescription("Add a credential to a specified cardholder")
                        .WithExample(new[] { "credential", "add", Guid.NewGuid().ToString(), "--CardNumber", "4571" });

                    p.AddCommand<DeleteCredentialCommand>("delete")
                        .WithExample(new[] { "credential", "delete", Guid.NewGuid().ToString() });

                    p.AddCommand<EditCredentialCommand>("edit")
                        .WithExample(new[] { "credential", "edit", Guid.NewGuid().ToString(), "--Active", "false", "--CardType", "3" });

                    p.AddCommand<ViewCredentialCommand>("view")
                        .WithExample(new[] { "credential", "view", Guid.NewGuid().ToString() });
                });
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