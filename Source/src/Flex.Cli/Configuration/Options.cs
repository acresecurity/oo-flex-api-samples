
namespace Flex.Configuration
{
    internal class Options
    {
        public string Api { get; set; }

        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public MqttSubscriptionOptions Mqtt { get; set; } = new();
    }
}