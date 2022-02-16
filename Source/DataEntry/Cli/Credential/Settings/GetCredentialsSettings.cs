using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential.Settings
{
    internal class GetCredentialsSettings : CredentialSettings
    {
        [CommandOption("-w|--where")]
        public string Where { get; set; } = null;

        [CommandOption("-o|--orderBy")]
        public string OrderBy { get; set; } = null;
    }
}
