using Spectre.Console.Cli;

namespace Flex.Cli.Hardware.Settings
{
    internal class TreeSettings : DefaultCommandSettings
    {
        /// <summary>
        /// Optional parameter to filter the hardware tree
        /// </summary>
        [CommandOption("-f|--filter <filter>")]
        public string[] Filter { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Optional parameter to flatten the hardware tree
        /// </summary>
        [CommandOption("-a|--flatten <flatten>")]
        public bool Flatten { get; set; }
    }
}