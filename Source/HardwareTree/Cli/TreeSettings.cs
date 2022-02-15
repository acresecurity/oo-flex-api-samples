using Spectre.Console.Cli;

namespace HardwareTree.Cli
{
    internal class TreeSettings : CommandSettings
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