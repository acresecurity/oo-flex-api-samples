﻿using Spectre.Console.Cli;

namespace AlarmsProcessing.Cli
{
    /// <summary>
    /// Settings for acknowledge, clear, and dismiss command line parameters.
    /// </summary>
    internal class AlarmSettings : CommandSettings
    {
        /// <summary>
        /// Required unique ID for the alarm to acknowledge, clear, or dismiss
        /// </summary>
        [CommandArgument(0, "<UniqueId>")]
        public int UniqueId { get; set; }

        /// <summary>
        /// Optional parameter for supplying the dispatch
        /// </summary>
        [CommandOption("-d|--dispatch <dispatchText>")]
        public string DispatchText { get; set; }
    }
}