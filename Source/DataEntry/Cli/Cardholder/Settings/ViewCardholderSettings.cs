using System.ComponentModel;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder.Settings
{
    internal class ViewCardholderSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        [CommandOption("-c|--credentials")]
        [Description("If true return the credentials assigned to the cardholder.")]
        public bool Credentials { get; set; }

        [CommandOption("-p|--photos")]
        [Description("If true return the photos assigned to the cardholder.")]
        public bool Photos { get; set; }
    }
}