using FluentValidation;

namespace Flex.Configuration.Validation
{
    // ReSharper disable once UnusedMember.Global
    internal class OptionsValidation : AbstractValidator<Options>
    {
        public OptionsValidation(IValidator<MqttSubscriptionOptions> mqttValidator)
        {
            RuleFor(p => p.Mqtt)
                .SetValidator(mqttValidator);

            RuleFor(p => p.Api)
                .NotEmpty()
                .WithMessage($"'{nameof(Options.Api)}' is required.");

            RuleFor(p => p.Api)
                .Must(p => !string.IsNullOrEmpty(p) && (p.StartsWith("http://") || p.StartsWith("https://")))
                .WithMessage($"'{nameof(Options.Api)}' must start with 'http://' or 'https://'");

            RuleFor(p => p.Api)
                .NotEmpty()
                .Must(p => !string.IsNullOrEmpty(p) && !p.EndsWith("/"))
                .WithMessage($"'{nameof(Options.Api)}' must not end with '/'");

            RuleFor(p => p.Authority)
                .NotEmpty()
                .WithMessage($"'{nameof(Options.Authority)}' is required.");

            RuleFor(p => p.Authority)
                .Must(p => !string.IsNullOrEmpty(p) && (p.StartsWith("http://") || p.StartsWith("https://")))
                .WithMessage($"'{nameof(Options.Authority)}' must start with 'http://' or 'https://'");

            RuleFor(p => p.Authority)
                .Must(p => !string.IsNullOrEmpty(p) && p.EndsWith("/identity"))
                .WithMessage($"'{nameof(Options.Authority)}' must end with 'identity'");

            RuleFor(p => p.ClientId)
                .NotEmpty();

            RuleFor(p => p.ClientSecret)
                .NotEmpty();
        }
    }
}
