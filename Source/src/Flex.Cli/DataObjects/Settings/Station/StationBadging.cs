
namespace Flex.DataObjects.Settings.Station
{
    public partial class StationBadging
    {
        public string Printer { get; set; }

        public string PhotoExtension { get; set; }

        public int? BitsOnCard { get; set; }

        public int? CardBits { get; set; }

        public int? CardFormatType { get; set; }

        public int? CardOffset { get; set; }

        public int? DebugLevel { get; set; }

        public int? DebugType { get; set; }

        public int? FacilityBits { get; set; }

        public int? FacilityOffset { get; set; }

        public int? HookSafeCalls { get; set; }

        public int? IClassPort { get; set; }

        public int? IClassTimeout { get; set; }

        public int? PrinterHopper { get; set; }

        public int? PrinterStation { get; set; }

        public int? PrinterType { get; set; }

        public int? ProxPort { get; set; }

        public int? ProxTimeout { get; set; }

        public int? ReaderType { get; set; }

        public int? SeiwgPort { get; set; }

        public int? SmartCardPort { get; set; }

        public int? SmartCardTimeout { get; set; }

        public int? UseExternalTwain { get; set; }

        public int? CropType { get; set; }

        public int? CropWidth { get; set; }

        public int? CropHeight { get; set; }

        public Epad Epad { get; } = new();
    }
}
