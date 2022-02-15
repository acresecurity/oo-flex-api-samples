using Spectre.Console.Cli;

namespace HardwareTree.Cli
{
    internal class DoorSettings : CommandSettings
    {

    }

    internal class UnlockSettings : DoorSettings
    {
        /// <summary>
        /// Unique identifier for the parent cardholder record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }
    }
}
