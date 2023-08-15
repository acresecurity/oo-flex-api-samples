
namespace Flex.DataObjects.Settings
{
    public partial class Bosch
    {
        public bool? AutoPasscode { get; set; }

        public int? NameFormat { get; set; }

        public int? PasscodeLength { get; set; }

        public bool? PasscodeMode { get; set; }

        public int? StartUserId { get; set; }

        public int? ReceivedHostPort { get; set; }

        public bool? LimitBoschWhoDoesNotHaveAccessToBoschOnly { get; set; }
    }
}
