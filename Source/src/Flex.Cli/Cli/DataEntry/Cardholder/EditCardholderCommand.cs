using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class EditCardholderCommand : DefaultCommand<EditCardholderSettings>
    {
        public EditCardholderCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<EditCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, EditCardholderSettings commandSettings, HttpClient client, UserInfo userInfo, DataObjects.Settings.Settings settings)
        {
            // There really isn't a permission for editing a cardholder. Rights are assigned to the various properties, but for now we are going to
            // assume that if you can't view the cardholders then it would be safe to say you can't edit them either.
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to edit a cardholder");
                return CommandLineInsufficientPermission;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", _ => client.GetJsendAsync($"api/v2/cardholder/{commandSettings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return CommandLineRequestError;
            }

            var original = response.Deserialize<DataObjects.Cardholder.Cardholder>();

            response = await AnsiConsole.Status().StartAsync("Saving cardholder...", _ => client.PutJSendAsync($"api/v2/cardholder/{commandSettings.UniqueKey}", commandSettings));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (commandSettings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var updated = response.Deserialize<DataObjects.Cardholder.Cardholder>();
                AnsiConsole.MarkupLine("[green]Cardholder edited successfully[/]");
                CompareAndDisplay(commandSettings, original, updated, p => p.BorderColor(Color.Green), new[] { "UniqueKey" });
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
