using System.Net;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.AccessLevels
{
    internal class GetAccessLevelGroupsCommand : DefaultCommand<AccessLevelSettings>
    {
        public GetAccessLevelGroupsCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<AccessLevelSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view cardholders");
                return 0;
            }

            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving access levels ...", _ =>
                    client.GetJsendAsync($"{Settings.Api}/api/v2/hardware/accesslevel/groups"));

            if (response.IsSuccess())
            {
                var accessLevels = response.Deserialize<AccessLevelGroup[]>();

                DisplayTable(accessLevels,
                    nameof(AccessLevelGroup.UniqueKey),
                    nameof(AccessLevelGroup.Description),
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
