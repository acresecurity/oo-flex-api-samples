using System.Net;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Credential
{
    internal class AddCredentialCommand : DefaultCommand<AddCredentialSettings>
    {
        public AddCredentialCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<AddCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AddCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ADDCARD];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add credentials");
                return 0;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding credential...", p => client.PostJSendAsync($"{_settings.Api}/api/v2/cardholder/{settings.CardholderKey}/credential", settings));
            if (response.IsSuccess())
            {
                var added = response.Deserialize<Common.DataObjects.Credential>();
                AnsiConsole.MarkupLine($"[green]Credential added successfully[/] UniqueKey: [[{added.UniqueKey}]]");

                CompareAndDisplay(settings, new Common.DataObjects.Credential(), added, p => p.BorderColor(Color.Green), new []{ nameof(AddCredentialSettings.CardholderKey) });
                return 0;
            }

            DisplayError(response);

            return 1;
        }

        #endregion
    }
}
