using System.Net;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    internal class AddCardholderCommand : DefaultCommand<AddCardholderSettings>
    {
        public AddCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<AddCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AddCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ADDPERSONNEL];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add a cardholder");
                return 0;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding cardholder...", p => client.PostJSendAsync($"{_settings.Api}/api/v2/cardholder", settings));
            if (response.IsSuccess())
            {
                var added = response.Deserialize<Common.DataObjects.Cardholder>();
                AnsiConsole.MarkupLine($"[green]Cardholder added successfully[/] UniqueKey: [[{added.UniqueKey}]]");

                CompareAndDisplay(settings, new Common.DataObjects.Cardholder(), added, p => p.BorderColor(Color.Green));
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
