using System.Globalization;
using FluentValidation;
using FluentValidation.Validators;

namespace Flex.Validation
{
    internal class AnsiValidator<T> : PropertyValidator<T, string>
    {
        private static bool ContainsUnicodeCharacter(string input)
        {
            //const int maxAnsiCode = 255;
            //return input.Any(p => p > maxAnsiCode);
            return !string.IsNullOrEmpty(input) && input.ToCharArray().Any(p => char.GetUnicodeCategory(p) == UnicodeCategory.OtherLetter);
        }

        #region Overrides of PropertyValidator<T,string>

        protected override string GetDefaultMessageTemplate(string errorCode) => "'{PropertyName}' must contain only ANSI characters. Unicode characters are not supported.";

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            return !ContainsUnicodeCharacter(value);
        }

        public override string Name => "AnsiValidator";

        #endregion
    }
}