using FluentValidation;

namespace Flex.Configuration.Validation
{
    // ReSharper disable once UnusedMember.Global
    internal class MqttSubscriptionOptionsValidation : AbstractValidator<MqttSubscriptionOptions>
    {
        public MqttSubscriptionOptionsValidation()
        {
            RuleFor(p => p.ClientName)
                .NotEmpty();

            RuleFor(p => p.Host)
                .NotEmpty();

            RuleFor(p => p.Host)
                .Must((root, p, context) =>
                {
                    if (root.Transport == Transport.WebSocket)
                        return !string.IsNullOrEmpty(p) && p.StartsWith("ws://") || p.StartsWith("wss://");

                    return true;
                })
                .WithMessage($"'{nameof(MqttSubscriptionOptions.Host)}' must start with 'ws://' or 'wss://'");

            RuleFor(p => p.Host)
                .Must((root, p, context) =>
                {
                    if (root.Transport == Transport.Tcp)
                        return !string.IsNullOrEmpty(p) && !p.StartsWith("ws://") && !p.StartsWith("wss://");

                    return true;
                })
                .WithMessage($"'{nameof(MqttSubscriptionOptions.Host)}' must not start with 'ws://' or 'wss://'");
            
            RuleFor(p => p.Port)
                .GreaterThan(0)
                .LessThan(65536);
        }
    }
}

