
namespace Flex.DataObjects.Settings.Station
{
    public partial class StationSite
    {
        public int? HighPriorityReportsMask { get; set; }

        public int? HttpEnabled { get; set; }

        public int? LowPriorityReportMask { get; set; }

        public int? PingSiteTimer { get; set; }

        public int? SiteDatabaseSetup { get; set; }

        public int? StationDatabaseSetup { get; set; }

        public string StationName { get; set; }

        public int? VisualStyle { get; set; }

        public SiteDriver SiteDriver { get; } = new();
    }
}
