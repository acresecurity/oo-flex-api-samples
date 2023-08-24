
namespace Flex.DataObjects.Settings
{
    public partial class NewCredential
    {
        public bool? IncrementIssueCode { get; set; }

        /// <summary>
        /// Duplicate active credential properties when adding a new credential
        /// </summary>
        public bool? DuplicateActiveCredential { get; set; }

        public bool? DeactivateExistingCards { get; set; }

        public bool? DefaultToDeactivate { get; set; }

        public int? DefaultActivation { get; set; }

        public int? DefaultActivationCardType { get; set; }

        public int? DefaultFacilityCode { get; set; }

        public int? DefaultCardMode { get; set; }
    }
}
