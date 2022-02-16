using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Common.DataObjects
{
    public class HardwareTreeItem
    {
        /// <summary>
        /// Octet based identifier for the hardware type.
        /// </summary>
        /// <example>
        /// 1.2.[D,E,MPG,TS]
        ///   1. "Site/Driver"
        ///     2. "Controller"
        ///       3. "Door, Elevator, Monitor Point Group, Time Schedule"
        /// 
        /// 1.2.3.[I,O,R]4
        /// 1. "Site/Driver"
        ///   2. "Controller"
        ///     3. "SubController"
        ///       4. "Input, Output, Reader
        /// </example>
        [JsonProperty("address")]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Description or name of the hardware item
        /// </summary>
        [JsonProperty("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Defines what the type of hardware this item is.
        /// </summary>
        /// <example>
        /// Controller, Door, Elevator, Input, Output, SubController, Reader, TimeSchedule
        /// </example>
        [JsonProperty("type")]
        [JsonPropertyName("type")]
        public virtual string Type { get; set; }

        /// <summary>
        /// GUID based unique key of the hardware item
        /// </summary>
        [JsonProperty("uniqueKey")]
        [JsonPropertyName("uniqueKey")]
        public Guid? UniqueKey { get; set; }

        /// <summary>
        /// Items like the Mercury time schedules require the controller they are assigned to.
        /// </summary>
        [JsonProperty("isDependent")]
        [JsonPropertyName("isDependent")]
        public virtual bool? IsDependent { get; set; }

        /// <summary>
        /// Unique key of the parent hardware item.
        /// </summary>
        [JsonProperty("parentKey")]
        [JsonPropertyName("parentKey")]
        public virtual Guid? ParentKey { get; set; }

        /// <summary>
        /// Hardware type of the parent hardware item
        /// </summary>
        [JsonProperty("parentType")]
        [JsonPropertyName("parentType")]
        public virtual string ParentType { get; set; }

        /// <summary>
        /// Child hardware items.
        /// </summary>
        /// <remarks>
        /// The server will return all, some or no child items depending on the size of the system. In which case, if
        /// <see cref="IsCollection"/> is true and <see cref="Items"/> is null then you will need to retrieve the child items.
        /// </remarks>
        [JsonProperty("items")]
        [JsonPropertyName("items")]
        public virtual IEnumerable<HardwareTreeItem> Items { get; set; } = Array.Empty<HardwareTreeItem>();

        /// <summary>
        /// Provides additional information on a hardware type, for example, if a door is a POE or wireless.
        /// </summary>
        [JsonProperty("subType")]
        [JsonPropertyName("subType")]
        public virtual string SubType { get; set; }

        /// <summary>
        /// If the hardware item has child items
        /// </summary>
        /// <example>
        /// Mercury doors will have input and outpoint points, and readers.
        /// </example>
        [JsonProperty("isCollection")]
        [JsonPropertyName("isCollection")]
        public virtual bool IsCollection { get; set; }

        public string Index => $"{UniqueKey}{ParentKey}";

        public virtual string DisplayText
        {
            get
            {
                if (!string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Description))
                    return $"{Address}: {Description}";

                if (!string.IsNullOrEmpty(Address))
                    return Address;

                if (!string.IsNullOrEmpty(Description))
                    return Description;

                return "Unknown hardware type";
            }
        }
    }
}