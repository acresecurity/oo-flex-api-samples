
namespace Flex.DataObjects.Settings.Station
{
    public partial class AlarmGrid
    {
        public BackgroundAck BackgroundAck { get; } = new();

        public BackgroundAlarm BackgroundAlarm { get; } = new();

        public BackgroundClear BackgroundClear { get; } = new();

        public BackgroundTrouble BackgroundTrouble { get; } = new();

        public BlinkAck BlinkAck { get; } = new();

        public BlinkAlarm BlinkAlarm { get; } = new();

        public BlinkClear BlinkClear { get; } = new();

        public BlinkTrouble BlinkTrouble { get; } = new();

        public ForegroundAck ForegroundAck { get; } = new();

        public ForegroundAlarm ForegroundAlarm { get; } = new();

        public ForegroundClear ForegroundClear { get; } = new();

        public ForegroundTrouble ForegroundTrouble { get; } = new();

        public string FontName { get; set; }

        public int? FontSize { get; set; }
    }
}
