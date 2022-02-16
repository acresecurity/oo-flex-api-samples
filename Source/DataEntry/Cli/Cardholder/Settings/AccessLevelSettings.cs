﻿using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder.Settings
{
    internal class AccessLevelSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        [CommandArgument(1, "<ACCESS_LEVELS>")]
        public virtual Guid[] AccessLevels { get; set; }
    }
}
