
namespace Flex.DataObjects.Settings.Station
{
    public partial class SiteDriver
    {
        public bool? UseDnaMasking { get; set; }

        public bool? UseDnaAlarmEscalation { get; set; }

        public int? BumpCounter { get; set; }

        public bool? SslEnabled { get; set; }

        public int? PingType { get; set; }

        public int? PollingTimer { get; set; }

        public int? PollingTimerLow { get; set; }

        public int? PingTimer { get; set; }

        public int? PollDelay { get; set; }

        public string SiteName { get; set; }

        public int? BumpFlagType { get; set; }

        public int? TransactionsCacheSize { get; set; }

        public bool? DownloadOnDemand { get; set; }

        public int? Site { get; set; }

        public int? DebugWindows { get; set; }

        public int? AlarmsQueueType { get; set; }

        public int? CommandsTimer { get; set; }

        public int? DownloadTimer { get; set; }

        public int? Peripheral { get; set; }

        public string SmtpAuth { get; set; }

        public string SmtpServer { get; set; }

        public string FromEmailAddress { get; set; }

        public int? SmtpAuthType { get; set; }

        public int? SmtpPort { get; set; }

        public bool? SmtpUseTls { get; set; }

        public bool? SmtpLogging { get; set; }

        public int? SmtpTlsStartMethod { get; set; }

        public bool? UseDnaWeb { get; set; }

        public bool? EnableRexPoll { get; set; }

        public bool? IgnoreTimeAttendance { get; set; }

        public int? EventReceiverTcpPort { get; set; }

        public int? ExternalTcpPort { get; set; }
    }
}
