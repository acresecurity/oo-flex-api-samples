using Flex.Configuration;
using Flex.Services.Abstractions;
using FluentValidation;
using FluentValidation.Results;

namespace Flex.Services
{
    internal class OptionsProvider : IOptionsProvider
    {
        private readonly Microsoft.Extensions.Options.IOptions<Options> _options;
        private readonly IValidator<Options> _validation;

        public OptionsProvider(Microsoft.Extensions.Options.IOptions<Options> options, IValidator<Options> validation)
        {
            _options = options;
            _validation = validation;
        }

        public Options Options => _options?.Value;

        public ValidationResult Validate() => _validation.Validate(Options);

        public Task<ValidationResult> ValidateAsync() => _validation.ValidateAsync(Options);
    }
}
