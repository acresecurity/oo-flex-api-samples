﻿using System.Net;
using System.Text;
using Common;
using Common.Configuration;
using Common.DataObjects;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AlarmsProcessing.Cli
{
    /// <summary>
    /// Clear alarm command
    /// </summary>
    internal class ClearCommand : DefaultCommand
    {
        public ClearCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AlarmCommand

        protected override async Task ExecuteAsync(CommandContext context, AlarmSettings settings, HttpClient client, UserInfo userInfo, Alarm alarm)
        {
            //
            // Depending on the configuration of the system, some operators may not be able to clear an alarm if they do not have access to that priority level.
            // Priority levels are from 1-15. In this code, right.AsBool() if true, the operator can clear it. If false, they can not.
            // The server will execute a preprocess check to verify the priority level and will return a verification error if the operator can not.
            // This is how you can do the same check before hand and know if it will allow it or not.
            //
            var right = userInfo[alarm.PriorityToRight(UserRights.ALLOWACK1)];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", $"Operator is not allowed to clear an alarm with a priority of [{alarm.Priority}]");
                return;
            }

            // 
            // Depending on the configuration of the system, some operators might be required to provide a dispatch text when handling an alarm.
            // This check will determine if that is a requirement or not. These are also alarm priority dependent (1-15).
            // In this code, right.AsBool() if true the dispatch text is required, false if it isn't.
            //
            right = userInfo[alarm.PriorityToRight(UserRights.RQRDISPTXT1)];
            if (right.AsBool() && string.IsNullOrEmpty(settings.DispatchText))
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", $"Operator is required to supply a dispatch text for alarm priority of [{alarm.Priority}]".EscapeMarkup());
                return;
            }

            if (alarm.Status is AlarmStatus.PendingClear)
            {
                var url = $"{_settings.Api}/api/v2/hardware/alarm/{settings.UniqueId}/clear";

                var content = new StringContent(string.IsNullOrEmpty(settings.DispatchText) ? string.Empty : settings.DispatchText, Encoding.ASCII, "text/plain");
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Sending Clear command", p => client.PostJSendAsync(url, content));

                if (response.IsSuccess())
                    AnsiConsole.WriteLine("Alarm was cleared");
                else
                    DisplayError(response);
            }
            else
                AnsiConsole.WriteLine("Alarm can not be cleared at this time");
        }

        #endregion
    }
}