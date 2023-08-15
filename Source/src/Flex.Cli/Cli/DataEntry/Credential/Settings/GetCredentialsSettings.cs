using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential.Settings
{
    internal class GetCredentialsSettings : CredentialSettings
    {
        [CommandOption("-w|--where")]
        public string Where { get; set; }

        [CommandOption("-o|--orderBy")]
        public string OrderBy { get; set; }
    }
}