using Spectre.Console;
using Spectre.Console.Cli;
using System.Text;

namespace Flex.Cli
{
    internal abstract class DefaultCommandSettings : CommandSettings
    {
        /// <summary>
        /// Optional parameter for displaying the output in JSON format
        /// </summary>
        [CommandOption("-j|--json")]
        public bool OutputJson { get; set; }

        [CommandOption("--no-banner")]
        public bool IncludeBanner { get; set; } = true;

        protected virtual FluentValidation.Results.ValidationResult InternalValidate() => new();

        #region Overrides of CommandSettings

        /// <summary>
        /// Use <see cref="InternalValidate"/> to validate the command settings
        /// </summary>
        public sealed override ValidationResult Validate()
        {
            var result = InternalValidate();
            if (result.IsValid)
                return ValidationResult.Success();

            var sb = new StringBuilder();
            sb.AppendLine(ValidationErrorHeader);
            sb.AppendLine();
            foreach (var item in result.Errors)
                sb.AppendLine($"[yellow]{item.PropertyName}[/]: {item.ErrorMessage}");

            return ValidationResult.Error(sb.ToString());
        }

        #endregion
    }
}
