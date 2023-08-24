using Flex.Configuration;
using FluentValidation.Results;

namespace Flex.Services.Abstractions
{
    internal interface IOptionsProvider
    {
        Options Options { get; }

        ValidationResult Validate();

        Task<ValidationResult> ValidateAsync();
    }
}
