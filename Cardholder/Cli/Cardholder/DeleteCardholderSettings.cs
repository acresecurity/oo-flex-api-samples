﻿using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    public class DeleteCardholderSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }
    }
}
