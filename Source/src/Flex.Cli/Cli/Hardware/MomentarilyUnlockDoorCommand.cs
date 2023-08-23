using System.Net;
using Flex.Cli.Hardware.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Hardware
{
    internal class MomentarilyUnlockDoorCommand : AsyncCommand<MomentarilyUnlockDoorSettings>
    {
        public MomentarilyUnlockDoorCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<DoorSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, MomentarilyUnlockDoorSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewHardware];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to momentarily unlock doors");
                return CommandLineInsufficientPermission;
            }

            var response = await AnsiConsole.Status().StartAsync("Sending momentary unlock command...", _ => client.PostJSendAsync($"api/v2/hardware/door/{settings.UniqueKey}/momentaryunlock", null));

            if (!response.IsSuccess())
                return DisplayError(response);

            AnsiConsole.MarkupLine("[green]Door was momentarily unlocked[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
