using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class ViewAssignedAccessLevelsCommand : AsyncCommand<ViewCredentialSettings>
    {
        public ViewAssignedAccessLevelsCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<ViewCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view access levels");
                return CommandLineInsufficientPermission;
            }

            var response = await client.GetJsendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}/accesslevel/groups");

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