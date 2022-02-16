using System.Net;
using DataEntry.Cli.Cardholder.Settings;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder
{
    internal class EditCardholderCommand : DefaultCommand<EditCardholderSettings>
    {
        public EditCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<EditCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, EditCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            // There really isn't a permission for editing a cardholder. Rights are assigned to the various properties, but for now we are going to
            // assume that if you can't view the cardholders then it would be safe to say you can't edit them either.
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to edit a cardholder");
                return 0;
            }

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Cardholder>();

            response = await AnsiConsole.Status().StartAsync("Saving cardholder...", _ => client.PutJSendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}", settings));
            if (response.IsSuccess())
            {
                var updated = response.Deserialize<Common.DataObjects.Cardholder>();
                AnsiConsole.MarkupLine("[green]Cardholder edited successfully[/]");
                CompareAndDisplay(settings, original, updated, p => p.BorderColor(Color.Green), new [] { "UniqueKey" });
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}