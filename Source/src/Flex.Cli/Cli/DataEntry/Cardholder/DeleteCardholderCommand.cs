using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class DeleteCardholderCommand : AsyncCommand<DeleteCardholderSettings>
    {
        public DeleteCardholderCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<DeleteCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeleteCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.DELETEPERSONNEL];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a cardholder");
                return CommandLineInsufficientPermission;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", _ => client.GetJsendAsync($"api/v2/cardholder/{settings.UniqueKey}"));
            if (!response.IsSuccess())
                return DisplayError(response);

            var original = response.Deserialize<DataObjects.Cardholder.Cardholder>();
            DisplayTable(original, p => { p.BorderColor(Color.Red); },
                nameof(DataObjects.Cardholder.Cardholder.UniqueKey),
                nameof(DataObjects.Cardholder.Cardholder.FirstName),
                nameof(DataObjects.Cardholder.Cardholder.LastName),
                nameof(DataObjects.Cardholder.Cardholder.Title),
                nameof(DataObjects.Cardholder.Cardholder.Department));

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the above cardholder?", false))
                return CommandLineCancelled;

            response = await AnsiConsole.Status().StartAsync("Deleting cardholder...", _ => client.DeleteJSendAsync($"api/v2/cardholder/{settings.UniqueKey}"));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
                AnsiConsole.MarkupLine("[green]Cardholder deleted successfully[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
