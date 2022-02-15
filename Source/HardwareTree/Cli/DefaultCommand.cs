using System.Net;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace HardwareTree.Cli
{
    internal abstract class DefaultCommand<TSettings> : Common.Cli.AsyncCommand<TSettings>
        where TSettings : CommandSettings
    {
        protected DefaultCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<TSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, TSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            //
            // Fetch operator user information which include the rights assigned to the operator.
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving operator/user information", p => client.GetJsendAsync($"{_settings.Api}/account/user"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var userInfo = response.Deserialize<UserInfo>();
            return await ExecuteAsync(context, settings, client, userInfo);
        }

        #endregion

        protected abstract Task<int> ExecuteAsync(CommandContext context, TSettings settings, HttpClient client, UserInfo userInfo);
    }
}
