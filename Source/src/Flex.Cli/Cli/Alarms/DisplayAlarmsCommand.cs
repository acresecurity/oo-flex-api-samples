using System.Net;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Alarms
{
    /// <summary>
    /// Fetch and display all of the current alarms.
    /// </summary>
    internal class DisplayAlarmsCommand : AsyncCommand<DefaultCommandSettings>
    {
        public DisplayAlarmsCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand

        protected override async Task<int> ExecuteAsync(CommandContext context, DefaultCommandSettings settings, HttpClient client, UserInfo userInfo, Dictionary<int, string> eventDescriptions)
        {
            //
            // Retrieve all of the current alarms and display them
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving alarms ...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/hardware/alarms"));

            if (!response.IsSuccess())
                return DisplayError(response);

            var alarms = response.Deserialize<Alarm[]>();
            OutputTable(eventDescriptions, alarms);

            return CommandLineSuccess;
        }

        #endregion
    }
}
