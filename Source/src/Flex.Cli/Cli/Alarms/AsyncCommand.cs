using System.Globalization;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Alarms
{
    /// <summary>
    /// Provides simple helpers for outputting alarms in a table format.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    internal abstract class AsyncCommand<TSettings> : Cli.AsyncCommand<TSettings>
        where TSettings : DefaultCommandSettings
    {
        protected AsyncCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        //protected Dictionary<int, string> EventDescriptions { get; private set; }

        protected abstract Task<int> ExecuteAsync(CommandContext context, TSettings settings, HttpClient client, UserInfo userInfo, Dictionary<int, string> eventDescriptions);

        #region Overrides of AsyncCommand<AlarmSettings>

        protected sealed override async Task<int> ExecuteAsync(CommandContext context, TSettings settings, HttpClient client, UserInfo userInfo)
        {
            var eventDescriptions = await Cache.EventDescriptions();
            if (eventDescriptions == null)
                return CommandLineCacheError;

            return await ExecuteAsync(context, settings, client, userInfo, eventDescriptions);
        }

        #endregion
        
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
                    item.Transaction.ToString(CultureInfo.InvariantCulture),
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
