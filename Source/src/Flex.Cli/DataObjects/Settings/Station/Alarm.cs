namespace Flex.DataObjects.Settings.Station
{
    public partial class Alarm
    {
        public AlarmAttribute AlarmAttribute { get; } = new();

        public AlarmLoop AlarmLoop { get; } = new();

        public AlarmSound AlarmSound { get; } = new();
    }
}
