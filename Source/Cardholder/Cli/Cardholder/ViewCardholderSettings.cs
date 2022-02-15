using System.ComponentModel;
using Spectre.Console.Cli;

namespace Cardholder.Cli.Cardholder
{
    internal class ViewCardholderSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        [CommandOption("-c|--credentials")]
        [Description("If true return the credentials assigned to the cardholder, otherwise the cardholder.")]
        public bool Credentials { get; set; }
    }
}