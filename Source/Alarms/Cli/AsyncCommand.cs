using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AlarmsProcessing.Cli
{
    /// <summary>
    /// Provides simple helpers for outputting alarms in a table format.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    internal abstract class AsyncCommand<TSettings> : Common.Cli.AsyncCommand<TSettings>
        where TSettings : CommandSettings
    {
        public AsyncCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        /// <summary>
        /// Helper method to display the alarms in a table format in the console window
        /// </summary>
        /// <param name="eventDescriptions"></param>
        /// <param name="alarms"></param>
        protected static void OutputTable(Dictionary<int, string> eventDescriptions, params Alarm[] alarms)
        {
            var table = new Table()
                .Title("Alarms")
                .BorderColor(Color.Red)
                .AddColumn("ID")
                .AddColumn("Priority")
                .AddColumn("Date/Time")
                .AddColumn("Hardware")
                .AddColumn("Description")
                .AddColumn("Count")
                .AddColumn("Status");

            foreach (var item in alarms)
            {
                table.AddRow(
                    item.UniqueId.ToString(),
                    item.Priority.ToString(),
                    item.Transaction.ToString(),
                    $"{item.HardwareAddress}: {item.HardwareDescription}".EscapeMarkup(),
                    eventDescriptions[item.EventDescriptionId].EscapeMarkup(),
                    item.Count.ToString(),
                    item.Status.ToString());
            }

            table.Expand();

            AnsiConsole.Write(table);
        }
    }
}