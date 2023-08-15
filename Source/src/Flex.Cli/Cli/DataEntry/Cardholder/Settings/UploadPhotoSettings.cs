using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder.Settings
{
    internal class UploadPhotoSettings : CardholderSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        /// <summary>
        /// The image to upload
        /// </summary>
        [CommandArgument(1, "<FILENAME>")]
        public virtual string Filename { get; set; }
    }
}