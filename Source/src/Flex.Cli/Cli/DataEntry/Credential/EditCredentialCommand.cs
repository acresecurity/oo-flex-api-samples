using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class EditCredentialCommand : DefaultCommand<EditCredentialSettings>
    {
        public EditCredentialCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<EditCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, EditCredentialSettings commandSettings, HttpClient client, UserInfo userInfo, DataObjects.Settings.Settings settings)
        {
            // Same thing with Cardholders, there really isn't a permission for editing a cardholder. Rights are assigned to the various properties,
            // but for now we are going to assume that if you can't view the cardholders then it would be safe to say you can't edit them either.
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to edit credentials");
                return CommandLineInsufficientPermission;
            }

            // Retrieving the original credential just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving credential...", _ => client.GetJsendAsync($"api/v2/credential/{commandSettings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return CommandLineRequestError;
            }

            var original = response.Deserialize<DataObjects.Cardholder.Credential>();

            response = await AnsiConsole.Status().StartAsync("Saving cardholder...", _ => client.PutJSendAsync($"api/v2/credential/{commandSettings.UniqueKey}", commandSettings));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (commandSettings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var updated = response.Deserialize<DataObjects.Cardholder.Credential>();
                AnsiConsole.MarkupLine("[green]Credential edited successfully[/]");
                CompareAndDisplay(commandSettings, original, updated, p => p.BorderColor(Color.Green), new[] { "UniqueKey" });
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
