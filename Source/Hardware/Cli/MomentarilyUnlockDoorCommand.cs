using System.Net;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Hardware.Cli
{
    internal class MomentarilyUnlockDoorCommand : DefaultCommand<MomentarilyUnlockDoorSettings>
    {
        public MomentarilyUnlockDoorCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DoorSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, MomentarilyUnlockDoorSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewHardware];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to momentarily unlock doors");
                return 0;
            }

            var response = await AnsiConsole.Status().StartAsync("Sending momentary unlock command...", _ => client.PostJSendAsync($"{Settings.Api}/api/v2/hardware/door/{settings.UniqueKey}/momentaryunlock", null));
            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine("[green]Door was momentarily unlocked[/]");
                return 0;
            }

            DisplayError(response);

            return 1;
        }

        #endregion
    }
}
