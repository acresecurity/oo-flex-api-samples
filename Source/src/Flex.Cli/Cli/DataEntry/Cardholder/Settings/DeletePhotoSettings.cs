using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder.Settings
{
    internal class DeletePhotoSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }
    }
}