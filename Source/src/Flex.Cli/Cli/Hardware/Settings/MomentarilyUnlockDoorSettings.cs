using System.ComponentModel;
using Spectre.Console.Cli;

namespace Flex.Cli.Hardware.Settings
{
    internal class MomentarilyUnlockDoorSettings : HardwareSettings
    {
        /// <summary>
        /// Unique identifier for the hardware
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        [Description("Unique Identifier (GUID) of the hardware")]
        public virtual Guid? UniqueKey { get; set; }
    }
}