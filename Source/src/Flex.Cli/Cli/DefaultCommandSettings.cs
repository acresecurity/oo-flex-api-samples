using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli
{
    internal class DefaultCommandSettings : CommandSettings
    {
        /// <summary>
        /// Optional parameter for displaying the output in JSON format
        /// </summary>
        [CommandOption("-j|--json")]
        public bool OutputJson { get; set; }

        /// <summary>
        /// Optional parameter for flushing the cache
        /// </summary>
        /// <remarks>
        /// Will also reset authentication tokens requiring a new login
        /// </remarks>
        [CommandOption("--flush")]
        public bool FlushCache { get; set; }

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
            
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine(ValidationErrorHeader);

            var table = new Table();
            table.BorderColor(Color.Red);
            table.AddColumn("Field");
            table.AddColumn("Message");

            foreach (var item in result.Errors)
                table.AddRow(item.PropertyName ?? "<Missing Field>", item.ErrorMessage.EscapeMarkup());

            table.Expand();
            AnsiConsole.Write(table);

            return ValidationResult.Error(ValidationErrorHeader);
        }

        #endregion
    }
}
