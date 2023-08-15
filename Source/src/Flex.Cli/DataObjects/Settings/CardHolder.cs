
namespace Flex.DataObjects.Settings
{
    public partial class Cardholder
    {
        public bool? AllowDuplicateCardNumbers { get; set; }

        public bool? AllowDuplicatePins { get; set; }

        public int? PersonnelCardQuantity { get; set; }

        public int? PersonnelFieldPrompts { get; set; }

        public int? PersonnelPictureQuantity { get; set; }

        public bool? AllowBlankEmployeeNumber { get; set; }

        public bool? AllowBlankEmployeeId { get; set; }

        public bool? EnforceUniqueEmployeeNumber { get; set; }

        public bool? EnforceUniqueEmployeeId { get; set; }

        public CardholderSelection CardholderSelection { get; } = new();
    }
}
