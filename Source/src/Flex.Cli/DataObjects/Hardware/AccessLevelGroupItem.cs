using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Flex.DataObjects.Hardware
{
    public partial class AccessLevelGroupItem
    {
        [JsonIgnore]
        [JsonExtensionData]
        public Dictionary<string, JToken> AdditionalData { get; set; } = new();

        /// <summary>
        /// Hardware type assigned to the access level
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// Unique identifier for the hardware assigned to the access level
        /// </summary>
        public Guid UniqueKey { get; set; }

        /// <summary>
        /// Friendly description of the hardware item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Address for the hardware item
        /// </summary>
        public string Address { get; set; }

        public Guid? ParentKey { get; set; }

        public Guid? TimeScheduleKey { get; set; }

        public string TimeScheduleDescription { get; set; }

        // Mercury specific, only for 0:Elevator hardware type
        public Guid? FloorGroupKey { get; set; }

        // Mercury specific, only for 0:Elevator hardware type
        public string FloorGroupDescription { get; set; }

        public bool? CanAssign { get; set; }

        public SecurityLevel SecurityLevel { get; set; }
    }
}