using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Credential
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
            var response = await AnsiConsole.Status().StartAsync("Retrieving credential...", p => client.GetJsendAsync($"{_settings.Api}/api/v2/credential/{settings.UniqueKey}"));
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

            response = await AnsiConsole.Status().StartAsync("Deleting credential...", p => client.DeleteJSendAsync($"{_settings.Api}/api/v2/credential/{settings.UniqueKey}"));
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
