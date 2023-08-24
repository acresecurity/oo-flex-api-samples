
namespace Flex.DataObjects.Settings
{
    public partial class InputConversionTable
    {
        public LowerResistance LowerResistance { get; } = new();

        public ReportPriority ReportPriority { get; } = new();

        public StatusCode StatusCode { get; } = new();

        public UpperResistance UpperResistance { get; } = new();

        public int? Number0 { get; set; }

        public int? Number1 { get; set; }

        public int? Priority0 { get; set; }

        public int? Priority1 { get; set; }

        public int? Status0 { get; set; }

        public int? Status1 { get; set; }

        public int? InputConversionTableQuantity { get; set; }
    }
}
