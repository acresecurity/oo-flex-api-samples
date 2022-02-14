using System.Net;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    public class EditCardholderCommand : DefaultCommand<EditCardholderSettings>
    {
        public EditCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<AddCardholderSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, EditCardholderSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            // Retrieving the original cardholder just so that we can compare and show the results.
            var response = await AnsiConsole.Status().StartAsync("Retrieving cardholder...", p => client.GetJsendAsync($"{_settings.Api}/api/v2/cardholder/{settings.UniqueKey}"));
            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var original = response.Deserialize<Common.DataObjects.Cardholder>();

            response = await AnsiConsole.Status().StartAsync("Saving cardholder...", p => client.PutJSendAsync($"{_settings.Api}/api/v2/cardholder/{settings.UniqueKey}", settings));
            if (response.IsSuccess())
            {
                var updated = response.Deserialize<Common.DataObjects.Cardholder>();
                AnsiConsole.MarkupLine("[green]Cardholder edited successfully[/]");
                CompareAndDisplay(settings, original, updated, new [] { "UniqueKey" });
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}