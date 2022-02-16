using Spectre.Console.Cli;

namespace DataEntry.Cli.Credential.Settings
{
    internal class AccessLevelSettings : CredentialSettings
    {
        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        [CommandArgument(0, "<UNIQUE_KEY>")]
        public virtual Guid? UniqueKey { get; set; }

        [CommandArgument(1, "<ACCESS_LEVELS>")]
        public virtual Guid[] AccessLevels { get; set; }
    }
}
