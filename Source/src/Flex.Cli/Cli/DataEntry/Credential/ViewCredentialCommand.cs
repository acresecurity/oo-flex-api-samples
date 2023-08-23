using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class ViewCredentialCommand : AsyncCommand<ViewCredentialSettings>
    {
        public ViewCredentialCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<ViewCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCredentialSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view credentials");
                return CommandLineInsufficientPermission;
            }

            var response = await client.GetJsendAsync($"api/v2/credential/{settings.UniqueKey}");

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var cardholder = response.Deserialize<DataObjects.Cardholder.Credential>();
                DisplayObject(cardholder);
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
