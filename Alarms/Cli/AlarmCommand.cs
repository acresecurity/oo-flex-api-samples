using System.Net;
using Common;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AlarmsProcessing.Cli
{
    /// <summary>
    /// Base class that handles checks (permissions) and gathering data needed for all of the alarm handling commands (Acknowledge, Dismiss, Clear).
    /// For acknowledging an alarm <see cref="AcknowledgeCommand"/>
    /// For clearing an alarm <see cref="ClearCommand"/>
    /// For dismissing an alarm <see cref="DismissCommand"/>
    /// </summary>
    public abstract class AlarmCommand : AsyncCommand<AlarmSettings>
    {
        public AlarmCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of Command<AlarmSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, AlarmSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            //
            // Fetch operator user information which include the rights assigned to the operator.
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving operator/user information", p => client.GetJsendAsync($"{_settings.Api}account/user"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var userInfo = response.Deserialize<UserInfo>();

            //
            // Fetch the alarm to be processed. This way we can verify the state and the alarm priority before hand to avoid
            // and server side verification errors that might be returned.
            //
            response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving alarms", p => client.GetJsendAsync($"{_settings.Api}/api/v2/hardware/alarm/{settings.UniqueId}"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var alarm = response.Deserialize<Alarm>();

            Dictionary<int, string> eventDescriptions;

            //
            // Retrieve the event descriptions just so that we can provide it on the table when displayed.
            //
            var (pagedResponse, descriptions) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving event descriptions", p => client.FetchPaged<EventDescription[]>($"{_settings.Api}/api/v2/hardware/event/descriptions"));

            if (pagedResponse.IsSuccess())
                eventDescriptions = descriptions.ToDictionary(p => p.UniqueId, p => p.Description);
            else
            {
                DisplayError(pagedResponse);
                return 1;
            }

            OutputTable(eventDescriptions, alarm);

            await ExecuteAsync(context, settings, client, userInfo, alarm);

            return 0;
        }

        #endregion

        protected abstract Task ExecuteAsync(CommandContext context, AlarmSettings settings, HttpClient client, UserInfo userInfo, Alarm alarm);
    }
}