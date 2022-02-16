using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder.Settings
{
    internal class GetCardholdersSettings : CardholderSettings
    {
        [CommandOption("-w|--where")]
        public string Where { get; set; } = null;

        [CommandOption("-o|--orderBy")]
        public string OrderBy { get; set; } = null;
    }
}
