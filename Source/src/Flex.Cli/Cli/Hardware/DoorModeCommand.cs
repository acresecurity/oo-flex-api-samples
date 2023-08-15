using System.Net;
using Flex.Cli.Hardware.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Humanizer;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Hardware
{
    internal class DoorModeCommand : AsyncCommand<DoorModeSettings>
    {
        public DoorModeCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<DoorModeSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DoorModeSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.AllowCtrlAcmMode];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to change the door mode");
                return CommandLineInsufficientPermission;
            }

            var response = await AnsiConsole.Status().StartAsync("Sending door mode command...", _ =>
                client.PostJSendAsync($"{Settings.Api}/api/v2/hardware/door/{settings.UniqueKey}/mode", settings.Mode.Camelize()));

            if (!response.IsSuccess())
                return DisplayError(response);

            AnsiConsole.MarkupLine($"[green]Door mode was set to[/] [yellow]{settings.Mode.Pascalize()}[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}