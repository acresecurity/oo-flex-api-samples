using System.ComponentModel;
using Spectre.Console.Cli;

namespace Hardware.Cli
{
    internal class DoorModeSettings : HardwareSettings
    {
        /// <summary>
        /// Unique identifier for the hardware
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        [Description("Unique Identifier (GUID) of the hardware")]
        public virtual Guid? UniqueKey { get; set; }

        [CommandArgument(1, "<MODE>")]
        public string Mode { get; set; }
    }
}
