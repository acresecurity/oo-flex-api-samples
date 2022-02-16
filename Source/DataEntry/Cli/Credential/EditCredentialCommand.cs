using System.Net;
using DataEntry.Cli.Credential.Settings;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential
{
    internal class EditCredentialCommand : DefaultCommand<EditCredentialSettings>
    {
        public EditCredentialCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<EditCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, EditCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            // Same thing with Cardholders, there really isn't a permission for editing a cardholder. Rights are assigned to the various properties,
            // but for now we are going to assume that if you can't view the cardholders then it would be safe to say you can't edit them either.
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to edit credentials");
                return 0;
            }

            // Retrieving the original credential just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving credential...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Credential>();

            response = await AnsiConsole.Status().StartAsync("Saving cardholder...", _ => client.PutJSendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}", settings));
            if (response.IsSuccess())
            {
                var updated = response.Deserialize<Common.DataObjects.Credential>();
                AnsiConsole.MarkupLine("[green]Credential edited successfully[/]");
                CompareAndDisplay(settings, original, updated, p => p.BorderColor(Color.Green), new [] { "UniqueKey" });
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
