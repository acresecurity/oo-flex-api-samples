using System.Net;
using DataEntry.Cli.Cardholder.Settings;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder
{
    internal class DeleteCardholderCommand : DefaultCommand<DeleteCardholderSettings>
    {
        public DeleteCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DeleteCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeleteCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.DELETEPERSONNEL];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a cardholder");
                return 0;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Cardholder>();
            DisplayTable(original, p => { p.BorderColor(Color.Red); },
                nameof(Common.DataObjects.Cardholder.UniqueKey),
                nameof(Common.DataObjects.Cardholder.FirstName),
                nameof(Common.DataObjects.Cardholder.LastName),
                nameof(Common.DataObjects.Cardholder.Title),
                nameof(Common.DataObjects.Cardholder.Department));

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the above cardholder?", false))
                return 1;

            response = await AnsiConsole.Status().StartAsync("Deleting cardholder...", _ => client.DeleteJSendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine("[green]Cardholder deleted successfully[/]");
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}