﻿using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential.Settings
{
    internal class ViewCredentialSettings : CredentialSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }
    }
}