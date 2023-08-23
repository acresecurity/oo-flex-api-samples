using System.Runtime.Serialization;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flex.Configuration
{
    [DataContract]
    internal class Options : ReactiveObject
    {
        [Reactive, DataMember]
        public string Api { get; set; }

        [Reactive, DataMember]
        public string ClientId { get; set; }

        [Reactive, DataMember]
        public string ClientSecret { get; set; }

        [Reactive, DataMember]
        public MqttSubscriptionOptions Mqtt { get; set; } = new();
    }
}
