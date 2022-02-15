using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace HardwareTree.Cli
{
    internal class MomentarilyUnlockDoorCommand : DefaultCommand<UnlockSettings>
    {
        public MomentarilyUnlockDoorCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DoorSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, UnlockSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewHardware];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to momentarily unlock doors");
                return 0;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding credential...", p => client.PostJSendAsync($"{_settings.Api}/api/v2/hardware/door/{settings.UniqueKey}/momentaryunlock", settings));
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
