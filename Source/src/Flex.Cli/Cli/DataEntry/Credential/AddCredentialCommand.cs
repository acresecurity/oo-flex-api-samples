using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class AddCredentialCommand : DefaultCommand<AddCredentialSettings>
    {
        public AddCredentialCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AddCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AddCredentialSettings commandSettings, HttpClient client, UserInfo userInfo, DataObjects.Settings.Settings settings)
        {
            var right = userInfo[UserRights.ADDCARD];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add credentials");
                return CommandLineInsufficientPermission;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding credential...", _ => client.PostJSendAsync($"{Settings.Api}/api/v2/cardholder/{commandSettings.CardholderKey}/credential", settings));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (commandSettings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var added = response.Deserialize<DataObjects.Cardholder.Credential>();
                AnsiConsole.MarkupLine($"[green]Credential added successfully[/] UniqueKey: [[{added.UniqueKey}]]");
                CompareAndDisplay(commandSettings, new DataObjects.Cardholder.Credential(), added, p => p.BorderColor(Color.Green), new[] { nameof(AddCredentialSettings.CardholderKey) });
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}