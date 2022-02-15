using System.Net;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Credential
{
    internal class ViewCredentialCommand : DefaultCommand<ViewCredentialSettings>
    {
        public ViewCredentialCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<ViewCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view credentials");
                return 0;
            }

            var response = await client.GetJsendAsync($"{_settings.Api}/api/v2/credential/{settings.UniqueKey}");
            if (response.IsSuccess())
            {
                var cardholder = response.Deserialize<Common.DataObjects.Credential>();
                DisplayObject(cardholder);

                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
