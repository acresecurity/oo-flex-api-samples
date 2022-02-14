using System.Net;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    public class AddCardholderCommand : DefaultCommand<AddCardholderSettings>
    {
        public AddCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<AddOrEditCardholderSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, AddCardholderSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            var response = await AnsiConsole.Status().StartAsync("Adding cardholder...", p => client.PostJSendAsync($"{_settings.Api}/api/v2/cardholder", settings));
            if (response.IsSuccess())
            {
                var added = response.Deserialize<Common.DataObjects.Cardholder>();
                AnsiConsole.MarkupLine($"[green]Cardholder added successfully[/] UniqueKey: [[{added.UniqueKey}]]");

                CompareAndDisplay(settings, new Common.DataObjects.Cardholder(), added);
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
