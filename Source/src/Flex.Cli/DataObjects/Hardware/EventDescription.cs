
namespace Flex.DataObjects.Hardware
{
    public partial class EventDescription
    {
        public string Description { get; set; }

        public bool Display { get; set; }

        public int GroupIndex { get; set; }

        public int UniqueId { get; set; }

        public bool IsAlarm { get; set; }

        public bool Log { get; set; }

        public int Priority { get; set; }

        public bool ReturnToNormal { get; set; }
    }
}