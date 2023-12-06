using System.Net;
using Flex.Cli.Alarms.Settings;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Alarms
{
    /// <summary>
    /// Base class that handles checks (permissions) and gathering data needed for all of the alarm handling commands (Acknowledge, Dismiss, Clear).
    /// For acknowledging an alarm <see cref="AcknowledgeCommand"/>
    /// For clearing an alarm <see cref="ClearCommand"/>
    /// For dismissing an alarm <see cref="DismissCommand"/>
    /// </summary>
    internal abstract class DefaultCommand : AsyncCommand<AlarmSettings>
    {
        protected DefaultCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        protected abstract Task<int> ExecuteAsync(CommandContext context, AlarmSettings settings, HttpClient client, UserInfo userInfo, Alarm alarm);

        #region Overrides of AsyncCommand<AlarmSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AlarmSettings settings, HttpClient client, UserInfo userInfo, Dictionary<int, string> eventDescriptions)
        {
            //
            // Fetch the alarm to be processed. This way we can verify the state and the alarm priority before hand to avoid
            // and server side verification errors that might be returned.
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving alarms", _ => client.GetJsendAsync($"/api/v2/hardware/alarm/{settings.UniqueId}"));

            if (!response.IsSuccess())
                return DisplayError(response);

            var alarm = response.Deserialize<Alarm>();

            OutputTable(eventDescriptions, alarm);

            await ExecuteAsync(context, settings, client, userInfo, alarm);

            return CommandLineSuccess;
        }

        #endregion
    }
}
