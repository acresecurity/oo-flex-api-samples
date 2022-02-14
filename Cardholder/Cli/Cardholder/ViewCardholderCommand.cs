using System.Net;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    internal class ViewCardholderCommand : DefaultCommand<ViewCardholderSettings>
    {
        public ViewCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<ViewCardholderSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, ViewCardholderSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            var response = await client.GetJsendAsync($"{_settings.Api}/api/v2/cardholder/{settings.UniqueKey}");
            if (response.IsSuccess())
            {
                var cardholder = response.Deserialize<Common.DataObjects.Cardholder>();
                DisplayObject(cardholder);

                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
