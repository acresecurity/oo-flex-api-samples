using System.Net;
using Common.Configuration;
using Common.DataObjects;
using Humanizer;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Hardware.Cli
{
    internal class DoorModeCommand : DefaultCommand<DoorModeSettings>
    {
        public DoorModeCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DoorModeSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DoorModeSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.AllowCtrlAcmMode];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to change the door mode");
                return 0;
            }

            var response = await AnsiConsole.Status().StartAsync("Sending door mode command...", _ =>
                client.PostJSendAsync($"{Settings.Api}/api/v2/hardware/door/{settings.UniqueKey}/mode", settings.Mode.Camelize()));

            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine($"[green]Door mode was set to[/] [yellow]{settings.Mode.Pascalize()}[/]");
                return 0;
            }

            DisplayError(response);

            return 1;
        }

        #endregion
    }
}
