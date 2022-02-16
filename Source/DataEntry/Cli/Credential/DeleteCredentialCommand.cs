using System.Net;
using DataEntry.Cli.Credential.Settings;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential
{
    internal class DeleteCredentialCommand : DefaultCommand<DeleteCredentialSettings>
    {
        public DeleteCredentialCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DeleteCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeleteCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.DELETECARD];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a credential");
                return 0;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving credential...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Credential>();
            DisplayTable(original, p => { p.BorderColor(Color.Red); },
                nameof(Common.DataObjects.Credential.UniqueKey),
                nameof(Common.DataObjects.Credential.CardNumber),
                nameof(Common.DataObjects.Credential.FacilityCode),
                nameof(Common.DataObjects.Credential.HotStamp),
                nameof(Common.DataObjects.Credential.Mode),
                nameof(Common.DataObjects.Credential.CardType));

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the above credential?", false))
                return 1;

            response = await AnsiConsole.Status().StartAsync("Deleting credential...", _ => client.DeleteJSendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}"));
            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine("[green]Credential deleted successfully[/]");
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
