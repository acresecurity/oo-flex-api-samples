using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Flex.DataObjects.Hardware
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy), NamingStrategyParameters = new object[] { true, true, true })]
    public partial class Status
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData = new Dictionary<string, JToken>();

        public string Type { get; set; }

        public string UniqueKey { get; set; }

        public string ParentKey { get; set; }

        [JsonIgnore]
        public string Index => UniqueKey + ParentKey;

        [JsonIgnore]
        public JToken this[string key] => _additionalData.TryGetValue(key, out var result) ? result : default;
    }
}