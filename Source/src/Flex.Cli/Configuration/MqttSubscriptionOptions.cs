using MQTTnet.Formatter;
using System.Security.Authentication;

namespace Flex.Configuration
{
    internal class MqttSubscriptionOptions
    {
        public string ClientName { get; set; } = "Flex.Cli";

        public Transport Transport { get; set; } = Transport.Tcp;

        public string Host { get; set; } = "localhost";

        public int Port { get; set; } = 1883;

        public SslProtocols TlsVersion { get; set; } = SslProtocols.None;

        public MqttProtocolVersion ProtocolVersion { get; set; } = MqttProtocolVersion.V311;

        public bool ExternalServer { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}