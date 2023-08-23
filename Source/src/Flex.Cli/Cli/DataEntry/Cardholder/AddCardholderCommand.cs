using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class AddCardholderCommand : DefaultCommand<AddCardholderSettings>
    {
        public AddCardholderCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AddCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AddCardholderSettings commandSettings, HttpClient client, UserInfo userInfo, DataObjects.Settings.Settings settings)
        {
            var right = userInfo[UserRights.ADDPERSONNEL];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add a cardholder");
                return CommandLineInsufficientPermission;
            }

            var validation = commandSettings.Validate();
            if (!validation.Successful)
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", validation.Message);
                return CommandLineClientValidationError;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding cardholder...", _ => client.PostJSendAsync("api/v2/cardholder", settings));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (commandSettings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var added = response.Deserialize<DataObjects.Cardholder.Cardholder>();
                AnsiConsole.MarkupLine($"[green]Cardholder added successfully[/] UniqueKey: [[{added.UniqueKey}]]");
                CompareAndDisplay(commandSettings, new DataObjects.Cardholder.Cardholder(), added, p => p.BorderColor(Color.Green));
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
