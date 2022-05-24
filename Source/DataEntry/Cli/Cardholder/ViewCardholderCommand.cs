using System.Net;
using DataEntry.Cli.Cardholder.Settings;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder
{
    internal class ViewCardholderCommand : DefaultCommand<ViewCardholderSettings>
    {
        public ViewCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<ViewCardholderSettings>
        
        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view cardholders");
                return 0;
            }

            var response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}");
            if (response.IsSuccess())
            {
                var cardholder = response.Deserialize<Common.DataObjects.Cardholder>();
                DisplayObject(cardholder);
            }

            if (settings.Credentials)
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/credentials");
                if (response.IsSuccess())
                {
                    var credentials = response.Deserialize<Common.DataObjects.Credential[]>();
                    DisplayObject(credentials);
                }
            }

            if (settings.Photos)
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/photos");
                if (response.IsSuccess())
                {
                    var photos = response.Deserialize<Common.DataObjects.Photo[]>();
                    DisplayObject(photos);
                }
            }

            return DisplayError(response) ? 1 : 0;
        }

        #endregion
    }
}
