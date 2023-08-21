using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class DeleteCredentialCommand : AsyncCommand<DeleteCredentialSettings>
    {
        public DeleteCredentialCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<DeleteCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeleteCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.DELETECARD];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a credential");
                return CommandLineInsufficientPermission;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving credential...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}"));
            if (!response.IsSuccess())
                DisplayError(response);

            var original = response.Deserialize<DataObjects.Cardholder.Credential>();
            DisplayTable(original, p => { p.BorderColor(Color.Red); },
                nameof(DataObjects.Cardholder.Credential.UniqueKey),
                nameof(DataObjects.Cardholder.Credential.CardNumber),
                nameof(DataObjects.Cardholder.Credential.FacilityCode),
                nameof(DataObjects.Cardholder.Credential.HotStamp),
                nameof(DataObjects.Cardholder.Credential.Mode),
                nameof(DataObjects.Cardholder.Credential.CardType));

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the above credential?", false))
                return CommandLineCancelled;

            response = await AnsiConsole.Status().StartAsync("Deleting credential...", _ => client.DeleteJSendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}"));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
                AnsiConsole.MarkupLine("[green]Credential deleted successfully[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
