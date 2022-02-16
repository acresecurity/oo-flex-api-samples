using System.Net;
using Common.Configuration;
using Common.DataObjects;
using DataEntry.Cli.Credential.Settings;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential
{
    internal class RemoveAccessLevelsCommand : DefaultCommand<AccessLevelSettings>
    {
        public RemoveAccessLevelsCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<AccessLevelSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.AssignAxsLvl];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to assign or change access levels");
                return 0;
            }

            var response = await client.DeleteJSendAsync($"{Settings.Api}/api/v2/credential/{settings.UniqueKey}/accesslevel/groups", settings.AccessLevels);
            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine("[green]Access levels removed[/]");

                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
