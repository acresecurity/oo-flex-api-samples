
namespace Flex.DataObjects.Settings
{
    public partial class Tenant
    {
        public int? PersonnelCardQuantity { get; set; }

        public int? PersonnelPictureQuantity { get; set; }

        public bool? PersonnelFieldPrompts { get; set; }

        public int? DefaultActivation { get; set; }

        public int? DefaultFacilityCode { get; set; }

        public int? DefaultCodeMode { get; set; }

        public bool? DefaultToDeactivateNewCard { get; set; }

        public CustomPersonField CustomPersonField { get; set; } = new ();
    }
}
