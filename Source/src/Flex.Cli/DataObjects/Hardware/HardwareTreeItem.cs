using Newtonsoft.Json;
using Spectre.Console;
using System.Text;

namespace Flex.DataObjects.Hardware
{
    public partial class HardwareTreeItem
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
        public string Address { get; set; }

        /// <summary>
        /// Description or name of the hardware item
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Defines what the type of hardware this item is.
        /// </summary>
        /// <example>
        /// Controller, Door, Elevator, Input, Output, SubController, Reader, TimeSchedule
        /// </example>
        [JsonProperty("type")]
        public virtual string Type { get; set; }

        /// <summary>
        /// GUID based unique key of the hardware item
        /// </summary>
        [JsonProperty("uniqueKey")]
        public Guid? UniqueKey { get; set; }

        /// <summary>
        /// Items like the Mercury time schedules require the controller they are assigned to.
        /// </summary>
        [JsonProperty("isDependent")]
        public virtual bool? IsDependent { get; set; }

        /// <summary>
        /// Unique key of the parent hardware item.
        /// </summary>
        [JsonProperty("parentKey")]
        public virtual Guid? ParentKey { get; set; }

        /// <summary>
        /// Hardware type of the parent hardware item
        /// </summary>
        [JsonProperty("parentType")]
        public virtual string ParentType { get; set; }

        /// <summary>
        /// Child hardware items.
        /// </summary>
        /// <remarks>
        /// The server will return all, some or no child items depending on the size of the system. In which case, if
        /// <see cref="IsCollection"/> is true and <see cref="Items"/> is null then you will need to retrieve the child items.
        /// </remarks>
        [JsonProperty("items")]
        public virtual IEnumerable<HardwareTreeItem> Items { get; set; } = Array.Empty<HardwareTreeItem>();

        /// <summary>
        /// Provides additional information on a hardware type, for example, if a door is a POE or wireless.
        /// </summary>
        [JsonProperty("subType")]
        public virtual string SubType { get; set; }

        /// <summary>
        /// If the hardware item has child items
        /// </summary>
        /// <example>
        /// Mercury doors will have input and outpoint points, and readers.
        /// </example>
        [JsonProperty("isCollection")]
        public virtual bool IsCollection { get; set; }

        public string Index => $"{UniqueKey}{ParentKey}";

        public virtual string MarkupText
        {
            get
            {
                if (!string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Description))
                    return $"{GetImage()} {Address}: {Description}";

                if (!string.IsNullOrEmpty(Address))
                    return $"{GetImage()} {Address}";

                if (!string.IsNullOrEmpty(Description))
                    return $"{GetImage()} {Description}";

                return "Unknown hardware type";
            }
        }

        private string GetImage()
        {
            if (!Equals(Console.OutputEncoding, Encoding.UTF8))
                return string.Empty;

            return Type switch
            {
                // Mercury
                "0:Channel" => Emoji.Known.Television,
                "0:Controller" => Emoji.Known.FileCabinet,
                "0:Door" => Emoji.Known.Door,
                "0:Doors" => Emoji.Known.Door,
                "0:Driver" => Emoji.Known.DesktopComputer,
                "0:Elevator" => Emoji.Known.Elevator,
                "0:Elevators" => Emoji.Known.Elevator,
                "0:Input" => Emoji.Known.RedCircle,
                "0:MPGs" => Emoji.Known.HollowRedCircle,
                "0:MonitorPointGroups" => Emoji.Known.HollowRedCircle,
                "0:Mpg" => Emoji.Known.HollowRedCircle,
                "0:Output" => Emoji.Known.BlueCircle,
                "0:Reader" => Emoji.Known.ClosedBook,
                "0:SecuredArea" => Emoji.Known.HollowRedCircle,
                "0:SubController" => Emoji.Known.FileFolder,
                "0:TimeSchedule" => Emoji.Known.AlarmClock,
                "0:TimeSchedules" => Emoji.Known.AlarmClock,

                // Axis
                "5:Controller" => Emoji.Known.FileCabinet,
                "5:Door" => Emoji.Known.Door,
                "5:Driver" => Emoji.Known.DesktopComputer,

                // Assa
                "1:Controller" => Emoji.Known.FileCabinet,
                "1:Input" => Emoji.Known.RedCircle,
                "1:Output" => Emoji.Known.BlueCircle,
                "1:Driver" => Emoji.Known.DesktopComputer,
                "1:Door" => Emoji.Known.Door,
                "1:Doors" => Emoji.Known.Door,

                // Isonas
                "9:Doors" => Emoji.Known.Door,
                "9:Door" => Emoji.Known.Door,
                "9:Controller" => Emoji.Known.FileCabinet,
                "9:Driver" => Emoji.Known.DesktopComputer,

                // Engage
                "13:Door" => Emoji.Known.Door,
                "13:Doors" => Emoji.Known.Door,
                "13:Driver" => Emoji.Known.DesktopComputer,
                "13:Gateway" => Emoji.Known.GlobeShowingAmericas,
                "13:Controller" => Emoji.Known.FileCabinet,

                _ => ""
            };
        }
    }
}