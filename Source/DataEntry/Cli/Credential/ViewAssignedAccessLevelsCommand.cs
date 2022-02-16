using System.Net;
using Common.Configuration;
using Common.DataObjects;
using DataEntry.Cli.Credential.Settings;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential
{
    internal class ViewAssignedAccessLevelsCommand : DefaultCommand<ViewCredentialSettings>
    {
        public ViewAssignedAccessLevelsCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<ViewCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view access levels");
                return 0;
            }

            var response = await client.GetJsendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}/accesslevel/groups");
            if (response.IsSuccess())
            {
                var accessLevels = response.Deserialize<AccessLevelGroup[]>();

                DisplayTable(accessLevels,
                    nameof(AccessLevelGroup.UniqueKey),
                    nameof(AccessLevelGroup.Name),
                    nameof(AccessLevelGroup.CanAssign),
                    nameof(AccessLevelGroup.GroupType));

                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
