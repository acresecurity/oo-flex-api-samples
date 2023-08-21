using System.Runtime.Serialization;
using System.Security.Authentication;
using MQTTnet.Formatter;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flex.Configuration
{
    [DataContract]
    internal class MqttSubscriptionOptions : ReactiveObject
    {
        [Reactive, DataMember]
        public string ClientName { get; set; } = "Flex.Cli";

        [Reactive, DataMember]
        public Transport Transport { get; set; } = Transport.Tcp;

        [Reactive, DataMember]
        public string Host { get; set; } = "localhost";

        [Reactive, DataMember]
        public int Port { get; set; } = 1883;

        [Reactive, DataMember]
        public SslProtocols TlsVersion { get; set; } = SslProtocols.None;

        [Reactive, DataMember]
        public MqttProtocolVersion ProtocolVersion { get; set; } = MqttProtocolVersion.V311;

        [Reactive, DataMember]
        public bool ExternalServer { get; set; }

        [Reactive, DataMember]
        public string Username { get; set; }

        [Reactive, DataMember]
        public string Password { get; set; }
    }
}
