using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class AssignAccessLevelsCommand : AsyncCommand<AccessLevelSettings>
    {
        public AssignAccessLevelsCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AssignAccessLevelSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.AssignAxsLvl];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to assign or change access levels");
                return CommandLineInsufficientPermission;
            }

            var response = await client.PutJSendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/accesslevels/groups", settings.AccessLevels);

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
                AnsiConsole.MarkupLine("[green]Access levels assigned[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
