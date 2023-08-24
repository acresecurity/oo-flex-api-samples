
namespace Flex.DataObjects.Settings
{
    public partial class Badging
    {
        public int? DeactivateCards { get; set; }

        public int? EnableUpdateOnProxCard { get; set; }

        public int? IdBadgingCamType { get; set; }

        public int? TwainSource { get; set; }

        public int? UsingLumenera { get; set; }

        public IssueCode IssueCode { get; } = new();
    }
}
