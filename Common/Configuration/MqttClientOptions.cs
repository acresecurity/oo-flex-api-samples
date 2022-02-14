using System.Security.Authentication;

namespace Common.Configuration
{
    public class MqttClientOptions : MQTTnet.Client.MqttClientOptions
    {
        public string ClientName { get; set; }

        public Transport Transport { get; set; } = Transport.Tcp;

        public string Host { get; set; } = "localhost";

        public int Port { get; set; } = 1883;

        public SslProtocols TlsVersion { get; set; } = SslProtocols.None;
    }
}
