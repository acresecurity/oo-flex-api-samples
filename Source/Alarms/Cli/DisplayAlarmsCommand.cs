using System.Net;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AlarmsProcessing.Cli
{
    /// <summary>
    /// Fetch and display all of the current alarms.
    /// </summary>
    internal class DisplayAlarmsCommand : AsyncCommand<EmptyCommandSettings>
    {
        public DisplayAlarmsCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand

        public override async Task<int> ExecuteAsync(CommandContext context, EmptyCommandSettings settings)
        {
            //var client = await FlexApiExtensions.GetHttpClientViaResourceOwnerPassword();
            var client = await GetClient();
            if (client == null)
                return 1;

            Dictionary<int, string> eventDescriptions;

            //
            // Retrieve the event descriptions just so that we can provide it on the table when displayed.
            //
            var (pagedResponse, descriptions) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving event descriptions ...", _ => client.FetchPaged<EventDescription[]>($"{Settings.Api}/api/v2/hardware/event/descriptions"));

            if (pagedResponse.IsSuccess())
                eventDescriptions = descriptions.ToDictionary(p => p.UniqueId, p => p.Description);
            else
            {
                DisplayError(pagedResponse);
                return 1;
            }

            //
            // Retrieve all of the current alarms and display them
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving alarms ...", _ => client.GetJsendAsync($"{Settings.Api}/api/v2/hardware/alarms"));

            if (response.IsSuccess())
            {
                var alarms = response.Deserialize<Alarm[]>();
                OutputTable(eventDescriptions, alarms);
            }
            else
            {
                DisplayError(response);
                return 1;
            }

            return 0;
        }

        #endregion
    }
}