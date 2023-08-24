using Flex.DataObjects.Cardholder;
using Flex.DataObjects.Hardware;
using Flex.DataObjects.Settings.Station;

namespace Flex.DataObjects.Settings
{
    public partial class Settings
    {
        public int? AllowNonTenantSSPEvents { get; set; }

        public bool? AssignAccessLevelsToInactiveCards { get; set; }

        public int? AutoAlarmClear { get; set; }

        public bool? AxlGroupToCards { get; set; }

        public bool? BadgingRenameCropped { get; set; }

        public bool? CardNumberCreatedExternally { get; set; }

        public int? CustomRepeatableQuery1Defined { get; set; }

        public int? CustomRepeatableQuery2Defined { get; set; }

        public int? CustomRepeatableQuery3Defined { get; set; }

        public int? CustomRepeatableQuery4Defined { get; set; }

        public int? CustomRepeatableQuery5Defined { get; set; }

        public int? CustomRepeatableQuery6Defined { get; set; }

        public int? CustomRepeatableQuery7Defined { get; set; }

        public int? CustomRepeatableQuery8Defined { get; set; }

        public int? CustomRepeatableQuery9Defined { get; set; }

        public bool? ForceFacilityCodeUse { get; set; }

        public bool? HideTenantPersonData { get; set; }

        public string LastBatchFolder { get; set; }

        public int? MaximumRecordsSelectedInReport { get; set; }

        public int? NTAuthType { get; set; }

        public bool? OpenAllCardsUponRepeatableQuery { get; set; }


        public bool? PIVCleanupCertificates { get; set; }

        public bool? PIVPreferVerifiedPIN { get; set; }

        public bool? PredefinedDispatchers { get; set; }

        public bool? PromptWhenAddNewCardholderToGroup { get; set; }

        public bool? ProxMode { get; set; }

        public string SupremaEnrollmentProgram { get; set; }

        public int? TenantLongDisplay { get; set; }

        public int? TraceHistoryEmployeeId { get; set; }

        public int? TraceHistorySSN { get; set; }

        public bool? UseDatabaseForDirectCommands { get; set; }

        public bool? UseOperatorSSPLists { get; set; }

        public int? UsesCustomCardholderType { get; set; }

        public bool? UseSituationManager { get; set; }

        public bool? UseSituationManagerHostMacro { get; set; }

        public int? UseStrongPasswords { get; set; }

        public bool? UseUniquePasscode { get; set; }

        public bool? VideoExportAndEmail { get; set; }

        public int? BadgingAllowUpdateOnProxCard { get; set; }

        public string CameraEventsSupportedDVR { get; set; }

        public bool? AllowHBMOnToolbar { get; set; }

        public int? AllowNonTenantSspEvents { get; set; }

        public bool? TenantArchiveProfile { get; set; }

        public Assa Assa { get; set; } = new();

        public Badging Badging { get; set; } = new();

        public Bosch Bosch { get; set; } = new();

        public CustomRepeatableQuery1 CRQ1 { get; set; } = new();

        public CustomRepeatableQuery2 CRQ2 { get; set; } = new();

        public CustomRepeatableQuery3 CRQ3 { get; set; } = new();

        public CustomRepeatableQuery4 CRQ4 { get; set; } = new();

        public CustomRepeatableQuery5 CRQ5 { get; set; } = new();

        public CustomRepeatableQuery6 CRQ6 { get; set; } = new();

        public CustomRepeatableQuery7 CRQ7 { get; set; } = new();

        public CustomRepeatableQuery8 CRQ8 { get; set; } = new();

        public CustomRepeatableQuery9 CRQ9 { get; set; } = new();

        public CustomAccessLevel CustomAccessLevel { get; set; } = new();

        public CustomPersonField CustomPersonField { get; set; } = new();

        public Filtering Filtering { get; set; } = new();

        public HardwareDescription HardwareDescription { get; set; } = new();

        public InputConversionTable InputConversionTable { get; set; } = new();

        public NewCredential NewCredential { get; set; } = new();

        public Cardholder Cardholder { get; set; } = new();

        public OperatorSettings Operator { get; set; } = new();

        public Segregation Segregation { get; set; } = new();

        public SituationManager SituationManager { get; } = new();

        public StationSettings Station { get; set; } = new();

        public Dictionary<int, Tenant> Tenants { get; set; } = new();

        public List<CredentialType> CredentialTypes { get; set; } = new();

        public List<DisabledReason> DisableReasons { get; set; } = new();

        public List<Acknowledgments> Acknowledgments { get; set; } = new();

        public List<CardMode> CardModes { get; set; } = new();

        public List<CardholderType> CardholderTypes { get; set; } = new();

        public PhotoNamingConfiguration PhotoNamingConfiguration { get; set; } = new();

        public string AuthorityUrl { get; set; }

        public ulong MaxCardSize { get; set; }
    }
}
