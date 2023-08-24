
namespace Flex.DataObjects.Settings.Station
{
    public partial class StationSettings
    {
        public int? AlarmCounter { get; set; }

        public bool? AllowLegacyAxlGroups { get; set; }

        public int? AppendAddressToDescription { get; set; }

        public int? AppendAddressToDescriptionIndex { get; set; }

        public bool? AppToFrontOnAlarm { get; set; }

        public int? AutoEmailEnabled { get; set; }

        public bool? AutoExpandHardwareTreeOnStartup { get; set; }

        public bool? AutoRefreshBadgeImage { get; set; }

        public bool? AutoSaveEventFilter { get; set; }

        public string Company { get; set; }

        public bool? CyclePhotoIDWindows { get; set; }

        public int? DefaultActivation { get; set; }

        public bool? DisableAutoGrid { get; set; }

        public int? DisallowExit { get; set; }

        public int? DisallowMinimization { get; set; }

        public bool? DisplayPrompt { get; set; }

        public bool? DocTabsOnBottom { get; set; }

        public int? DriverType { get; set; }

        public int EmailEnabled { get; set; }

        public int? EmbeddedVideo { get; set; }

        public int? EnablePagePop { get; set; }

        public int? GlobalizationSettings { get; set; }

        public int? GraphicsBlinkRate { get; set; }

        public int? HardwardSortOrder { get; set; }

        public int? HardwareRefreshFromClientUpdates { get; set; }

        public bool? HardwareTreeDblClickExpand { get; set; }

        public int? HideDoodObjectOnOutputTab { get; set; }

        public int? HideDoorObjectOnInputTab { get; set; }

        public bool? HidePinPassword { get; set; }

        public string HomePage { get; set; }

        public bool? HotStampDelink { get; set; }

        public int? InactiveWarningTimeout { get; set; }

        public int? InactivityLimit { get; set; }

        public int? InformationBarBackColor { get; set; }

        public int? InformationBarTextColor { get; set; }

        public int? InitialDisplay { get; set; }

        public int? LastSavedCameraType { get; set; }

        public int? MasterCameraStation { get; set; }

        public int? MaximizePagesWhenOpening { get; set; }

        public int? PelcoScanned { get; set; }

        public bool? PreventAlarmClose { get; set; }

        public int? ShortcutBarTextColor { get; set; }

        public int? ShowAlarmsGridWhenOffline { get; set; }

        public bool? ShowAxlDoorDescription { get; set; }

        public int? Site { get; set; }

        public int? SizeTooltipHeadings { get; set; }

        public int? SSPDebugCaptureOption { get; set; }

        public int? StationLevel { get; set; }

        public int? SummaryPollRate { get; set; }

        public int? TextColor { get; set; }

        public int? TraceHistoryPrintToSize { get; set; }

        public int? TransactionsQuantity { get; set; }

        public bool? UseDnsReports { get; set; }

        public int? UseNTAuth { get; set; }

        public int? UserPCProx { get; set; }

        public int? UseSMTPEmail { get; set; }

        public bool? UseTabbedInterface { get; set; }

        public bool? UseTrayIcon { get; set; }

        public int? UseVideoTooltips { get; set; }

        public bool? UseWinTabSignature { get; set; }

        public int? DoNotLoadHomepages { get; set; }

        public Alarm Alarm { get; } = new();
        public AlarmGrid AlarmGrid { get; } = new();
        public Debug Debug { get; } = new();
        public SortOrder OtherSortOrder { get; } = new();
        public Directory Directory { get; } = new();
        public Email Email { get; } = new();
        public Isonas Isonas { get; } = new();
        public StationPersonnel Personnel { get; } = new();
        public PhotoRecall PhotoRecall { get; } = new();
        public StationSituationManager SituationManager { get; } = new();
        public StationBadging StationBadging { get; } = new();
        public StationSite StationSite { get; } = new();
        public ToolTip ToolTip { get; } = new();
        public Video Video { get; } = new();

        public HardwareTabsSettingsConfiguration HardwareTabsSettingsConfiguration { get; set; } = new();
    }
}
