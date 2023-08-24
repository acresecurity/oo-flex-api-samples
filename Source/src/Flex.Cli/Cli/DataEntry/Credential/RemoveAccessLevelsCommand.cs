using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class RemoveAccessLevelsCommand : AsyncCommand<AccessLevelSettings>
    {
        public RemoveAccessLevelsCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AccessLevelSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.AssignAxsLvl];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to assign or change access levels");
                return CommandLineInsufficientPermission;
            }

            var response = await client.DeleteJSendAsync($"api/v2/credential/{settings.UniqueKey}/accesslevel/groups", settings.AccessLevels);

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
                AnsiConsole.MarkupLine("[green]Access levels removed[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
