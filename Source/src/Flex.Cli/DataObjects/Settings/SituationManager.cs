
namespace Flex.DataObjects.Settings
{
    public partial class SituationManager
    {
        public bool? UseSituationManager { get; set; }

        public int? UseHostMacro { get; set; }

        public int? ThreatLevel { get; set; }

        public ManagerDescription ManagerDescriptions { get; } = new ();

        public ManagerText ManagerText { get; } = new();
    }
}
