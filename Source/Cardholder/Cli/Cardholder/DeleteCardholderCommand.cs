using System.Net;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    public class DeleteCardholderCommand : DefaultCommand<DeleteCardholderSettings>
    {
        public DeleteCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<AddCardholderSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, DeleteCardholderSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", p => client.GetJsendAsync($"{_settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Cardholder>();
            var table = new Table();
            table.BorderColor(Color.Red);
            table.AddColumns("[yellow]UniqueKey[/]", "[yellow]FirstName[/]", "[yellow]LastName[/]", "[yellow]Title[/]", "[yellow]Department[/]");
            table.AddRow(new [] { original.UniqueKey.ToString(), original.FirstName,  original.LastName, original.Title, original.Department }.Select(p => string.IsNullOrEmpty(p) ? string.Empty : p).ToArray());
            AnsiConsole.Write(table);
            if (!AnsiConsole.Confirm("Are you sure you wish to delete the above cardholder?", false))
                return 1;

            response = await AnsiConsole.Status().StartAsync("Deleting cardholder...", p => client.DeleteJSendAsync($"{_settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
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