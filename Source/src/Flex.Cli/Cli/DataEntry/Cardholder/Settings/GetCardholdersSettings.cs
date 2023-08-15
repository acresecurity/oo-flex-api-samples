using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder.Settings
{
    internal class GetCardholdersSettings : CardholderSettings
    {
        [CommandOption("-w|--where")]
        public string Where { get; set; }

        [CommandOption("-o|--orderBy")]
        public string OrderBy { get; set; }
    }
}