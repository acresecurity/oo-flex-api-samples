using System.Net;
using Flex.Cli.DataEntry.AccessLevels.Settings;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.AccessLevels
{
    internal class GetAccessLevelGroupsCommand : AsyncCommand<AccessLevelSettings>
    {
        public GetAccessLevelGroupsCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AccessLevelSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view access levels");
                return CommandLineInsufficientPermission;
            }

            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving access levels ...", _ =>
                    client.GetJsendAsync("api/v2/hardware/accesslevel/groups"));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var accessLevels = response.Deserialize<AccessLevelGroup[]>();
                DisplayTable(accessLevels,
                    nameof(AccessLevelGroup.UniqueKey),
                    nameof(AccessLevelGroup.Name),
                    nameof(AccessLevelGroup.CanAssign),
                    nameof(AccessLevelGroup.GroupType));
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
