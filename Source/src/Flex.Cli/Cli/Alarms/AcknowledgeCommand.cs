using System.Net;
using System.Text;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;
using Flex.DataObjects.Hardware;

namespace Flex.Cli.Alarms
{
    /// <summary>
    /// Acknowledge alarm command
    /// </summary>
    internal class AcknowledgeCommand : DefaultCommand
    {
        public AcknowledgeCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of DefaultCommand

        protected override async Task<int> ExecuteAsync(CommandContext context, Settings.AlarmSettings settings, HttpClient client, UserInfo userInfo, Alarm alarm)
        {
            //
            // Depending on the configuration of the system, some operators may not be able to acknowledge an alarm if they do not have access to that priority level.
            // Priority levels are from 1-15. In this code, right.AsBool() if true, the operator can acknowledge it. If false, they can not.
            // The server will execute a preprocess check to verify the priority level and will return a verification error if the operator can not.
            // This is how you can do the same check before hand and know if it will allow it or not.
            //
            var right = userInfo[alarm.PriorityToRight(UserRights.ALLOWACK1)];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", $"Operator is not allowed to acknowledge an alarm with a priority of [{alarm.Priority}]");
                return CommandLineInsufficientPermission;
            }

            if (alarm.Status is AlarmStatus.Alarm or AlarmStatus.ReturnToNormal)
            {
                var url = $"{Settings.Api}/api/v2/hardware/alarm/{settings.UniqueId}/acknowledge";

                var content = new StringContent(string.IsNullOrEmpty(settings.DispatchText) ? string.Empty : settings.DispatchText, Encoding.ASCII, "text/plain");
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Sending Acknowledge command", _ => client.PostJSendAsync(url, content));

                if (response.IsSuccess())
                    AnsiConsole.WriteLine("Alarm was acknowledged");

                return DisplayError(response);
            }
            
            AnsiConsole.WriteLine("Alarm can not be acknowledged at this time");
            return CommandLineCancelled;
        }

        #endregion
    }
}
