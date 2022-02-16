using System.Net;
using DataEntry.Cli.Cardholder.Settings;
using Common.Configuration;
using Common.DataObjects;
using Common.Responses;
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

            JSendResponse response;
            if (settings.Credentials)
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/credentials");
                if (response.IsSuccess())
                {
                    var credentials = response.Deserialize<Common.DataObjects.Credential[]>();
                    DisplayObject(credentials);

                    return 0;
                }
            }
            else
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}");
                if (response.IsSuccess())
                {
                    var cardholder = response.Deserialize<Common.DataObjects.Cardholder>();
                    DisplayObject(cardholder);

                    return 0;
                }
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
