#if FLEXAPI
using System.ComponentModel;
using Flex.Privileges.Attributes;
using Flex.Privileges.Description_Lookups;
using Flex.Privileges.Enumerators;
#endif
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
#if FLEXAPI
namespace Flex.Privileges
#else
namespace Common
#endif
{

    class Assets
    {
        public const string AccessArea = "AccessArea.png";
        public const string AccessLevels = "AccessLevels.png";
        public const string ACM = "ACM.png";
        public const string ActID = "ActID.png";
        public const string Actions = "Actions.png";
        public const string AlarmRouting = "AlarmRouting.png";
        public const string Alarms = "Alarms.png";
        public const string Bosch = "Bosch.png";
        public const string Check = "Check.png";
        public const string CheckBox = "CheckBox.png";
        public const string Controller = "Controller.png";
        public const string ControlPoint = "ControlPoint.png";
        public const string DirectCommands = "DirectCommands.png";
        public const string DVRsandCameras = "DVRsandCameras.png";
        public const string Edit = "Edit.png";
        public const string EngageIP = "EngageIP.png";
        public const string ExtHardware = "ExtHardware.png";
        public const string Filters = "Filters.png";
        public const string Gear = "Gear.png";
        public const string Graphics = "Graphics.png";
        public const string HIDOmniKey = "HIDOmniKey.png";
        public const string Holidays = "Holidays.png";
        public const string Isonas = "Isonas.png";
        public const string Kone = "Kone.png";
        public const string Miscellaneous = "Miscellaneous.png";
        public const string MonitorPoint = "MonitorPoint.png";
        public const string MPG = "MPG.png";
        public const string OperatingSettings = "OperatingSettings.png";
        public const string OperatorFilters = "OperatorFilters.png";
        public const string OperatorImportSettings = "OperatorImportSettings.png";
        public const string Personnel = "Personnel.png";
        public const string Reports = "Reports.png";
        public const string Schedules = "Schedules.png";
        public const string Schindler = "Schindler.png";
        public const string Settings = "Settings.png";
        public const string StationLevels = "StationLevels.png";
        public const string Stentofon = "Stentofon.png";
        public const string SubController = "SubController.png";
        public const string Tenants = "Tenants.png";
        public const string ThyssenKrupp = "ThyssenKrupp.png";
        public const string TimeSchedule = "TimeSchedule.png";
        public const string TreeUserPriveleges32 = "TreeUserPriveleges32.png";
        public const string TriggersAndMacros = "TriggersAndMacros.png";
        public const string UniversalDriver = "UniversalDriver.png";
        public const string Views = "Views.png";
    }

    [JsonConverter(typeof(StringEnumConverter), false)] // Prevent values from being converted to camel case
    public enum UserRights
    {
#if FLEXAPI
        [Ignore]
#endif
        NONE,
#if FLEXAPI
        [Offset(1, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Monitor Point", ControlType.CheckBox, Assets.MonitorPoint, Assets.Controller, 0, 0, 1)]
#endif
        ALLOWCTRLMP,
#if FLEXAPI
        [Offset(2, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Control Point", ControlType.CheckBox, Assets.ControlPoint, Assets.Controller, 0, 1, 1)]
#endif
        ALLOWCTRLCP,
#if FLEXAPI
        [Offset(3, 1)]
        [Description("Allow Precision Assignment")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 6)]
#endif
        ALLOWASSIGNRDR,
#if FLEXAPI
        [Offset(4, 1)]
        [Secondary(458, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Monitor Point", ControlType.ComboBox, Assets.MonitorPoint, Assets.Controller, 0, 0, 0)]
#endif
        ALLOWEDITMP,
#if FLEXAPI
        [Offset(5, 1)]
        [Secondary(459, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Control Point", ControlType.ComboBox, Assets.ControlPoint, Assets.Controller, 0, 1, 0)]
#endif
        ALLOWEDITCP,
#if FLEXAPI
        [Offset(6, 1)]
        [Secondary(460, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Door\\Normal", ControlType.ComboBox, Assets.ACM, Assets.Controller, 0, 2, 5, 0)]
#endif
        ALLOWEDITRDR,
#if FLEXAPI
        [Offset(7, 1)]
        [Description("Priority: 1")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 0)]
#endif
        ALLOWACK1,
#if FLEXAPI
        [Offset(8, 1)]
        [Description("Priority: 2")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 1)]
#endif
        ALLOWACK2,
#if FLEXAPI
        [Offset(9, 1)]
        [Description("Priority: 3")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 2)]
#endif
        ALLOWACK3,
#if FLEXAPI
        [Offset(10, 1)]
        [Description("Priority: 4")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 3)]
#endif
        ALLOWACK4,
#if FLEXAPI
        [Offset(11, 1)]
        [Description("Priority: 5")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 4)]
#endif
        ALLOWACK5,
#if FLEXAPI
        [Offset(12, 1)]
        [Description("Priority: 6")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 5)]
#endif
        ALLOWACK6,
#if FLEXAPI
        [Offset(13, 1)]
        [Description("Priority: 7")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 6)]
#endif
        ALLOWACK7,
#if FLEXAPI
        [Offset(14, 1)]
        [Description("Priority: 8")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 7)]
#endif
        ALLOWACK8,
#if FLEXAPI
        [Offset(15, 1)]
        [Description("Priority: 9")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 8)]
#endif
        ALLOWACK9,
#if FLEXAPI
        [Offset(16, 1)]
        [Description("Priority: 10")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 9)]
#endif
        ALLOWACK10,
#if FLEXAPI
        [Offset(17, 1)]
        [Description("Priority: 11")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 10)]
#endif
        ALLOWACK11,
#if FLEXAPI
        [Offset(18, 1)]
        [Description("Priority: 12")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 11)]
#endif
        ALLOWACK12,
#if FLEXAPI
        [Offset(19, 1)]
        [Description("Priority: 13")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 12)]
#endif
        ALLOWACK13,
#if FLEXAPI
        [Offset(20, 1)]
        [Description("Priority: 14")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 13)]
#endif
        ALLOWACK14,
#if FLEXAPI
        [Offset(21, 1)]
        [Description("Priority: 15")]
        [Tree("Alarms\\Allow Acknowledge", ControlType.CheckBox, Assets.Alarms, 3, 4, 14)]
#endif
        ALLOWACK15,
#if FLEXAPI
        [Offset(37, 1)]
        [Description("Priority: 1")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 0)]
#endif
        RQRDISPTXT1,
#if FLEXAPI
        [Offset(38, 1)]
        [Description("Priority: 2")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 1)]
#endif
        RQRDISPTXT2,
#if FLEXAPI
        [Offset(39, 1)]
        [Description("Priority: 3")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 2)]
#endif
        RQRDISPTXT3,
#if FLEXAPI
        [Offset(40, 1)]
        [Description("Priority: 4")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 3)]
#endif
        RQRDISPTXT4,
#if FLEXAPI
        [Offset(41, 1)]
        [Description("Priority: 5")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 4)]
#endif
        RQRDISPTXT5,
#if FLEXAPI
        [Offset(42, 1)]
        [Description("Priority: 6")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 5)]
#endif
        RQRDISPTXT6,
#if FLEXAPI
        [Offset(43, 1)]
        [Description("Priority: 7")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 6)]
#endif
        RQRDISPTXT7,
#if FLEXAPI
        [Offset(44, 1)]
        [Description("Priority: 8")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 7)]
#endif
        RQRDISPTXT8,
#if FLEXAPI
        [Offset(45, 1)]
        [Description("Priority: 9")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 8)]
#endif
        RQRDISPTXT9,
#if FLEXAPI
        [Offset(46, 1)]
        [Description("Priority: 10")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 9)]
#endif
        RQRDISPTXT10,
#if FLEXAPI
        [Offset(47, 1)]
        [Description("Priority: 11")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 10)]
#endif
        RQRDISPTXT11,
#if FLEXAPI
        [Offset(48, 1)]
        [Description("Priority: 12")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 11)]
#endif
        RQRDISPTXT12,
#if FLEXAPI
        [Offset(49, 1)]
        [Description("Priority: 13")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 12)]
#endif
        RQRDISPTXT13,
#if FLEXAPI
        [Offset(50, 1)]
        [Description("Priority: 14")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 13)]
#endif
        RQRDISPTXT14,
#if FLEXAPI
        [Offset(51, 1)]
        [Description("Priority: 15")]
        [Tree("Alarms\\Require Dispatch Text", ControlType.CheckBox, Assets.Alarms, 3, 5, 14)]
#endif
        RQRDISPTXT15,
#if FLEXAPI
        [Offset(52, 1)]
        [Description("Download Configuration")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 10)]
#endif
        ALLOWDOWNLOADH,
#if FLEXAPI
        [Offset(53, 1)]
        [Description("Priority: 1")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 0)]
#endif
        ALLOWPURGE1,
#if FLEXAPI
        [Offset(54, 1)]
        [Description("Priority: 2")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 1)]
#endif
        ALLOWPURGE2,
#if FLEXAPI
        [Offset(55, 1)]
        [Description("Priority: 3")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 2)]
#endif
        ALLOWPURGE3,
#if FLEXAPI
        [Offset(56, 1)]
        [Description("Priority: 4")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 3)]
#endif
        ALLOWPURGE4,
#if FLEXAPI
        [Offset(57, 1)]
        [Description("Priority: 5")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 4)]
#endif
        ALLOWPURGE5,
#if FLEXAPI
        [Offset(58, 1)]
        [Description("Priority: 6")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 5)]
#endif
        ALLOWPURGE6,
#if FLEXAPI
        [Offset(59, 1)]
        [Description("Priority: 7")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 6)]
#endif
        ALLOWPURGE7,
#if FLEXAPI
        [Offset(60, 1)]
        [Description("Priority: 8")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 7)]
#endif
        ALLOWPURGE8,
#if FLEXAPI
        [Offset(61, 1)]
        [Description("Priority: 9")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 8)]
#endif
        ALLOWPURGE9,
#if FLEXAPI
        [Offset(62, 1)]
        [Description("Priority: 10")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 9)]
#endif
        ALLOWPURGE10,
#if FLEXAPI
        [Offset(63, 1)]
        [Description("Priority: 11")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 10)]
#endif
        ALLOWPURGE11,
#if FLEXAPI
        [Offset(64, 1)]
        [Description("Priority: 12")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 11)]
#endif
        ALLOWPURGE12,
#if FLEXAPI
        [Offset(65, 1)]
        [Description("Priority: 13")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 12)]
#endif
        ALLOWPURGE13,
#if FLEXAPI
        [Offset(66, 1)]
        [Description("Priority: 14")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 13)]
#endif
        ALLOWPURGE14,
#if FLEXAPI
        [Offset(67, 1)]
        [Description("Priority: 15")]
        [Tree("Alarms\\Allow Dismiss Alarm", ControlType.CheckBox, Assets.Alarms, 3, 6, 14)]
#endif
        ALLOWPURGE15,
#if FLEXAPI
        [Offset(68, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Time Schedules", ControlType.CheckBox, Assets.TimeSchedule, Assets.Controller, 0, 3, 0)]
#endif
        AllowCtrlTz,
#if FLEXAPI
        [Offset(69, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Access Area", ControlType.CheckBox, Assets.AccessArea, Assets.Controller, 0, 5, 1)]
#endif
        AllowCtrlArea,
#if FLEXAPI
        [Offset(70, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\MPG", ControlType.CheckBox, Assets.MPG, Assets.Controller, 0, 6, 1)]
#endif
        AllowCtrlMpg,
#if FLEXAPI
        [Offset(71, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Triggers & Macros", ControlType.CheckBox, Assets.TriggersAndMacros, Assets.Controller, 0, 7, 1)]
#endif
        AllowCtrlMacro,
#if FLEXAPI
        [Offset(72, 1)]
        [Secondary(461, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Time Schedules\\Time Schedule", ControlType.ComboBox, Assets.TimeSchedule, Assets.Controller, 0, 3, 4, 0)]
#endif
        AllowEditTz,
#if FLEXAPI
        [Offset(73, 1)]
        [Secondary(462, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Access Area", ControlType.ComboBox, Assets.AccessArea, Assets.Controller, 0, 5, 0)]
#endif
        AllowEditArea,
#if FLEXAPI
        [Offset(74, 1)]
        [Secondary(463, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\MPG", ControlType.ComboBox, Assets.MPG, Assets.Controller, 0, 6, 0)]
#endif
        AllowEditMpg,
#if FLEXAPI
        [Offset(75, 1)]
        [Secondary(464, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Triggers & Macros", ControlType.ComboBox, Assets.TriggersAndMacros, Assets.Controller, 0, 7, 0)]
#endif
        AllowEditMacro,
#if FLEXAPI
        [Offset(76, 2)]
        [Description("Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 0)]
#endif
        CardField1,
#if FLEXAPI
        [Offset(78, 2)]
        [Description("Hot Stamp")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 1)]
#endif
        CardField2,
#if FLEXAPI
        [Offset(80, 2)]
        [Description("Issue Code")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 2)]
#endif
        CardField3,
#if FLEXAPI
        [Offset(82, 2)]
        [Description("P.I.N.")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 3)]
#endif
        CardField4,
#if FLEXAPI
        [Offset(84, 2)]
        [Description("Facility Code")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 4)]
#endif
        CardField5,
#if FLEXAPI
        [Offset(86, 2)]
        [Description("Trigger Code")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 5)]
#endif
        CardField6,
#if FLEXAPI
        [Offset(88, 2)]
        [Description("Activation")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 6)]
#endif
        CardField7,
#if FLEXAPI
        [Offset(90, 2)]
        [Description("Deactivation")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 7)]
#endif
        CardField8,
#if FLEXAPI
        [Offset(92, 2)]
        [Description("Vacation Start")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 8)]
#endif
        CardField9,
#if FLEXAPI
        [Offset(94, 2)]
        [Description("Upgrade Start")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 9)]
#endif
        CardField10,
#if FLEXAPI
        [Offset(96, 2)]
        [Description("APB Location")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 10)]
#endif
        CardField11,
#if FLEXAPI
        [Offset(98, 2)]
        [Description("Card Type")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 11)]
#endif
        CardField12,
#if FLEXAPI
        [Offset(100, 2)]
        [Description("Reason Disabled")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 12)]
#endif
        CardField13,
#if FLEXAPI
        [Offset(102, 2)]
        [Description("ADA Mode")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 13)]
#endif
        CardField14,
#if FLEXAPI
        [Offset(104, 2)]
        [Description("Activate Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 14)]
#endif
        CardField15,
#if FLEXAPI
        [Offset(106, 2)]
        [Description("Allow 1 Free APB Pass")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 15)]
#endif
        CardField16,
#if FLEXAPI
        [Offset(108, 2)]
        [Description("V.I.P. (APB Exempt)")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 16)]
#endif
        CardField17,
#if FLEXAPI
        [Offset(110, 2)]
        [Description("Alter Use Count")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 17)]
#endif
        CardField18,
#if FLEXAPI
        [Offset(112, 2)]
        [Description("Alter APB Location")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 18)]
#endif
        CardField19,
#if FLEXAPI
        [Offset(114, 2)]
        [Description("Use Limit")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 19)]
#endif
        CardField20,
#if FLEXAPI
        [Offset(116, 2)]
        [Description("Reset APB")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 20)]
#endif
        CardField21,
#if FLEXAPI
        [Offset(118, 2)]
        [Description("Trace History")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 21)]
#endif
        CardField22,
#if FLEXAPI
        [Offset(120, 2)]
        [Description("Access Levels")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 22)]
#endif
        CardField23,
#if FLEXAPI
        [Offset(122, 2)]
        [Description("PIN Exempt Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 23)]
#endif
        CardField24,
#if FLEXAPI
        [Offset(124, 2)]
        [Description("Last Used")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 24)]
#endif
        CardField25,
#if FLEXAPI
        [Offset(126, 2)]
        [Description("Host Macro")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 25)]
#endif
        CardField26,
#if FLEXAPI
        [Offset(128, 2)]
        [Description("Unique ID")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 0)]
#endif
        PersonnelField1,
#if FLEXAPI
        [Offset(130, 2)]
        [Description("First")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 1)]
#endif
        PersonnelField2,
#if FLEXAPI
        [Offset(132, 2)]
        [Description("Last")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 3)]
#endif
        PersonnelField3,
#if FLEXAPI
        [Offset(134, 2)]
        [Description("E-Mail")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 5)]
#endif
        PersonnelField4,
#if FLEXAPI
        [Offset(136, 2)]
        [Description("Location")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 7)]
#endif
        PersonnelField5,
#if FLEXAPI
        [Offset(138, 2)]
        [Description("Department")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 9)]
#endif
        PersonnelField6,
#if FLEXAPI
        [Offset(140, 2)]
        [Description("Site")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 11)]
#endif
        PersonnelField7,
#if FLEXAPI
        [Offset(142, 2)]
        [Description("Title")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 13)]
#endif
        PersonnelField8,
#if FLEXAPI
        [Offset(144, 2)]
        [Description("Work Phone")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 15)]
#endif
        PersonnelField9,
#if FLEXAPI
        [Offset(146, 2)]
        [Description("Employee #")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 16)]
#endif
        PersonnelField10,
#if FLEXAPI
        [Offset(148, 2)]
        [Description("Company")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 17)]
#endif
        PersonnelField11,
#if FLEXAPI
        [Offset(150, 2)]
        [Description("Edit (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 18)]
#endif
        PersonnelField12,
#if FLEXAPI
        [Offset(152, 2)]
        [Description("Address1 (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 19)]
#endif
        PersonnelField13,
#if FLEXAPI
        [Offset(154, 2)]
        [Description("Address2 (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 20)]
#endif
        PersonnelField14,
#if FLEXAPI
        [Offset(156, 2)]
        [Description("City (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 21)]
#endif
        PersonnelField15,
#if FLEXAPI
        [Offset(158, 2)]
        [Description("State/Prov (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 22)]
#endif
        PersonnelField16,
#if FLEXAPI
        [Offset(160, 2)]
        [Description("Country (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 23)]
#endif
        PersonnelField17,
#if FLEXAPI
        [Offset(162, 2)]
        [Description("Zip (Company)")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 24)]
#endif
        PersonnelField18,
#if FLEXAPI
        [Offset(164, 2)]
        [Description("Employee Photo 1")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 25)]
#endif
        PersonnelField19,
#if FLEXAPI
        [Offset(166, 2)]
        [Description("Employee Photo 2")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 26)]
#endif
        PersonnelField20,
#if FLEXAPI
        [Offset(168, 2)]
        [Description("Employee Photo 3")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 27)]
#endif
        PersonnelField21,
#if FLEXAPI
        [Offset(170, 2)]
        [Description("Employee Photo 4")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 28)]
#endif
        PersonnelField22,
#if FLEXAPI
        [Offset(172, 2)]
        [Description("Address 1")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 29)]
#endif
        PersonnelField23,
#if FLEXAPI
        [Offset(174, 2)]
        [Description("Address 2")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 30)]
#endif
        PersonnelField24,
#if FLEXAPI
        [Offset(176, 2)]
        [Description("City")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 31)]
#endif
        PersonnelField25,
#if FLEXAPI
        [Offset(178, 2)]
        [Description("State/Province")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 32)]
#endif
        PersonnelField26,
#if FLEXAPI
        [Offset(180, 2)]
        [Description("Country")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 33)]
#endif
        PersonnelField27,
#if FLEXAPI
        [Offset(182, 2)]
        [Description("Home Phone")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 34)]
#endif
        PersonnelField28,
#if FLEXAPI
        [Offset(184, 2)]
        [Description("Zip")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 35)]
#endif
        PersonnelField29,
#if FLEXAPI
        [Offset(186, 2)]
        [Description("Employee ID")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 36)]
#endif
        PersonnelField30,
#if FLEXAPI
        [Offset(188, 2)]
        [Description("DL #")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 37)]
#endif
        PersonnelField31,
#if FLEXAPI
        [Offset(190, 2)]
        [Description("Other Personal Information")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 38)]
#endif
        PersonnelField32,
#if FLEXAPI
        [Offset(192, 2)]
        [Description("Hire Date")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 39)]
#endif
        PersonnelField33,
#if FLEXAPI
        [Offset(194, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 0", Default = "Custom String 1")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 40)]
#endif
        PersonnelField34,
#if FLEXAPI
        [Offset(196, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 1", Default = "Custom String 2")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 41)]
#endif
        PersonnelField35,
#if FLEXAPI
        [Offset(198, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 2", Default = "Custom String 3")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 42)]
#endif
        PersonnelField36,
#if FLEXAPI
        [Offset(200, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 3", Default = "Custom Value 1")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 56)]
#endif
        PersonnelField37,
#if FLEXAPI
        [Offset(202, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 4", Default = "Custom Value 2")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 57)]
#endif
        PersonnelField38,
#if FLEXAPI
        [Offset(204, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 5", Default = "Custom Value 3")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 58)]
#endif
        PersonnelField39,
#if FLEXAPI
        [Offset(206, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 0)]
#endif
        PersonnelField40,
#if FLEXAPI
        [Offset(208, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 1)]
#endif
        PersonnelField41,
#if FLEXAPI
        [Offset(210, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 2)]
#endif
        PersonnelField42,
#if FLEXAPI
        [Offset(212, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 3)]
#endif
        PersonnelField43,
#if FLEXAPI
        [Offset(214, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 4)]
#endif
        PersonnelField44,
#if FLEXAPI
        [Offset(216, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 5)]
#endif
        PersonnelField45,
#if FLEXAPI
        [Offset(218, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 6)]
#endif
        PersonnelField46,
#if FLEXAPI
        [Offset(220, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 7)]
#endif
        PersonnelField47,
#if FLEXAPI
        [Offset(222, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 8)]
#endif
        PersonnelField48,
#if FLEXAPI
        [Offset(224, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 9)]
#endif
        PersonnelField49,
#if FLEXAPI
        [Offset(226, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 10)]
#endif
        PersonnelField50,
#if FLEXAPI
        [Offset(228, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 11)]
#endif
        PersonnelField51,
#if FLEXAPI
        [Offset(230, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 12)]
#endif
        PersonnelField52,
#if FLEXAPI
        [Offset(232, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 13)]
#endif
        PersonnelField53,
#if FLEXAPI
        [Offset(234, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 14)]
#endif
        PersonnelField54,
#if FLEXAPI
        [Offset(236, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 15)]
#endif
        PersonnelField55,
#if FLEXAPI
        [Offset(238, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 16)]
#endif
        PersonnelField56,
#if FLEXAPI
        [Offset(240, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 17)]
#endif
        PersonnelField57,
#if FLEXAPI
        [Offset(242, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 18)]
#endif
        PersonnelField58,
#if FLEXAPI
        [Offset(244, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 19)]
#endif
        PersonnelField59,
#if FLEXAPI
        [Offset(246, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 20)]
#endif
        PersonnelField60,
#if FLEXAPI
        [Offset(248, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 21)]
#endif
        PersonnelField61,
#if FLEXAPI
        [Offset(250, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 22)]
#endif
        PersonnelField62,
#if FLEXAPI
        [Offset(252, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 23)]
#endif
        PersonnelField63,
#if FLEXAPI
        [Offset(254, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 24)]
#endif
        PersonnelField64,
#if FLEXAPI
        [Offset(310, 1)]
        [Description("Download Personnel")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 8)]
#endif
        ALLOWDOWNLOADP,
#if FLEXAPI
        [Offset(311, 1)]
        [Description("Reload Firmware")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 4)]
#endif
        HhdReloadFirmware,
#if FLEXAPI
        [Offset(312, 1)]
        [Description("Card Formats")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 8)]
#endif
        HdwCardFormats,
#if FLEXAPI
        [Offset(313, 1)]
        [Description("Calculate Memory")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 7)]
#endif
        HdwCalculateMemory,
#if FLEXAPI
        [Offset(314, 1)]
        [Description("Set Controller Time")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 5)]
#endif
        HdwSetTime,
#if FLEXAPI
        [Offset(317, 1)]
        [Description("Set Card Flags")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 7)]
#endif
        SetCardFlags,
#if FLEXAPI
        [Offset(318, 1)]
        [Description("View Photos")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 6)]
#endif
        PersonnelViewPhotos,
#if FLEXAPI
        [Offset(360, 1)]
        [Description("Add Cardholder")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 0)]
#endif
        ADDPERSONNEL,
#if FLEXAPI
        [Offset(361, 1)]
        [Description("Remove Cardholder")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 1)]
#endif
        DELETEPERSONNEL,
#if FLEXAPI
        [Offset(362, 1)]
        [Description("Add Card")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 2)]
#endif
        ADDCARD,
#if FLEXAPI
        [Offset(363, 1)]
        [Description("Remove Card")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 3)]
#endif
        DELETECARD,
#if FLEXAPI
        [Offset(364, 1)]
        [Description("Deactivate Card")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 4)]
#endif
        DEACTIVATECARD,
#if FLEXAPI
        [Offset(365, 1)]
        [Description("Reset Hardware")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 1)]
#endif
        ALLOWRESET,
#if FLEXAPI
        [Offset(366, 1)]
        [Description("Card # View")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 0)]
#endif
        PERSONNELTABA,
#if FLEXAPI
        [Offset(367, 1)]
        [Description("Name View")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 1)]
#endif
        PERSONNELTABB,
#if FLEXAPI
        [Offset(368, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  1", Default = "Custom Tab 1")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 2)]
#endif
        PERSONNELTAB1,
#if FLEXAPI
        [Offset(369, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  2", Default = "Custom Tab 2")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 3)]
#endif
        PERSONNELTAB2,
#if FLEXAPI
        [Offset(370, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  3", Default = "Custom Tab 3")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 4)]
#endif
        PERSONNELTAB3,
#if FLEXAPI
        [Offset(371, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  4", Default = "Custom Tab 4")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 5)]
#endif
        PERSONNELTAB4,
#if FLEXAPI
        [Offset(372, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  5", Default = "Custom Tab 5")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 6)]
#endif
        PERSONNELTAB5,
#if FLEXAPI
        [Offset(373, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  6", Default = "Custom Tab 6")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 7)]
#endif
        PERSONNELTAB6,
#if FLEXAPI
        [Offset(374, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  7", Default = "Custom Tab 7")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 8)]
#endif
        PERSONNELTAB7,
#if FLEXAPI
        [Offset(375, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Personnel Selection String  8", Default = "Custom Tab 8")]
        [Tree("Personnel\\Personnel View (Tabs)", ControlType.CheckBox, Assets.Personnel, 2, 1, 9)]
#endif
        PERSONNELTAB8,
#if FLEXAPI
        [Offset(376, 1)]
        [Secondary(465, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Controller", ControlType.ComboBox, Assets.Controller, 0, 8, 0)]
#endif
        AllowEditSSP,
#if FLEXAPI
        [Offset(377, 1)]
        [Secondary(466, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Sub-controller", ControlType.ComboBox, Assets.SubController, Assets.Controller, 0, 9, 0)]
#endif
        AllowEditSIO,
#if FLEXAPI
        [Offset(378, 1)]
        [Description("Remove Controller")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 9)]
#endif
        AllowDeleteSSP,
#if FLEXAPI
        [Offset(379, 1)]
        [Description("Remove Sub-controller")]
        [Tree("Hardware\\Sub-controller", ControlType.CheckBox, Assets.SubController, Assets.Controller, 0, 9, 1)]
#endif
        AllowDeleteSIO,
#if FLEXAPI
        [Offset(380, 1)]
        [Secondary(469, 1)]
        [Description("Edit Site")]
        [Tree("Hardware\\Miscellaneous", ControlType.ComboBox, Assets.Miscellaneous, Assets.Controller, 0, 11, 0)]
#endif
        ALLOWEDITSITE,
#if FLEXAPI
        [Offset(381, 1)]
        [Secondary(866, 1)]
        [Description("Allow Edit Camera Properties")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.ComboBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 1)]
#endif
        ALLOWEDITCAMERA,
#if FLEXAPI
        [Offset(382, 1)]
        [Secondary(468, 1)]
        [Description("Edit Channel")]
        [Tree("Hardware\\Miscellaneous", ControlType.ComboBox, Assets.Miscellaneous, Assets.Controller, 0, 11, 1)]
#endif
        ALLOWEDITCHANNEL,
#if FLEXAPI
        [Offset(383, 1)]
        [Secondary(470, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Access Levels", ControlType.ComboBox, Assets.AccessLevels, 1, 0)]
#endif
        AllowEditAxsLvl,
#if FLEXAPI
        [Offset(385, 1)]
        [Description("Access Level Descriptions")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 0)]
#endif
        ACCESSLEVELS_ACCESSLEVELDESCRIPTIONS_R,
#if FLEXAPI
        [Offset(386, 1)]
        [Description("Access Levels By SSP")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 1)]
#endif
        ACCESSLEVELS_ACCESSLEVELS_R,
#if FLEXAPI
        [Offset(388, 1)]
        [Description("Acknowledged Alarms")]
        [Tree("Reports\\Alarms", ControlType.CheckBox, Assets.Reports, 4, 1, 1)]
#endif
        AMTREPORTS_ACKNOWLEDGEDALARMS_R,
#if FLEXAPI
        [Offset(389, 1)]
        [Description("Alarm History")]
        [Tree("Reports\\Alarms", ControlType.CheckBox, Assets.Reports, 4, 1, 0)]
#endif
        AMTREPORTS_ALARMSREPORT_R,
#if FLEXAPI
        [Offset(390, 1)]
        [Description("Elevators")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 15)]
#endif
        HARDWARESETTINGS_ELEVATORS_R,
#if FLEXAPI
        [Offset(391, 1)]
        [Description("Event Log Settings")]
        [Tree("Reports\\Events", ControlType.CheckBox, Assets.Reports, 4, 2, 1)]
#endif
        AMTREPORTS_LOGGING_R,
#if FLEXAPI
        [Offset(392, 1)]
        [Description("Pending Alarms")]
        [Tree("Reports\\Alarms", ControlType.CheckBox, Assets.Reports, 4, 1, 2)]
#endif
        AMTREPORTS_PENDINGALARMS_R,
#if FLEXAPI
        [Offset(393, 1)]
        [Description("Event History")]
        [Tree("Reports\\Events", ControlType.CheckBox, Assets.Reports, 4, 2, 0)]
#endif
        AMTREPORTS_TRANSACTIONSREPORT_R,
#if FLEXAPI
        [Offset(394, 1)]
        [Description("Audit Trail")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 0)]
#endif
        AUDITTRAILREPORTS_R,
#if FLEXAPI
        [Offset(395, 1)]
        [Description("Doors")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 10)]
#endif
        DOORREPORTS_SETTINGS_R,
#if FLEXAPI
        [Offset(397, 1)]
        [Description("Controllers (SSP)")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 2)]
#endif
        HARDWARE_CONTROLLER_R,
#if FLEXAPI
        [Offset(399, 1)]
        [Description("Readers")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 7)]
#endif
        HARDWARE_READER_R,
#if FLEXAPI
        [Offset(401, 1)]
        [Description("Sites")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 0)]
#endif
        HARDWARE_SITE_R,
#if FLEXAPI
        [Offset(402, 1)]
        [Description("Auto Armed Secured Areas")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 9)]
#endif
        HARDWARE_SITESTATE_R,
#if FLEXAPI
        [Offset(403, 1)]
        [Description("Sub-controllers (SIO)")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 4)]
#endif
        HARDWARE_SUBCONTROLLER_R,
#if FLEXAPI
        [Offset(405, 1)]
        [Description("APB Doors")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 14)]
#endif
        HARDWARESETTINGS_APBDOORS_R,
#if FLEXAPI
        [Offset(406, 1)]
        [Description("Cameras")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 8)]
#endif
        HARDWARESETTINGS_CAMERAS_R,
#if FLEXAPI
        [Offset(407, 1)]
        [Description("Channels")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 1)]
#endif
        HARDWARESETTINGS_CHANNELS_R,
#if FLEXAPI
        [Offset(408, 1)]
        [Description("Control Points")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 6)]
#endif
        HARDWARESETTINGS_CONTROLPOINTS_R,
#if FLEXAPI
        [Offset(409, 1)]
        [Description("Door Contacts")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 11)]
#endif
        HARDWARESETTINGS_DOORCONTACTS_R,
#if FLEXAPI
        [Offset(410, 1)]
        [Description("Door Strikes")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 13)]
#endif
        HARDWARESETTINGS_DOORSTRIKES_R,
#if FLEXAPI
        [Offset(411, 1)]
        [Description("Monitor Points")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 5)]
#endif
        HARDWARESETTINGS_MONITORPOINTS_R,
#if FLEXAPI
        [Offset(412, 1)]
        [Description("Monitor Point Groups (MPG)")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 9)]
#endif
        HARDWARESETTINGS_MPG_R,
#if FLEXAPI
        [Offset(413, 1)]
        [Description("Request to Exits (RTE)")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 12)]
#endif
        HARDWARESETTINGS_REQUESTTOEXIT_R,
#if FLEXAPI
        [Offset(417, 1)]
        [Description("Door Access Profile")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 6)]
#endif
        OTHERREPORTS_DOORACCESSPROFILE_R,
#if FLEXAPI
        [Offset(418, 1)]
        [Description("Holidays")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 1)]
#endif
        OTHERREPORTS_HOLIDAYS_R,
#if FLEXAPI
        [Offset(419, 1)]
        [Description("Macros")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 2)]
#endif
        OTHERREPORTS_MACROS_R,
#if FLEXAPI
        [Offset(420, 1)]
        [Description("Operators")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 8)]
#endif
        OTHERREPORTS_OPERATORS_R,
#if FLEXAPI
        [Offset(421, 1)]
        [Description("Time Schedules")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 3)]
#endif
        OTHERREPORTS_TIMESCHEDULES_R,
#if FLEXAPI
        [Offset(422, 1)]
        [Description("Triggers")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 4)]
#endif
        OTHERREPORTS_TRIGGERS_R,
#if FLEXAPI
        [Offset(423, 1)]
        [Description("Personnel - Card Info")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 2)]
#endif
        PERSONNELREPORTS_ALLUSERS_R,
#if FLEXAPI
        [Offset(424, 1)]
        [Description("Companies")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 0)]
#endif
        PERSONNELREPORTS_COMPANIES_R,
#if FLEXAPI
        [Offset(425, 1)]
        [Description("Personnel - Groups")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 4)]
#endif
        PERSONNELREPORTS_GROUPS_R,
#if FLEXAPI
        [Offset(426, 1)]
        [Description("Personnel - Access")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 3)]
#endif
        PERSONNELREPORTS_INDIVIDUAL_R,
#if FLEXAPI
        [Offset(427, 1)]
        [Description("Personnel - General")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 1)]
#endif
        PERSONNELREPORTS_PERSONNELGENERAL_R,
#if FLEXAPI
        [Offset(428, 1)]
        [DescriptionLookup(Default = "Custom Report 1")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 2)]
#endif
        CUSTOM1_R,
#if FLEXAPI
        [Offset(429, 1)]
        [DescriptionLookup(Default = "Custom Report 2")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 3)]
#endif
        CUSTOM2_R,
#if FLEXAPI
        [Offset(430, 1)]
        [DescriptionLookup(Default = "Custom Report 3")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 4)]
#endif
        CUSTOM3_R,
#if FLEXAPI
        [Offset(431, 1)]
        [DescriptionLookup(Default = "Custom Report 4")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 5)]
#endif
        CUSTOM4_R,
#if FLEXAPI
        [Offset(432, 1)]
        [DescriptionLookup(Default = "Custom Report 5")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 6)]
#endif
        CUSTOM5_R,
#if FLEXAPI
        [Offset(433, 1)]
        [DescriptionLookup(Default = "Custom Report 6")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 7)]
#endif
        CUSTOM6_R,
#if FLEXAPI
        [Offset(434, 1)]
        [DescriptionLookup(Default = "Custom Report 7")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 8)]
#endif
        CUSTOM7_R,
#if FLEXAPI
        [Offset(435, 1)]
        [DescriptionLookup(Default = "Custom Report 8")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 9)]
#endif
        CUSTOM8_R,
#if FLEXAPI
        [Offset(436, 1)]
        [DescriptionLookup(Default = "Custom Report 9")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 10)]
#endif
        CUSTOM9_R,
#if FLEXAPI
        [Offset(437, 1)]
        [DescriptionLookup(Default = "Custom Report 10")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 11)]
#endif
        CUSTOM10_R,
#if FLEXAPI
        [Offset(438, 1)]
        [DescriptionLookup(Default = "Custom Report 11")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 12)]
#endif
        CUSTOM11_R,
#if FLEXAPI
        [Offset(439, 1)]
        [DescriptionLookup(Default = "Custom Report 12")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 13)]
#endif
        CUSTOM12_R,
#if FLEXAPI
        [Offset(440, 1)]
        [DescriptionLookup(Default = "Custom Report 13")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 14)]
#endif
        CUSTOM13_R,
#if FLEXAPI
        [Offset(441, 1)]
        [DescriptionLookup(Default = "Custom Report 14")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 15)]
#endif
        CUSTOM14_R,
#if FLEXAPI
        [Offset(442, 1)]
        [DescriptionLookup(Default = "Custom Report 15")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 16)]
#endif
        CUSTOM15_R,
#if FLEXAPI
        [Offset(443, 1)]
        [DescriptionLookup(Default = "Custom Report 16")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 17)]
#endif
        CUSTOM16_R,
#if FLEXAPI
        [Offset(444, 1)]
        [DescriptionLookup(Default = "Custom Report 17")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 18)]
#endif
        CUSTOM17_R,
#if FLEXAPI
        [Offset(445, 1)]
        [DescriptionLookup(Default = "Custom Report 18")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 19)]
#endif
        CUSTOM18_R,
#if FLEXAPI
        [Offset(446, 1)]
        [DescriptionLookup(Default = "Custom Report 19")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 20)]
#endif
        CUSTOM19_R,
#if FLEXAPI
        [Offset(447, 1)]
        [DescriptionLookup(Default = "Custom Report 20")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 21)]
#endif
        CUSTOM20_R,
#if FLEXAPI
        [Offset(448, 1)]
        [Description("Set Card Use Limit")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 5)]
#endif
        ALLOWSETUSELIMIT,
#if FLEXAPI
        [Offset(449, 1)]
        [Description("Issue Free Pass")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 6)]
#endif
        ALLOWISSUEFREEPASS,
#if FLEXAPI
        [Offset(450, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 7)]
#endif
        TraceACM,
#if FLEXAPI
        [Offset(451, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Monitor Point", ControlType.CheckBox, Assets.MonitorPoint, Assets.Controller, 0, 0, 3)]
#endif
        TraceMP,
#if FLEXAPI
        [Offset(452, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Control Point", ControlType.CheckBox, Assets.ControlPoint, Assets.Controller, 0, 1, 3)]
#endif
        TraceCP,
#if FLEXAPI
        [Offset(453, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Time Schedules", ControlType.CheckBox, Assets.TimeSchedule, Assets.Controller, 0, 3, 2)]
#endif
        TraceTS,
#if FLEXAPI
        [Offset(454, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Access Area", ControlType.CheckBox, Assets.AccessArea, Assets.Controller, 0, 5, 3)]
#endif
        TraceArea,
#if FLEXAPI
        [Offset(455, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\MPG", ControlType.CheckBox, Assets.MPG, Assets.Controller, 0, 6, 3)]
#endif
        TraceMPG,
#if FLEXAPI
        [Offset(456, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Triggers & Macros", ControlType.CheckBox, Assets.TriggersAndMacros, Assets.Controller, 0, 7, 3)]
#endif
        TraceOther,
#if FLEXAPI
        [Offset(457, 1)]
        [Description("Trace History")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 9)]
#endif
        TraceCard,
#if FLEXAPI
        [Offset(471, 1)]
        [Description("Door: Forced Arm")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 1)]
#endif
        AllowCtrlAcmForce,
#if FLEXAPI
        [Offset(472, 1)]
        [Description("Door: Held Arm")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 2)]
#endif
        AllowCtrlAcmHeld,
#if FLEXAPI
        [Offset(473, 1)]
        [Description("Door: Temporary Unlock")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 3)]
#endif
        AllowCtrlAcmUnlock,
#if FLEXAPI
        [Offset(474, 1)]
        [Description("Door: Set Door Mode")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 4)]
#endif
        AllowCtrlAcmMode,
#if FLEXAPI
        [Offset(475, 1)]
        [Description("Alarms")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 1)]
#endif
        ShowAlarms,
#if FLEXAPI
        [Offset(476, 1)]
        [Description("Events Manager")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 3)]
#endif
        ShowEvents,
#if FLEXAPI
        [Offset(477, 1)]
        [Description("Show Graphics")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 0)]
#endif
        ShowGraphics,
#if FLEXAPI
        [Offset(478, 1)]
        [Description("Allow Precision Assignment")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 6)]
#endif
        ALLOWASSIGNRDR_H,
#if FLEXAPI
        [Offset(479, 1)]
        [Secondary(491, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Door\\High Security", ControlType.ComboBox, Assets.ACM, Assets.Controller, 0, 2, 6, 0)]
#endif
        ALLOWEDITRDR_H,
#if FLEXAPI
        [Offset(480, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 14", Default = "Custom String 12")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 51)]
#endif
        PersonnelField97,
#if FLEXAPI
        [Offset(482, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 15", Default = "Custom String 13")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 52)]
#endif
        PersonnelField98,
#if FLEXAPI
        [Offset(484, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 16", Default = "Custom String 14")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 53)]
#endif
        PersonnelField99,
#if FLEXAPI
        [Offset(486, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 17", Default = "Custom String 15")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 54)]
#endif
        PersonnelField100,
#if FLEXAPI
        [Offset(488, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 18", Default = "Custom String 16")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 55)]
#endif
        PersonnelField101,
#if FLEXAPI
        [Offset(490, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 7)]
#endif
        TraceACM_H,
#if FLEXAPI
        [Offset(492, 1)]
        [Description("Door: Forced Arm")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 1)]
#endif
        AllowCtrlAcmForce_H,
#if FLEXAPI
        [Offset(493, 1)]
        [Description("Door: Held Arm")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 2)]
#endif
        AllowCtrlAcmHeld_H,
#if FLEXAPI
        [Offset(494, 1)]
        [Description("Door: Temporary Unlock")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 3)]
#endif
        AllowCtrlAcmUnlock_H,
#if FLEXAPI
        [Offset(495, 1)]
        [Description("Door: Set Door Mode")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 4)]
#endif
        AllowCtrlAcmMode_H,
#if FLEXAPI
        [Offset(496, 1)]
        [Description("Allow Precision Assignment")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 6)]
#endif
        ALLOWASSIGNRDR_M,
#if FLEXAPI
        [Offset(497, 1)]
        [Secondary(499, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.ComboBox, Assets.ACM, Assets.Controller, 0, 2, 7, 0)]
#endif
        ALLOWEDITRDR_M,
#if FLEXAPI
        [Offset(498, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 7)]
#endif
        TraceACM_M,
#if FLEXAPI
        [Offset(500, 1)]
        [Description("Door: Forced Arm")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 1)]
#endif
        AllowCtrlAcmForce_M,
#if FLEXAPI
        [Offset(501, 1)]
        [Description("Door: Held Arm")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 2)]
#endif
        AllowCtrlAcmHeld_M,
#if FLEXAPI
        [Offset(502, 1)]
        [Description("Door: Temporary Unlock")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 3)]
#endif
        AllowCtrlAcmUnlock_M,
#if FLEXAPI
        [Offset(503, 1)]
        [Description("Door: Set Door Mode")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 4)]
#endif
        AllowCtrlAcmMode_M,
#if FLEXAPI
        [Offset(504, 1)]
        [Description("Allow Precision Assignment")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 6)]
#endif
        ALLOWASSIGNRDR_L,
#if FLEXAPI
        [Offset(505, 1)]
        [Secondary(507, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Door\\Low Security", ControlType.ComboBox, Assets.ACM, Assets.Controller, 0, 2, 8, 0)]
#endif
        ALLOWEDITRDR_L,
#if FLEXAPI
        [Offset(506, 1)]
        [Description("Trace History")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 7)]
#endif
        TraceACM_L,
#if FLEXAPI
        [Offset(508, 1)]
        [Description("Door: Forced Arm")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 1)]
#endif
        AllowCtrlAcmForce_L,
#if FLEXAPI
        [Offset(509, 1)]
        [Description("Door: Held Arm")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 2)]
#endif
        AllowCtrlAcmHeld_L,
#if FLEXAPI
        [Offset(510, 1)]
        [Description("Door: Temporary Unlock")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 3)]
#endif
        AllowCtrlAcmUnlock_L,
#if FLEXAPI
        [Offset(511, 1)]
        [Description("Door: Set Door Mode")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 4)]
#endif
        AllowCtrlAcmMode_L,
#if FLEXAPI
        [Offset(512, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 25)]
#endif
        PersonnelField65,
#if FLEXAPI
        [Offset(514, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 26)]
#endif
        PersonnelField66,
#if FLEXAPI
        [Offset(516, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 27)]
#endif
        PersonnelField67,
#if FLEXAPI
        [Offset(518, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 28)]
#endif
        PersonnelField68,
#if FLEXAPI
        [Offset(520, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 29)]
#endif
        PersonnelField69,
#if FLEXAPI
        [Offset(522, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 30)]
#endif
        PersonnelField70,
#if FLEXAPI
        [Offset(524, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 31)]
#endif
        PersonnelField71,
#if FLEXAPI
        [Offset(526, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 32)]
#endif
        PersonnelField72,
#if FLEXAPI
        [Offset(528, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 33)]
#endif
        PersonnelField73,
#if FLEXAPI
        [Offset(530, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 34)]
#endif
        PersonnelField74,
#if FLEXAPI
        [Offset(532, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 35)]
#endif
        PersonnelField75,
#if FLEXAPI
        [Offset(534, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 36)]
#endif
        PersonnelField76,
#if FLEXAPI
        [Offset(536, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 37)]
#endif
        PersonnelField77,
#if FLEXAPI
        [Offset(538, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 38)]
#endif
        PersonnelField78,
#if FLEXAPI
        [Offset(540, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 39)]
#endif
        PersonnelField79,
#if FLEXAPI
        [Offset(542, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 40)]
#endif
        PersonnelField80,
#if FLEXAPI
        [Offset(544, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 41)]
#endif
        PersonnelField81,
#if FLEXAPI
        [Offset(546, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 42)]
#endif
        PersonnelField82,
#if FLEXAPI
        [Offset(548, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 43)]
#endif
        PersonnelField83,
#if FLEXAPI
        [Offset(550, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 44)]
#endif
        PersonnelField84,
#if FLEXAPI
        [Offset(552, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 45)]
#endif
        PersonnelField85,
#if FLEXAPI
        [Offset(554, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 46)]
#endif
        PersonnelField86,
#if FLEXAPI
        [Offset(556, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 47)]
#endif
        PersonnelField87,
#if FLEXAPI
        [Offset(558, 2)]
        [DescriptionLookup("")]
        [Tree("Personnel\\Personnel Custom Fields", ControlType.ComboBox, Assets.Personnel, 2, 3, 48)]
#endif
        PersonnelField88,
#if FLEXAPI
        [Offset(560, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 6", Default = "Custom String 4")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 43)]
#endif
        PersonnelField89,
#if FLEXAPI
        [Offset(562, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 7", Default = "Custom String 5")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 44)]
#endif
        PersonnelField90,
#if FLEXAPI
        [Offset(564, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 8", Default = "Custom String 6")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 45)]
#endif
        PersonnelField91,
#if FLEXAPI
        [Offset(566, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 9", Default = "Custom String 7")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 46)]
#endif
        PersonnelField92,
#if FLEXAPI
        [Offset(568, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 10", Default = "Custom String 8")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 47)]
#endif
        PersonnelField93,
#if FLEXAPI
        [Offset(570, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 11", Default = "Custom String 9")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 48)]
#endif
        PersonnelField94,
#if FLEXAPI
        [Offset(572, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 12", Default = "Custom String 10")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 49)]
#endif
        PersonnelField95,
#if FLEXAPI
        [Offset(574, 2)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Field 13", Default = "Custom String 11")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 50)]
#endif
        PersonnelField96,
#if FLEXAPI
        [Offset(704, 1)]
        [Description("Station Level 1")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 0)]
#endif
        Station1,
#if FLEXAPI
        [Offset(705, 1)]
        [Description("Station Level 2")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 1)]
#endif
        Station2,
#if FLEXAPI
        [Offset(706, 1)]
        [Description("Station Level 3")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 2)]
#endif
        Station3,
#if FLEXAPI
        [Offset(707, 1)]
        [Description("Station Level 4")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 3)]
#endif
        Station4,
#if FLEXAPI
        [Offset(708, 1)]
        [Description("Station Level 5")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 4)]
#endif
        Station5,
#if FLEXAPI
        [Offset(709, 1)]
        [Description("Station Level 6")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 5)]
#endif
        Station6,
#if FLEXAPI
        [Offset(710, 1)]
        [Description("Station Level 7")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 6)]
#endif
        Station7,
#if FLEXAPI
        [Offset(711, 1)]
        [Description("Station Level 8")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 7)]
#endif
        Station8,
#if FLEXAPI
        [Offset(712, 1)]
        [Description("Station Level 9")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 8)]
#endif
        Station9,
#if FLEXAPI
        [Offset(713, 1)]
        [Description("Station Level 10")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 9)]
#endif
        Station10,
#if FLEXAPI
        [Offset(714, 1)]
        [Description("Station Level 11")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 10)]
#endif
        Station11,
#if FLEXAPI
        [Offset(715, 1)]
        [Description("Station Level 12")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 11)]
#endif
        Station12,
#if FLEXAPI
        [Offset(716, 1)]
        [Description("Station Level 13")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 12)]
#endif
        Station13,
#if FLEXAPI
        [Offset(717, 1)]
        [Description("Station Level 14")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 13)]
#endif
        Station14,
#if FLEXAPI
        [Offset(718, 1)]
        [Description("Station Level 15")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 14)]
#endif
        Station15,
#if FLEXAPI
        [Offset(719, 1)]
        [Description("Station Level 16")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 15)]
#endif
        Station16,
#if FLEXAPI
        [Offset(720, 1)]
        [Description("Station Level 17")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 16)]
#endif
        Station17,
#if FLEXAPI
        [Offset(721, 1)]
        [Description("Station Level 18")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 17)]
#endif
        Station18,
#if FLEXAPI
        [Offset(722, 1)]
        [Description("Station Level 19")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 18)]
#endif
        Station19,
#if FLEXAPI
        [Offset(723, 1)]
        [Description("Station Level 20")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 19)]
#endif
        Station20,
#if FLEXAPI
        [Offset(724, 1)]
        [Description("Station Level 21")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 20)]
#endif
        Station21,
#if FLEXAPI
        [Offset(725, 1)]
        [Description("Station Level 22")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 21)]
#endif
        Station22,
#if FLEXAPI
        [Offset(726, 1)]
        [Description("Station Level 23")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 22)]
#endif
        Station23,
#if FLEXAPI
        [Offset(727, 1)]
        [Description("Station Level 24")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 23)]
#endif
        Station24,
#if FLEXAPI
        [Offset(728, 1)]
        [Description("Station Level 25")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 24)]
#endif
        Station25,
#if FLEXAPI
        [Offset(729, 1)]
        [Description("Station Level 26")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 25)]
#endif
        Station26,
#if FLEXAPI
        [Offset(730, 1)]
        [Description("Station Level 27")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 26)]
#endif
        Station27,
#if FLEXAPI
        [Offset(731, 1)]
        [Description("Station Level 28")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 27)]
#endif
        Station28,
#if FLEXAPI
        [Offset(732, 1)]
        [Description("Station Level 29")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 28)]
#endif
        Station29,
#if FLEXAPI
        [Offset(733, 1)]
        [Description("Station Level 30")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 29)]
#endif
        Station30,
#if FLEXAPI
        [Offset(734, 1)]
        [Description("Station Level 31")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 30)]
#endif
        Station31,
#if FLEXAPI
        [Offset(735, 1)]
        [Description("Station Level 32")]
        [Tree("Station Levels", ControlType.CheckBox, Assets.StationLevels, 10, 31)]
#endif
        Station32,
#if FLEXAPI
        [Offset(832, 1)]
        [Description("Assign High Security Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 2)]
#endif
        AssignAxsLvl_High,
#if FLEXAPI
        [Offset(833, 1)]
        [Description("Assign Medium Security Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 3)]
#endif
        AssignAxsLvl_Medium,
#if FLEXAPI
        [Offset(834, 1)]
        [Description("Assign Low Security Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 4)]
#endif
        AssignAxsLvl_Low,
#if FLEXAPI
        [Offset(835, 1)]
        [Description("Assign Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 1)]
#endif
        AssignAxsLvl,
#if FLEXAPI
        [Offset(836, 1)]
        [Description("Assign Access Level (Custom 1)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 5)]
#endif
        AssignAxsLvl_Custom1,
#if FLEXAPI
        [Offset(837, 1)]
        [Description("Assign Access Level (Custom 2)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 6)]
#endif
        AssignAxsLvl_Custom2,
#if FLEXAPI
        [Offset(838, 1)]
        [Description("Assign Access Level (Custom 3)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 7)]
#endif
        AssignAxsLvl_Custom3,
#if FLEXAPI
        [Offset(839, 1)]
        [Description("Assign Access Level (Custom 4)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 8)]
#endif
        AssignAxsLvl_Custom4,
#if FLEXAPI
        [Offset(840, 1)]
        [Description("NORMAL")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 7)]
#endif
        PersonnelType_Perm,
#if FLEXAPI
        [Offset(841, 1)]
        [Description("Visitor")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 10)]
#endif
        PersonnelType_Visitor,
#if FLEXAPI
        [Offset(842, 1)]
        [Description("Temp")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 8)]
#endif
        PersonnelType_Temp,
#if FLEXAPI
        [Offset(843, 1)]
        [Description("Contractor")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 0)]
#endif
        PersonnelType_Contractor,
#if FLEXAPI
        [Offset(844, 1)]
        [Description("Vendor")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 9)]
#endif
        PersonnelType_Vendor,
#if FLEXAPI
        [Offset(845, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 0", Default = "Custom 1")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 1)]
#endif
        PersonnelType_Custom1,
#if FLEXAPI
        [Offset(846, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 1", Default = "Custom 2")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 2)]
#endif
        PersonnelType_Custom2,
#if FLEXAPI
        [Offset(847, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 2", Default = "Custom 3")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 3)]
#endif
        PersonnelType_Custom3,
#if FLEXAPI
        [Offset(848, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 3", Default = "Custom 4")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 4)]
#endif
        PersonnelType_Custom4,
#if FLEXAPI
        [Offset(849, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 4", Default = "Custom 5")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 5)]
#endif
        PersonnelType_Custom5,
#if FLEXAPI
        [Offset(850, 1)]
        [Description("Disabled")]
        [Tree("Personnel\\Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 5, 6)]
#endif
        PersonnelType_Disabled,
#if FLEXAPI
        [Offset(851, 1)]
        [Description("Add or Edit Personnel Group")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 10)]
#endif
        PersonnelAddGroup,
#if FLEXAPI
        [Offset(852, 1)]
        [Description("Add Cardholder to Personnel Group")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 11)]
#endif
        PersonnelAddPersonToGroup,
#if FLEXAPI
        [Offset(853, 1)]
        [Description("Photo Badging")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 0)]
#endif
        Badging,
#if FLEXAPI
        [Offset(854, 1)]
        [Description("View Badge")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 1)]
#endif
        BadgingView,
#if FLEXAPI
        [Offset(855, 1)]
        [Description("Take Photo")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 2)]
#endif
        BadgingTakePhoto,
#if FLEXAPI
        [Offset(856, 1)]
        [Description("Add Photo")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 3)]
#endif
        BadgingAddPhoto,
#if FLEXAPI
        [Offset(857, 1)]
        [Description("Print Badge")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 4)]
#endif
        BadgingPrintBadge,
#if FLEXAPI
        [Offset(858, 1)]
        [Description("Remove Photos")]
        [Tree("Personnel\\Photo Badging", ControlType.CheckBox, Assets.Personnel, 2, 9, 5)]
#endif
        BadgingRemovePhoto,
#if FLEXAPI
        [Offset(859, 1)]
        [Description("Record Video")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.CheckBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 4)]
#endif
        DvrRecord,
#if FLEXAPI
        [Offset(860, 1)]
        [Description("View Archived Video")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.CheckBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 3)]
#endif
        DvrPlayback,
#if FLEXAPI
        [Offset(861, 1)]
        [Description("Allow PTZ Control")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.CheckBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 5)]
#endif
        DvrPTZControl,
#if FLEXAPI
        [Offset(862, 1)]
        [Description("View Live Video")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.CheckBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 2)]
#endif
        DvrViewCamera,
#if FLEXAPI
        [Offset(864, 1)]
        [Secondary(865, 0)]
        [Description("Allow Edit Server Properties")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.ComboBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 0)]
#endif
        ALLOWEDITSERVER,
#if FLEXAPI
        [Offset(887, 1)]
        [Secondary(888, 1)]
        [Description("UD Edit")]
        [Tree("Hardware\\Universal Driver", ControlType.ComboBox, Assets.UniversalDriver, Assets.Controller, 0, 12, 0)]
#endif
        UD_EDIT,
#if FLEXAPI
        [Offset(889, 1)]
        [Description("Allow Direct Control")]
        [Tree("Hardware\\Universal Driver", ControlType.CheckBox, Assets.UniversalDriver, Assets.Controller, 0, 12, 1)]
#endif
        UD_CONTROL,
#if FLEXAPI
        [Offset(890, 4)]
        [Description("UD Level")]
        [AvailableValues("Security Level 0", "Security Level 1", "Security Level 2", "Security Level 3", "Security Level 4", "Security Level 5", "Security Level 6", "Security Level 7")]
        [Tree("Hardware\\Universal Driver", ControlType.ComboBox, Assets.UniversalDriver, Assets.Controller, 0, 12, 3)]
#endif
        UD_LEVEL,
#if FLEXAPI
        [Offset(894, 1)]
        [Description("Acknowledge All")]
        [Tree("Alarms", ControlType.CheckBox, Assets.Alarms, 3, 0)]
#endif
        AlarmsAckAll,
#if FLEXAPI
        [Offset(895, 1)]
        [Description("Clear All Alarms")]
        [Tree("Alarms", ControlType.CheckBox, Assets.Alarms, 3, 2)]
#endif
        AlarmsClearAll,
#if FLEXAPI
        [Offset(896, 1)]
        [Description("Add Custom Report")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 0)]
#endif
        AddCustomReports,
#if FLEXAPI
        [Offset(897, 1)]
        [Description("Remove Custom Report")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 1)]
#endif
        RemoveCustomReports,
#if FLEXAPI
        [Offset(898, 1)]
        [Description("Add Tenant")]
        [Tree("Reports\\Tenants", ControlType.CheckBox, Assets.Tenants, Assets.Reports, 4, 8, 1)]
#endif
        TenantsAdd,
#if FLEXAPI
        [Offset(899, 1)]
        [Description("Remove Tenant")]
        [Tree("Reports\\Tenants", ControlType.CheckBox, Assets.Tenants, Assets.Reports, 4, 8, 2)]
#endif
        TenantsRemove,
#if FLEXAPI
        [Offset(900, 1)]
        [Secondary(901, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Reports\\Tenants", ControlType.ComboBox, Assets.Tenants, Assets.Reports, 4, 8, 0)]
#endif
        TenantsEdit,
#if FLEXAPI
        [Offset(902, 2)]
        [Description("HTML Viewer")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 5)]
#endif
        ViewHTML,
#if FLEXAPI
        [Offset(904, 2)]
        [Description("Hardware Manager")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 4)]
#endif
        ViewHardware,
#if FLEXAPI
        [Offset(906, 2)]
        [Description("Personnel")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 7)]
#endif
        ViewPersonnel,
#if FLEXAPI
        [Offset(908, 2)]
        [Description("Watch")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 13)]
#endif
        ViewWatch,
#if FLEXAPI
        [Offset(910, 2)]
        [Description("Time Schedules")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 11)]
#endif
        ViewTimeSchedules,
#if FLEXAPI
        [Offset(912, 2)]
        [Description("Photo Recall")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 8)]
#endif
        ViewPhotoRecall,
#if FLEXAPI
        [Offset(914, 2)]
        [Description("Triggers and Macros")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 12)]
#endif
        ViewTriggersMacros,
#if FLEXAPI
        [Offset(916, 2)]
        [Description("Text messaging")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 9)]
#endif
        ViewTextMessaging,
#if FLEXAPI
        [Offset(918, 2)]
        [Description("Operators...")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 6)]
#endif
        ViewOperators,
#if FLEXAPI
        [Offset(920, 2)]
        [Description("Access Areas")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 0)]
#endif
        ViewAccessAreas,
#if FLEXAPI
        [Offset(922, 2)]
        [Description("Actions E-Mail")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 7)]
#endif
        ActionsEMail,
#if FLEXAPI
        [Offset(924, 2)]
        [Description("Filter Events")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 8)]
#endif
        ActionsEventFilter,
#if FLEXAPI
        [Offset(926, 2)]
        [Description("Station Configuration")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 9)]
#endif
        ActionsStationCfg,
#if FLEXAPI
        [Offset(928, 2)]
        [Description("Customize")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 5)]
#endif
        ActionsCustomize,
#if FLEXAPI
        [Offset(930, 2)]
        [Description("Add To Watch")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 0)]
#endif
        ActionsAddToWatch,
#if FLEXAPI
        [Offset(932, 2)]
        [Description("Remove From Watch")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 1)]
#endif
        ActionsRemoveFromWatch,
#if FLEXAPI
        [Offset(934, 2)]
        [Description("Archive Data")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 2)]
#endif
        ActionsArchiveData,
#if FLEXAPI
        [Offset(936, 2)]
        [Description("Restore Data")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 3)]
#endif
        ActionsRestoreData,
#if FLEXAPI
        [Offset(938, 2)]
        [Description("Batch Processing")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 4)]
#endif
        ActionsBatchCommands,
#if FLEXAPI
        [Offset(940, 2)]
        [Description("Edit HTML Views")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 6)]
#endif
        ActionsEditHTMLViews,
#if FLEXAPI
        [Offset(942, 2)]
        [Description("Edit Graphics")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 1)]
#endif
        GraphicsEdit,
#if FLEXAPI
        [Offset(956, 1)]
        [Description("Arm All on page")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 2)]
#endif
        GraphicsArmAll,
#if FLEXAPI
        [Offset(957, 1)]
        [Description("Disarm all on page")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 3)]
#endif
        GraphicsDisarmAll,
#if FLEXAPI
        [Offset(958, 1)]
        [Description("Acknowledges All on page")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 4)]
#endif
        GraphicsAckAll,
#if FLEXAPI
        [Offset(959, 1)]
        [Description("Manage Direct Command")]
        [Tree("Direct Commands", ControlType.CheckBox, Assets.DirectCommands, 8, 0)]
#endif
        DirectCmdsManage,
#if FLEXAPI
        [Ignore]
        [Offset(960, 1)]
        [Description("Remove Direct Command")]
        [Tree("Direct Commands", ControlType.CheckBox, Assets.DirectCommands, 8, 3)]
#endif
        DirectCmdsRemove,
#if FLEXAPI
        [Ignore]
        [Offset(961, 1)]
        [Description("Edit Direct Command")]
        [HideIfDefault]
        [Tree("Direct Commands", ControlType.CheckBox, Assets.DirectCommands, 8, 2)]
#endif
        DirectCmdsEdit,
#if FLEXAPI
        [Offset(962, 1)]
        [DescriptionLookup(Default = "Direct Command 1")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 0)]
#endif
        DirectCmds_1,
#if FLEXAPI
        [Offset(963, 1)]
        [DescriptionLookup(Default = "Direct Command 2")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 1)]
#endif
        DirectCmds_2,
#if FLEXAPI
        [Offset(964, 1)]
        [DescriptionLookup(Default = "Direct Command 3")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 2)]
#endif
        DirectCmds_3,
#if FLEXAPI
        [Offset(965, 1)]
        [DescriptionLookup(Default = "Direct Command 4")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 3)]
#endif
        DirectCmds_4,
#if FLEXAPI
        [Offset(966, 1)]
        [DescriptionLookup(Default = "Direct Command 5")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 4)]
#endif
        DirectCmds_5,
#if FLEXAPI
        [Offset(967, 1)]
        [DescriptionLookup(Default = "Direct Command 6")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 5)]
#endif
        DirectCmds_6,
#if FLEXAPI
        [Offset(968, 1)]
        [DescriptionLookup(Default = "Direct Command 7")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 6)]
#endif
        DirectCmds_7,
#if FLEXAPI
        [Offset(969, 1)]
        [DescriptionLookup(Default = "Direct Command 8")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 7)]
#endif
        DirectCmds_8,
#if FLEXAPI
        [Offset(970, 1)]
        [DescriptionLookup(Default = "Direct Command 9")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 8)]
#endif
        DirectCmds_9,
#if FLEXAPI
        [Offset(971, 1)]
        [DescriptionLookup(Default = "Direct Command 10")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 9)]
#endif
        DirectCmds_10,
#if FLEXAPI
        [Offset(972, 1)]
        [DescriptionLookup(Default = "Direct Command 11")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 10)]
#endif
        DirectCmds_11,
#if FLEXAPI
        [Offset(973, 1)]
        [DescriptionLookup(Default = "Direct Command 12")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 11)]
#endif
        DirectCmds_12,
#if FLEXAPI
        [Offset(974, 1)]
        [DescriptionLookup(Default = "Direct Command 13")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 12)]
#endif
        DirectCmds_13,
#if FLEXAPI
        [Offset(975, 1)]
        [DescriptionLookup(Default = "Direct Command 14")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 13)]
#endif
        DirectCmds_14,
#if FLEXAPI
        [Offset(976, 1)]
        [DescriptionLookup(Default = "Direct Command 15")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 14)]
#endif
        DirectCmds_15,
#if FLEXAPI
        [Offset(977, 1)]
        [DescriptionLookup(Default = "Direct Command 16")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 15)]
#endif
        DirectCmds_16,
#if FLEXAPI
        [Offset(978, 1)]
        [DescriptionLookup(Default = "Direct Command 17")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 16)]
#endif
        DirectCmds_17,
#if FLEXAPI
        [Offset(979, 1)]
        [DescriptionLookup(Default = "Direct Command 18")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 17)]
#endif
        DirectCmds_18,
#if FLEXAPI
        [Offset(980, 1)]
        [DescriptionLookup(Default = "Direct Command 19")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 18)]
#endif
        DirectCmds_19,
#if FLEXAPI
        [Offset(981, 1)]
        [DescriptionLookup(Default = "Direct Command 20")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 19)]
#endif
        DirectCmds_20,
#if FLEXAPI
        [Offset(982, 1)]
        [DescriptionLookup(Default = "Direct Command 21")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 20)]
#endif
        DirectCmds_21,
#if FLEXAPI
        [Offset(983, 1)]
        [DescriptionLookup(Default = "Direct Command 22")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 21)]
#endif
        DirectCmds_22,
#if FLEXAPI
        [Offset(984, 1)]
        [DescriptionLookup(Default = "Direct Command 23")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 22)]
#endif
        DirectCmds_23,
#if FLEXAPI
        [Offset(985, 1)]
        [DescriptionLookup(Default = "Direct Command 24")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 23)]
#endif
        DirectCmds_24,
#if FLEXAPI
        [Offset(986, 1)]
        [DescriptionLookup(Default = "Direct Command 25")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 24)]
#endif
        DirectCmds_25,
#if FLEXAPI
        [Offset(987, 1)]
        [DescriptionLookup(Default = "Direct Command 26")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 25)]
#endif
        DirectCmds_26,
#if FLEXAPI
        [Offset(988, 1)]
        [DescriptionLookup(Default = "Direct Command 27")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 26)]
#endif
        DirectCmds_27,
#if FLEXAPI
        [Offset(989, 1)]
        [DescriptionLookup(Default = "Direct Command 28")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 27)]
#endif
        DirectCmds_28,
#if FLEXAPI
        [Offset(990, 1)]
        [DescriptionLookup(Default = "Direct Command 29")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 28)]
#endif
        DirectCmds_29,
#if FLEXAPI
        [Offset(991, 1)]
        [DescriptionLookup(Default = "Direct Command 30")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 29)]
#endif
        DirectCmds_30,
#if FLEXAPI
        [Offset(992, 1)]
        [DescriptionLookup(Default = "Direct Command 31")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 30)]
#endif
        DirectCmds_31,
#if FLEXAPI
        [Offset(993, 1)]
        [DescriptionLookup(Default = "Direct Command 32")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 31)]
#endif
        DirectCmds_32,
#if FLEXAPI
        [Offset(994, 1)]
        [DescriptionLookup(Default = "Direct Command 33")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 32)]
#endif
        DirectCmds_33,
#if FLEXAPI
        [Offset(995, 1)]
        [DescriptionLookup(Default = "Direct Command 34")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 33)]
#endif
        DirectCmds_34,
#if FLEXAPI
        [Offset(996, 1)]
        [DescriptionLookup(Default = "Direct Command 35")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 34)]
#endif
        DirectCmds_35,
#if FLEXAPI
        [Offset(997, 1)]
        [DescriptionLookup(Default = "Direct Command 36")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 35)]
#endif
        DirectCmds_36,
#if FLEXAPI
        [Offset(998, 1)]
        [DescriptionLookup(Default = "Direct Command 37")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 36)]
#endif
        DirectCmds_37,
#if FLEXAPI
        [Offset(999, 1)]
        [DescriptionLookup(Default = "Direct Command 38")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 37)]
#endif
        DirectCmds_38,
#if FLEXAPI
        [Offset(1000, 1)]
        [DescriptionLookup(Default = "Direct Command 39")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 38)]
#endif
        DirectCmds_39,
#if FLEXAPI
        [Offset(1001, 1)]
        [DescriptionLookup(Default = "Direct Command 40")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 39)]
#endif
        DirectCmds_40,
#if FLEXAPI
        [Offset(1002, 1)]
        [DescriptionLookup(Default = "Direct Command 41")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 40)]
#endif
        DirectCmds_41,
#if FLEXAPI
        [Offset(1003, 1)]
        [DescriptionLookup(Default = "Direct Command 42")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 41)]
#endif
        DirectCmds_42,
#if FLEXAPI
        [Offset(1004, 1)]
        [DescriptionLookup(Default = "Direct Command 43")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 42)]
#endif
        DirectCmds_43,
#if FLEXAPI
        [Offset(1005, 1)]
        [DescriptionLookup(Default = "Direct Command 44")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 43)]
#endif
        DirectCmds_44,
#if FLEXAPI
        [Offset(1006, 1)]
        [DescriptionLookup(Default = "Direct Command 45")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 44)]
#endif
        DirectCmds_45,
#if FLEXAPI
        [Offset(1007, 1)]
        [DescriptionLookup(Default = "Direct Command 46")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 45)]
#endif
        DirectCmds_46,
#if FLEXAPI
        [Offset(1008, 1)]
        [DescriptionLookup(Default = "Direct Command 47")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 46)]
#endif
        DirectCmds_47,
#if FLEXAPI
        [Offset(1009, 1)]
        [DescriptionLookup(Default = "Direct Command 48")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 47)]
#endif
        DirectCmds_48,
#if FLEXAPI
        [Offset(1010, 1)]
        [DescriptionLookup(Default = "Direct Command 49")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 48)]
#endif
        DirectCmds_49,
#if FLEXAPI
        [Offset(1011, 1)]
        [DescriptionLookup(Default = "Direct Command 50")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 49)]
#endif
        DirectCmds_50,
#if FLEXAPI
        [Offset(1015, 3)]
        [Description("Schedule Report")]
        [Tree("Schedules", ControlType.CheckBox, Assets.Schedules, 9, 3)]
#endif
        Schedule_Report,
#if FLEXAPI
        [Offset(1018, 3)]
        [Description("Schedule Batch Commands")]
        [Tree("Schedules", ControlType.CheckBox, Assets.Schedules, 9, 1)]
#endif
        Schedule_Batch,
#if FLEXAPI
        [Offset(1021, 3)]
        [Description("Schedule Archives")]
        [Tree("Schedules", ControlType.CheckBox, Assets.Schedules, 9, 0)]
#endif
        Schedule_Archive,
#if FLEXAPI
        [Offset(1024, 3)]
        [Description("Schedule Downloads")]
        [Tree("Schedules", ControlType.CheckBox, Assets.Schedules, 9, 2)]
#endif
        Schedule_Downloads,
#if FLEXAPI
        [Offset(1046, 1)]
        [Description("Allow Do Not Show Again")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 11)]
#endif
        AllowDoNotShowAgain,
#if FLEXAPI
        [Offset(1047, 1)]
        [Description("Allow Do Not Ask Again")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 10)]
#endif
        AllowDoNotAskAgain,
#if FLEXAPI
        [Offset(1141, 1)]
        [Description("Normal")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 0)]
#endif
        CardType_Perm,
#if FLEXAPI
        [Offset(1142, 1)]
        [Description("Visitor")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 1)]
#endif
        CardType_Visitor,
#if FLEXAPI
        [Offset(1143, 1)]
        [Description("Temporary")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 2)]
#endif
        CardType_Temp,
#if FLEXAPI
        [Offset(1144, 1)]
        [Description("Contractor")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 4)]
#endif
        CardType_Contractor,
#if FLEXAPI
        [Offset(1145, 1)]
        [Description("Vendor")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 5)]
#endif
        CardType_Vendor,
#if FLEXAPI
        [Offset(1146, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "6", Default = "Custom 1")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 6)]
#endif
        CardType_Custom1,
#if FLEXAPI
        [Offset(1147, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "7", Default = "Custom 2")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 7)]
#endif
        CardType_Custom2,
#if FLEXAPI
        [Offset(1148, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "8", Default = "Custom 3")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 8)]
#endif
        CardType_Custom3,
#if FLEXAPI
        [Offset(1149, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "9", Default = "Custom 4")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 9)]
#endif
        CardType_Custom4,
#if FLEXAPI
        [Offset(1150, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "10", Default = "Custom 5")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 10)]
#endif
        CardType_Custom5,
#if FLEXAPI
        [Offset(1151, 1)]
        [Description("Disabled")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 3)]
#endif
        CardType_Disabled,
#if FLEXAPI
        [Offset(1152, 1)]
        [Description("IPVideo 1")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 10)]
#endif
        ViewIPVideo,
#if FLEXAPI
        [Offset(1153, 1)]
        [Description("Access Levels")]
        [Tree("Views", ControlType.CheckBox, Assets.Views, 5, 2)]
#endif
        ShowAccessLevels,
#if FLEXAPI
        [Offset(1154, 1)]
        [Description("Add or Edit Access Level Groups")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 9)]
#endif
        AllowEditAxsLvlGroup,
#if FLEXAPI
        [Offset(1155, 1)]
        [Description("View Access Level Groups")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 10)]
#endif
        ViewHdwAxsLvlGroup,
#if FLEXAPI
        [Offset(1157, 1)]
        [Description("Restored Acknowledged Alarms")]
        [Tree("Reports\\Restored Archive Data", ControlType.CheckBox, Assets.Reports, 4, 5, 0)]
#endif
        RESTORED_ACKNOWLEDGEDALARMS_R,
#if FLEXAPI
        [Offset(1158, 1)]
        [Description("Restored Audit Trail")]
        [Tree("Reports\\Restored Archive Data", ControlType.CheckBox, Assets.Reports, 4, 5, 1)]
#endif
        RESTORED_AUDITTRAIL_R,
#if FLEXAPI
        [Offset(1159, 1)]
        [Description("Restored Event History")]
        [Tree("Reports\\Restored Archive Data", ControlType.CheckBox, Assets.Reports, 4, 5, 2)]
#endif
        RESTORED_TRANSACTIONS_R,
#if FLEXAPI
        [Offset(1161, 1)]
        [Description("Connect")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 2)]
#endif
        SSPConnect,
#if FLEXAPI
        [Offset(1162, 1)]
        [Description("Disconnect")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 3)]
#endif
        SSPDisconnect,
#if FLEXAPI
        [Offset(1163, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Monitor Point", ControlType.CheckBox, Assets.MonitorPoint, Assets.Controller, 0, 0, 2)]
#endif
        RqrCtrlTextMP,
#if FLEXAPI
        [Offset(1164, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Control Point", ControlType.CheckBox, Assets.ControlPoint, Assets.Controller, 0, 1, 2)]
#endif
        RqrCtrlTextCP,
#if FLEXAPI
        [Offset(1165, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 5)]
#endif
        RqrCtrlTextACM,
#if FLEXAPI
        [Offset(1166, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 5)]
#endif
        RqrCtrlTextACMH,
#if FLEXAPI
        [Offset(1167, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 5)]
#endif
        RqrCtrlTextACMM,
#if FLEXAPI
        [Offset(1168, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 5)]
#endif
        RqrCtrlTextACML,
#if FLEXAPI
        [Offset(1169, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\MPG", ControlType.CheckBox, Assets.MPG, Assets.Controller, 0, 6, 2)]
#endif
        RqrCtrlTextMPG,
#if FLEXAPI
        [Offset(1170, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Access Area", ControlType.CheckBox, Assets.AccessArea, Assets.Controller, 0, 5, 2)]
#endif
        RqrCtrlTextArea,
#if FLEXAPI
        [Offset(1171, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Time Schedules", ControlType.CheckBox, Assets.TimeSchedule, Assets.Controller, 0, 3, 1)]
#endif
        RqrCtrlTextTS,
#if FLEXAPI
        [Offset(1172, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Triggers & Macros", ControlType.CheckBox, Assets.TriggersAndMacros, Assets.Controller, 0, 7, 2)]
#endif
        RqrCtrlTextMacro,
#if FLEXAPI
        [Offset(1173, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 6)]
#endif
        RqrCtrlTextSSP,
#if FLEXAPI
        [Offset(1177, 1)]
        [Description("Require Text on Control")]
        [Tree("Hardware\\Universal Driver", ControlType.CheckBox, Assets.UniversalDriver, Assets.Controller, 0, 12, 2)]
#endif
        RqrCtrlTextUAD,
#if FLEXAPI
        [Offset(1178, 1)]
        [Description("Require Text on Control")]
        [Tree("Direct Commands", ControlType.CheckBox, Assets.DirectCommands, 8, 1)]
#endif
        RqrCtrlTextMisc,
#if FLEXAPI
        [Offset(1179, 1)]
        [DescriptionLookup(Default = "Direct Command 51")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 50)]
#endif
        DirectCmds_51,
#if FLEXAPI
        [Offset(1180, 1)]
        [DescriptionLookup(Default = "Direct Command 52")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 51)]
#endif
        DirectCmds_52,
#if FLEXAPI
        [Offset(1181, 1)]
        [DescriptionLookup(Default = "Direct Command 53")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 52)]
#endif
        DirectCmds_53,
#if FLEXAPI
        [Offset(1182, 1)]
        [DescriptionLookup(Default = "Direct Command 54")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 53)]
#endif
        DirectCmds_54,
#if FLEXAPI
        [Offset(1183, 1)]
        [DescriptionLookup(Default = "Direct Command 55")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 54)]
#endif
        DirectCmds_55,
#if FLEXAPI
        [Offset(1184, 1)]
        [DescriptionLookup(Default = "Direct Command 56")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 55)]
#endif
        DirectCmds_56,
#if FLEXAPI
        [Offset(1185, 1)]
        [DescriptionLookup(Default = "Direct Command 57")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 56)]
#endif
        DirectCmds_57,
#if FLEXAPI
        [Offset(1186, 1)]
        [DescriptionLookup(Default = "Direct Command 58")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 57)]
#endif
        DirectCmds_58,
#if FLEXAPI
        [Offset(1187, 1)]
        [DescriptionLookup(Default = "Direct Command 59")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 58)]
#endif
        DirectCmds_59,
#if FLEXAPI
        [Offset(1188, 1)]
        [DescriptionLookup(Default = "Direct Command 60")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 59)]
#endif
        DirectCmds_60,
#if FLEXAPI
        [Offset(1189, 1)]
        [DescriptionLookup(Default = "Direct Command 61")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 60)]
#endif
        DirectCmds_61,
#if FLEXAPI
        [Offset(1190, 1)]
        [DescriptionLookup(Default = "Direct Command 62")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 61)]
#endif
        DirectCmds_62,
#if FLEXAPI
        [Offset(1191, 1)]
        [DescriptionLookup(Default = "Direct Command 63")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 62)]
#endif
        DirectCmds_63,
#if FLEXAPI
        [Offset(1192, 1)]
        [DescriptionLookup(Default = "Direct Command 64")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 63)]
#endif
        DirectCmds_64,
#if FLEXAPI
        [Offset(1193, 1)]
        [DescriptionLookup(Default = "Direct Command 65")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 64)]
#endif
        DirectCmds_65,
#if FLEXAPI
        [Offset(1194, 1)]
        [DescriptionLookup(Default = "Direct Command 66")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 65)]
#endif
        DirectCmds_66,
#if FLEXAPI
        [Offset(1195, 1)]
        [DescriptionLookup(Default = "Direct Command 67")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 66)]
#endif
        DirectCmds_67,
#if FLEXAPI
        [Offset(1196, 1)]
        [DescriptionLookup(Default = "Direct Command 68")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 67)]
#endif
        DirectCmds_68,
#if FLEXAPI
        [Offset(1197, 1)]
        [DescriptionLookup(Default = "Direct Command 69")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 68)]
#endif
        DirectCmds_69,
#if FLEXAPI
        [Offset(1198, 1)]
        [DescriptionLookup(Default = "Direct Command 70")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 69)]
#endif
        DirectCmds_70,
#if FLEXAPI
        [Offset(1199, 1)]
        [DescriptionLookup(Default = "Direct Command 71")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 70)]
#endif
        DirectCmds_71,
#if FLEXAPI
        [Offset(1200, 1)]
        [DescriptionLookup(Default = "Direct Command 72")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 71)]
#endif
        DirectCmds_72,
#if FLEXAPI
        [Offset(1201, 1)]
        [DescriptionLookup(Default = "Direct Command 73")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 72)]
#endif
        DirectCmds_73,
#if FLEXAPI
        [Offset(1202, 1)]
        [DescriptionLookup(Default = "Direct Command 74")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 73)]
#endif
        DirectCmds_74,
#if FLEXAPI
        [Offset(1203, 1)]
        [DescriptionLookup(Default = "Direct Command 75")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 74)]
#endif
        DirectCmds_75,
#if FLEXAPI
        [Offset(1204, 1)]
        [DescriptionLookup(Default = "Direct Command 76")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 75)]
#endif
        DirectCmds_76,
#if FLEXAPI
        [Offset(1205, 1)]
        [DescriptionLookup(Default = "Direct Command 77")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 76)]
#endif
        DirectCmds_77,
#if FLEXAPI
        [Offset(1206, 1)]
        [DescriptionLookup(Default = "Direct Command 78")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 77)]
#endif
        DirectCmds_78,
#if FLEXAPI
        [Offset(1207, 1)]
        [DescriptionLookup(Default = "Direct Command 79")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 78)]
#endif
        DirectCmds_79,
#if FLEXAPI
        [Offset(1208, 1)]
        [DescriptionLookup(Default = "Direct Command 80")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 79)]
#endif
        DirectCmds_80,
#if FLEXAPI
        [Offset(1209, 1)]
        [DescriptionLookup(Default = "Direct Command 81")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 80)]
#endif
        DirectCmds_81,
#if FLEXAPI
        [Offset(1210, 1)]
        [DescriptionLookup(Default = "Direct Command 82")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 81)]
#endif
        DirectCmds_82,
#if FLEXAPI
        [Offset(1211, 1)]
        [DescriptionLookup(Default = "Direct Command 83")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 82)]
#endif
        DirectCmds_83,
#if FLEXAPI
        [Offset(1212, 1)]
        [DescriptionLookup(Default = "Direct Command 84")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 83)]
#endif
        DirectCmds_84,
#if FLEXAPI
        [Offset(1213, 1)]
        [DescriptionLookup(Default = "Direct Command 85")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 84)]
#endif
        DirectCmds_85,
#if FLEXAPI
        [Offset(1214, 1)]
        [DescriptionLookup(Default = "Direct Command 86")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 85)]
#endif
        DirectCmds_86,
#if FLEXAPI
        [Offset(1215, 1)]
        [DescriptionLookup(Default = "Direct Command 87")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 86)]
#endif
        DirectCmds_87,
#if FLEXAPI
        [Offset(1216, 1)]
        [DescriptionLookup(Default = "Direct Command 88")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 87)]
#endif
        DirectCmds_88,
#if FLEXAPI
        [Offset(1217, 1)]
        [DescriptionLookup(Default = "Direct Command 89")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 88)]
#endif
        DirectCmds_89,
#if FLEXAPI
        [Offset(1218, 1)]
        [DescriptionLookup(Default = "Direct Command 90")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 89)]
#endif
        DirectCmds_90,
#if FLEXAPI
        [Offset(1219, 1)]
        [DescriptionLookup(Default = "Direct Command 91")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 90)]
#endif
        DirectCmds_91,
#if FLEXAPI
        [Offset(1220, 1)]
        [DescriptionLookup(Default = "Direct Command 92")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 91)]
#endif
        DirectCmds_92,
#if FLEXAPI
        [Offset(1221, 1)]
        [DescriptionLookup(Default = "Direct Command 93")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 92)]
#endif
        DirectCmds_93,
#if FLEXAPI
        [Offset(1222, 1)]
        [DescriptionLookup(Default = "Direct Command 94")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 93)]
#endif
        DirectCmds_94,
#if FLEXAPI
        [Offset(1223, 1)]
        [DescriptionLookup(Default = "Direct Command 95")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 94)]
#endif
        DirectCmds_95,
#if FLEXAPI
        [Offset(1224, 1)]
        [DescriptionLookup(Default = "Direct Command 96")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 95)]
#endif
        DirectCmds_96,
#if FLEXAPI
        [Offset(1225, 1)]
        [DescriptionLookup(Default = "Direct Command 97")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 96)]
#endif
        DirectCmds_97,
#if FLEXAPI
        [Offset(1226, 1)]
        [DescriptionLookup(Default = "Direct Command 98")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 97)]
#endif
        DirectCmds_98,
#if FLEXAPI
        [Offset(1227, 1)]
        [DescriptionLookup(Default = "Direct Command 99")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 98)]
#endif
        DirectCmds_99,
#if FLEXAPI
        [Offset(1228, 1)]
        [DescriptionLookup(Default = "Direct Command 100")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 99)]
#endif
        DirectCmds_100,
#if FLEXAPI
        [Offset(1229, 1)]
        [Description("Allow Situation Severe")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 12)]
#endif
        SituationSevere,
#if FLEXAPI
        [Offset(1230, 1)]
        [Description("Allow Situation High")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 13)]
#endif
        SituationHigh,
#if FLEXAPI
        [Offset(1231, 1)]
        [Description("Allow Situation Elevated")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 14)]
#endif
        SituationElevated,
#if FLEXAPI
        [Offset(1232, 1)]
        [Description("Allow Situation Guarded")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 15)]
#endif
        SituationGuarded,
#if FLEXAPI
        [Offset(1233, 1)]
        [Description("Allow Situation Low")]
        [Tree("Actions", ControlType.CheckBox, Assets.Actions, 6, 16)]
#endif
        SituationLow,
#if FLEXAPI
        [Offset(1234, 2)]
        [Description("Auto Activate Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 26)]
#endif
        CardField27,
#if FLEXAPI
        [Offset(1236, 2)]
        [Description("Auto Deactivate Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 27)]
#endif
        CardField28,
#if FLEXAPI
        [Offset(1238, 2)]
        [Description("Time/Attendance Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 28)]
#endif
        CardField29,
#if FLEXAPI
        [Offset(1240, 2)]
        [Description("Always Download")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 29)]
#endif
        CardField30,
#if FLEXAPI
        [Offset(1242, 2)]
        [Description("ASSA Credential Format")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 30)]
#endif
        CardField31,
#if FLEXAPI
        [Offset(1244, 2)]
        [Description("ASSA Facility Code")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 31)]
#endif
        CardField32,
#if FLEXAPI
        [Offset(1256, 2)]
        [Description("Personnel Types")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 4)]
#endif
        PersonnelField102,
#if FLEXAPI
        [Offset(1258, 1)]
        [Description("Create Filters")]
        [Tree("Filters", ControlType.CheckBox, Assets.Filters, 11, 0)]
#endif
        CreateRoutes,
#if FLEXAPI
        [Offset(1259, 1)]
        [Description("Assign Filters")]
        [Tree("Filters", ControlType.CheckBox, Assets.Filters, 11, 1)]
#endif
        AssignRoutes,
#if FLEXAPI
        [Offset(1260, 1)]
        [Description("Allow Door Follows Schedule Assignment")]
        [Tree("Hardware\\Door\\Normal", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 5, 8)]
#endif
        AllowFollowsTimeSchedule,
#if FLEXAPI
        [Offset(1261, 1)]
        [Description("Allow Door Follows Schedule Assignment")]
        [Tree("Hardware\\Door\\Low Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 8, 8)]
#endif
        AllowFollowsTSLow,
#if FLEXAPI
        [Offset(1262, 1)]
        [Description("Allow Door Follows Schedule Assignment")]
        [Tree("Hardware\\Door\\Medium Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 7, 8)]
#endif
        AllowFollowsTSMedium,
#if FLEXAPI
        [Offset(1263, 1)]
        [Description("Allow Door Follows Schedule Assignment")]
        [Tree("Hardware\\Door\\High Security", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 6, 8)]
#endif
        AllowFollowsTSHigh,
#if FLEXAPI
        [Offset(1265, 1)]
        [Description("Door Follows Time Schedule")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 6)]
#endif
        OTHERREPORTS_DFTZ_R,
#if FLEXAPI
        [Offset(1266, 1)]
        [Description("Timed Events")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 7)]
#endif
        OTHERREPORTS_TIMEDEVENTS_R,
#if FLEXAPI
        [Offset(1267, 1)]
        [Description("Extended")]
        [Tree("Personnel\\Card Types", ControlType.CheckBox, Assets.Personnel, 2, 7, 11)]
#endif
        CardType_Extended,
#if FLEXAPI
        [Offset(1268, 1)]
        [Description("Allow Remove All Access")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 12)]
#endif
        RemoveAllAccess,
#if FLEXAPI
        [Offset(1269, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (High)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 12)]
#endif
        AxsLvl_High_ConfirmDD,
#if FLEXAPI
        [Offset(1270, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Med)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 13)]
#endif
        AxsLvl_Med_ConfirmDD,
#if FLEXAPI
        [Offset(1271, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Low)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 14)]
#endif
        AxsLvl_Low_ConfirmDD,
#if FLEXAPI
        [Offset(1272, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Normal)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 11)]
#endif
        AxsLvl_Normal_ConfirmDD,
#if FLEXAPI
        [Offset(1273, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Custom 1)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 15)]
#endif
        AxsLvl_Custom1_ConfirmDD,
#if FLEXAPI
        [Offset(1274, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Custom 2)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 16)]
#endif
        AxsLvl_Custom2_ConfirmDD,
#if FLEXAPI
        [Offset(1275, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Custom 3)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 17)]
#endif
        AxsLvl_Custom3_ConfirmDD,
#if FLEXAPI
        [Offset(1276, 1)]
        [Description("Force Drag & Drop Confirmation on Access Level (Custom 4)")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 18)]
#endif
        AxsLvl_Custom4_ConfirmDD,
#if FLEXAPI
        [Offset(1277, 1)]
        [Description("Use Custom XML Permissions")]
        [Tree("Personnel\\Advanced Custom Settings", ControlType.CheckBox, Assets.Personnel, 2, 10, 0)]
#endif
        UseCustomXMLPermissions,
#if FLEXAPI
        [Offset(1278, 1)]
        [Description("Normal")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 0)]
#endif
        CardType_Perm_Assign,
#if FLEXAPI
        [Offset(1279, 1)]
        [Description("Visitor")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 1)]
#endif
        CardType_Visitor_Assign,
#if FLEXAPI
        [Offset(1280, 1)]
        [Description("Temporary")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 2)]
#endif
        CardType_Temp_Assign,
#if FLEXAPI
        [Offset(1281, 1)]
        [Description("Contractor")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 4)]
#endif
        CardType_Contractor_Assign,
#if FLEXAPI
        [Offset(1282, 1)]
        [Description("Vendor")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 5)]
#endif
        CardType_Vendor_Assign,
#if FLEXAPI
        [Offset(1283, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "6", Default = "Custom 1")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 6)]
#endif
        CardType_Custom1_Assign,
#if FLEXAPI
        [Offset(1284, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "7", Default = "Custom 2")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 7)]
#endif
        CardType_Custom2_Assign,
#if FLEXAPI
        [Offset(1285, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "8", Default = "Custom 3")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 8)]
#endif
        CardType_Custom3_Assign,
#if FLEXAPI
        [Offset(1286, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "9", Default = "Custom 4")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 9)]
#endif
        CardType_Custom4_Assign,
#if FLEXAPI
        [Offset(1287, 1)]
        [DescriptionLookup(Lookup = typeof(CustomCardTypesLookup), Key = "10", Default = "Custom 5")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 10)]
#endif
        CardType_Custom5_Assign,
#if FLEXAPI
        [Offset(1288, 1)]
        [Description("Disabled")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 3)]
#endif
        CardType_Disabled_Assign,
#if FLEXAPI
        [Offset(1289, 1)]
        [Description("Extended")]
        [Tree("Personnel\\Assign Card Types", ControlType.CheckBox, Assets.Personnel, 2, 8, 11)]
#endif
        CardType_Extended_Assign,
#if FLEXAPI
        [Offset(1290, 1)]
        [Description("NORMAL")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 7)]
#endif
        PersonnelType_Perm_Assign,
#if FLEXAPI
        [Offset(1291, 1)]
        [Description("Visitor")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 10)]
#endif
        PersonnelType_Visitor_Assign,
#if FLEXAPI
        [Offset(1292, 1)]
        [Description("Temp")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 8)]
#endif
        PersonnelType_Temp_Assign,
#if FLEXAPI
        [Offset(1293, 1)]
        [Description("Contractor")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 0)]
#endif
        PersonnelType_Contractor_Assign,
#if FLEXAPI
        [Offset(1294, 1)]
        [Description("Vendor")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 9)]
#endif
        PersonnelType_Vendor_Assign,
#if FLEXAPI
        [Offset(1295, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 0", Default = "Custom 1")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 1)]
#endif
        PersonnelType_Custom1_Assign,
#if FLEXAPI
        [Offset(1296, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 1", Default = "Custom 2")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 2)]
#endif
        PersonnelType_Custom2_Assign,
#if FLEXAPI
        [Offset(1297, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 2", Default = "Custom 3")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 3)]
#endif
        PersonnelType_Custom3_Assign,
#if FLEXAPI
        [Offset(1298, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 3", Default = "Custom 4")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 4)]
#endif
        PersonnelType_Custom4_Assign,
#if FLEXAPI
        [Offset(1299, 1)]
        [DescriptionLookup(Lookup = typeof(SettingsLookup), Key = "Custom Person Type 4", Default = "Custom 5")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 5)]
#endif
        PersonnelType_Custom5_Assign,
#if FLEXAPI
        [Offset(1300, 1)]
        [Description("Disabled")]
        [Tree("Personnel\\Assign Personnel Types", ControlType.CheckBox, Assets.Personnel, 2, 6, 6)]
#endif
        PersonnelType_Disabled_Assign,
#if FLEXAPI
        [Offset(1301, 1)]
        [Description("Use CardType for Deactivation Date")]
        [Tree("Personnel\\Advanced Custom Settings", ControlType.CheckBox, Assets.Personnel, 2, 10, 2)]
#endif
        UseCardTypeDeactivation,
#if FLEXAPI
        [Offset(1302, 1)]
        [Description("Perform Advanced Access Level Check")]
        [Tree("Personnel\\Advanced Custom Settings", ControlType.CheckBox, Assets.Personnel, 2, 10, 1)]
#endif
        AdvancedAccessLevel_Check,
#if FLEXAPI
        [Offset(1303, 1)]
        [Secondary(1304, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Holidays\\Holiday", ControlType.ComboBox, Assets.TimeSchedule, Assets.Controller, 0, 4, 1, 0)]
#endif
        AllowEditHoliday,
#if FLEXAPI
        [Offset(1305, 1)]
        [Description("Location: Allow Configure Drop-Down List")]
        [Tree("Personnel\\Personnel Fields", ControlType.CheckBox, Assets.Personnel, 2, 2, 8)]
#endif
        PersonnelField_Location_List,
#if FLEXAPI
        [Offset(1306, 1)]
        [Description("Department: Allow Configure Drop-Down List")]
        [Tree("Personnel\\Personnel Fields", ControlType.CheckBox, Assets.Personnel, 2, 2, 10)]
#endif
        PersonnelField_Department_List,
#if FLEXAPI
        [Offset(1307, 1)]
        [Description("Site: Allow Configure Drop-Down List")]
        [Tree("Personnel\\Personnel Fields", ControlType.CheckBox, Assets.Personnel, 2, 2, 12)]
#endif
        PersonnelField_Site_List,
#if FLEXAPI
        [Offset(1308, 1)]
        [Description("Title: Allow Configure Drop-Down List")]
        [Tree("Personnel\\Personnel Fields", ControlType.CheckBox, Assets.Personnel, 2, 2, 14)]
#endif
        PersonnelField_Title_List,
#if FLEXAPI
        [Offset(1309, 1)]
        [Description("Allow Biometric Enrollment")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 13)]
#endif
        AllowBioEnroll,
#if FLEXAPI
        [Offset(1310, 1)]
        [Description("Allow Removal of Biometric Templates")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 14)]
#endif
        AllowRemoveBioTemplate,
#if FLEXAPI
        [Offset(1311, 1)]
        [Secondary(1312, 1)]
        [Description("PSIA Permissions")]
        [Tree("Hardware\\Controller", ControlType.ComboBox, Assets.Controller, 0, 8, 12)]
#endif
        AllowEditPSIAPermissions,
#if FLEXAPI
        [Offset(1313, 1)]
        [Secondary(1314, 1)]
        [Description("Web Service Credentials")]
        [Tree("Hardware\\Controller", ControlType.ComboBox, Assets.Controller, 0, 8, 13)]
#endif
        AllowEditPSIALogins,
#if FLEXAPI
        [Offset(1315, 1)]
        [DescriptionLookup(Default = "Direct Command 101")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 100)]
#endif
        DirectCmds_101,
#if FLEXAPI
        [Offset(1316, 1)]
        [DescriptionLookup(Default = "Direct Command 102")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 101)]
#endif
        DirectCmds_102,
#if FLEXAPI
        [Offset(1317, 1)]
        [DescriptionLookup(Default = "Direct Command 103")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 102)]
#endif
        DirectCmds_103,
#if FLEXAPI
        [Offset(1318, 1)]
        [DescriptionLookup(Default = "Direct Command 104")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 103)]
#endif
        DirectCmds_104,
#if FLEXAPI
        [Offset(1319, 1)]
        [DescriptionLookup(Default = "Direct Command 105")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 104)]
#endif
        DirectCmds_105,
#if FLEXAPI
        [Offset(1320, 1)]
        [DescriptionLookup(Default = "Direct Command 106")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 105)]
#endif
        DirectCmds_106,
#if FLEXAPI
        [Offset(1321, 1)]
        [DescriptionLookup(Default = "Direct Command 107")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 106)]
#endif
        DirectCmds_107,
#if FLEXAPI
        [Offset(1322, 1)]
        [DescriptionLookup(Default = "Direct Command 108")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 107)]
#endif
        DirectCmds_108,
#if FLEXAPI
        [Offset(1323, 1)]
        [DescriptionLookup(Default = "Direct Command 109")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 108)]
#endif
        DirectCmds_109,
#if FLEXAPI
        [Offset(1324, 1)]
        [DescriptionLookup(Default = "Direct Command 110")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 109)]
#endif
        DirectCmds_110,
#if FLEXAPI
        [Offset(1325, 1)]
        [DescriptionLookup(Default = "Direct Command 111")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 110)]
#endif
        DirectCmds_111,
#if FLEXAPI
        [Offset(1326, 1)]
        [DescriptionLookup(Default = "Direct Command 112")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 111)]
#endif
        DirectCmds_112,
#if FLEXAPI
        [Offset(1327, 1)]
        [DescriptionLookup(Default = "Direct Command 113")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 112)]
#endif
        DirectCmds_113,
#if FLEXAPI
        [Offset(1328, 1)]
        [DescriptionLookup(Default = "Direct Command 114")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 113)]
#endif
        DirectCmds_114,
#if FLEXAPI
        [Offset(1329, 1)]
        [DescriptionLookup(Default = "Direct Command 115")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 114)]
#endif
        DirectCmds_115,
#if FLEXAPI
        [Offset(1330, 1)]
        [DescriptionLookup(Default = "Direct Command 116")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 115)]
#endif
        DirectCmds_116,
#if FLEXAPI
        [Offset(1331, 1)]
        [DescriptionLookup(Default = "Direct Command 117")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 116)]
#endif
        DirectCmds_117,
#if FLEXAPI
        [Offset(1332, 1)]
        [DescriptionLookup(Default = "Direct Command 118")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 117)]
#endif
        DirectCmds_118,
#if FLEXAPI
        [Offset(1333, 1)]
        [DescriptionLookup(Default = "Direct Command 119")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 118)]
#endif
        DirectCmds_119,
#if FLEXAPI
        [Offset(1334, 1)]
        [DescriptionLookup(Default = "Direct Command 120")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 119)]
#endif
        DirectCmds_120,
#if FLEXAPI
        [Offset(1335, 1)]
        [DescriptionLookup(Default = "Direct Command 121")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 120)]
#endif
        DirectCmds_121,
#if FLEXAPI
        [Offset(1336, 1)]
        [DescriptionLookup(Default = "Direct Command 122")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 121)]
#endif
        DirectCmds_122,
#if FLEXAPI
        [Offset(1337, 1)]
        [DescriptionLookup(Default = "Direct Command 123")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 122)]
#endif
        DirectCmds_123,
#if FLEXAPI
        [Offset(1338, 1)]
        [DescriptionLookup(Default = "Direct Command 124")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 123)]
#endif
        DirectCmds_124,
#if FLEXAPI
        [Offset(1339, 1)]
        [DescriptionLookup(Default = "Direct Command 125")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 124)]
#endif
        DirectCmds_125,
#if FLEXAPI
        [Offset(1340, 1)]
        [DescriptionLookup(Default = "Direct Command 126")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 125)]
#endif
        DirectCmds_126,
#if FLEXAPI
        [Offset(1341, 1)]
        [DescriptionLookup(Default = "Direct Command 127")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 126)]
#endif
        DirectCmds_127,
#if FLEXAPI
        [Offset(1342, 1)]
        [DescriptionLookup(Default = "Direct Command 128")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 127)]
#endif
        DirectCmds_128,
#if FLEXAPI
        [Offset(1343, 1)]
        [DescriptionLookup(Default = "Direct Command 129")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 128)]
#endif
        DirectCmds_129,
#if FLEXAPI
        [Offset(1344, 1)]
        [DescriptionLookup(Default = "Direct Command 130")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 129)]
#endif
        DirectCmds_130,
#if FLEXAPI
        [Offset(1345, 1)]
        [DescriptionLookup(Default = "Direct Command 131")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 130)]
#endif
        DirectCmds_131,
#if FLEXAPI
        [Offset(1346, 1)]
        [DescriptionLookup(Default = "Direct Command 132")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 131)]
#endif
        DirectCmds_132,
#if FLEXAPI
        [Offset(1347, 1)]
        [DescriptionLookup(Default = "Direct Command 133")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 132)]
#endif
        DirectCmds_133,
#if FLEXAPI
        [Offset(1348, 1)]
        [DescriptionLookup(Default = "Direct Command 134")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 133)]
#endif
        DirectCmds_134,
#if FLEXAPI
        [Offset(1349, 1)]
        [DescriptionLookup(Default = "Direct Command 135")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 134)]
#endif
        DirectCmds_135,
#if FLEXAPI
        [Offset(1350, 1)]
        [DescriptionLookup(Default = "Direct Command 136")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 135)]
#endif
        DirectCmds_136,
#if FLEXAPI
        [Offset(1351, 1)]
        [DescriptionLookup(Default = "Direct Command 137")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 136)]
#endif
        DirectCmds_137,
#if FLEXAPI
        [Offset(1352, 1)]
        [DescriptionLookup(Default = "Direct Command 138")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 137)]
#endif
        DirectCmds_138,
#if FLEXAPI
        [Offset(1353, 1)]
        [DescriptionLookup(Default = "Direct Command 139")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 138)]
#endif
        DirectCmds_139,
#if FLEXAPI
        [Offset(1354, 1)]
        [DescriptionLookup(Default = "Direct Command 140")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 139)]
#endif
        DirectCmds_140,
#if FLEXAPI
        [Offset(1355, 1)]
        [DescriptionLookup(Default = "Direct Command 141")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 140)]
#endif
        DirectCmds_141,
#if FLEXAPI
        [Offset(1356, 1)]
        [DescriptionLookup(Default = "Direct Command 142")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 141)]
#endif
        DirectCmds_142,
#if FLEXAPI
        [Offset(1357, 1)]
        [DescriptionLookup(Default = "Direct Command 143")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 142)]
#endif
        DirectCmds_143,
#if FLEXAPI
        [Offset(1358, 1)]
        [DescriptionLookup(Default = "Direct Command 144")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 143)]
#endif
        DirectCmds_144,
#if FLEXAPI
        [Offset(1359, 1)]
        [DescriptionLookup(Default = "Direct Command 145")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 144)]
#endif
        DirectCmds_145,
#if FLEXAPI
        [Offset(1360, 1)]
        [DescriptionLookup(Default = "Direct Command 146")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 145)]
#endif
        DirectCmds_146,
#if FLEXAPI
        [Offset(1361, 1)]
        [DescriptionLookup(Default = "Direct Command 147")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 146)]
#endif
        DirectCmds_147,
#if FLEXAPI
        [Offset(1362, 1)]
        [DescriptionLookup(Default = "Direct Command 148")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 147)]
#endif
        DirectCmds_148,
#if FLEXAPI
        [Offset(1363, 1)]
        [DescriptionLookup(Default = "Direct Command 149")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 148)]
#endif
        DirectCmds_149,
#if FLEXAPI
        [Offset(1364, 1)]
        [DescriptionLookup(Default = "Direct Command 150")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 149)]
#endif
        DirectCmds_150,
#if FLEXAPI
        [Offset(1365, 1)]
        [DescriptionLookup(Default = "Direct Command 151")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 150)]
#endif
        DirectCmds_151,
#if FLEXAPI
        [Offset(1366, 1)]
        [DescriptionLookup(Default = "Direct Command 152")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 151)]
#endif
        DirectCmds_152,
#if FLEXAPI
        [Offset(1367, 1)]
        [DescriptionLookup(Default = "Direct Command 153")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 152)]
#endif
        DirectCmds_153,
#if FLEXAPI
        [Offset(1368, 1)]
        [DescriptionLookup(Default = "Direct Command 154")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 153)]
#endif
        DirectCmds_154,
#if FLEXAPI
        [Offset(1369, 1)]
        [DescriptionLookup(Default = "Direct Command 155")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 154)]
#endif
        DirectCmds_155,
#if FLEXAPI
        [Offset(1370, 1)]
        [DescriptionLookup(Default = "Direct Command 156")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 155)]
#endif
        DirectCmds_156,
#if FLEXAPI
        [Offset(1371, 1)]
        [DescriptionLookup(Default = "Direct Command 157")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 156)]
#endif
        DirectCmds_157,
#if FLEXAPI
        [Offset(1372, 1)]
        [DescriptionLookup(Default = "Direct Command 158")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 157)]
#endif
        DirectCmds_158,
#if FLEXAPI
        [Offset(1373, 1)]
        [DescriptionLookup(Default = "Direct Command 159")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 158)]
#endif
        DirectCmds_159,
#if FLEXAPI
        [Offset(1374, 1)]
        [DescriptionLookup(Default = "Direct Command 160")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 159)]
#endif
        DirectCmds_160,
#if FLEXAPI
        [Offset(1375, 1)]
        [DescriptionLookup(Default = "Direct Command 161")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 160)]
#endif
        DirectCmds_161,
#if FLEXAPI
        [Offset(1376, 1)]
        [DescriptionLookup(Default = "Direct Command 162")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 161)]
#endif
        DirectCmds_162,
#if FLEXAPI
        [Offset(1377, 1)]
        [DescriptionLookup(Default = "Direct Command 163")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 162)]
#endif
        DirectCmds_163,
#if FLEXAPI
        [Offset(1378, 1)]
        [DescriptionLookup(Default = "Direct Command 164")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 163)]
#endif
        DirectCmds_164,
#if FLEXAPI
        [Offset(1379, 1)]
        [DescriptionLookup(Default = "Direct Command 165")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 164)]
#endif
        DirectCmds_165,
#if FLEXAPI
        [Offset(1380, 1)]
        [DescriptionLookup(Default = "Direct Command 166")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 165)]
#endif
        DirectCmds_166,
#if FLEXAPI
        [Offset(1381, 1)]
        [DescriptionLookup(Default = "Direct Command 167")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 166)]
#endif
        DirectCmds_167,
#if FLEXAPI
        [Offset(1382, 1)]
        [DescriptionLookup(Default = "Direct Command 168")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 167)]
#endif
        DirectCmds_168,
#if FLEXAPI
        [Offset(1383, 1)]
        [DescriptionLookup(Default = "Direct Command 169")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 168)]
#endif
        DirectCmds_169,
#if FLEXAPI
        [Offset(1384, 1)]
        [DescriptionLookup(Default = "Direct Command 170")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 169)]
#endif
        DirectCmds_170,
#if FLEXAPI
        [Offset(1385, 1)]
        [DescriptionLookup(Default = "Direct Command 171")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 170)]
#endif
        DirectCmds_171,
#if FLEXAPI
        [Offset(1386, 1)]
        [DescriptionLookup(Default = "Direct Command 172")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 171)]
#endif
        DirectCmds_172,
#if FLEXAPI
        [Offset(1387, 1)]
        [DescriptionLookup(Default = "Direct Command 173")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 172)]
#endif
        DirectCmds_173,
#if FLEXAPI
        [Offset(1388, 1)]
        [DescriptionLookup(Default = "Direct Command 174")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 173)]
#endif
        DirectCmds_174,
#if FLEXAPI
        [Offset(1389, 1)]
        [DescriptionLookup(Default = "Direct Command 175")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 174)]
#endif
        DirectCmds_175,
#if FLEXAPI
        [Offset(1390, 1)]
        [DescriptionLookup(Default = "Direct Command 176")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 175)]
#endif
        DirectCmds_176,
#if FLEXAPI
        [Offset(1391, 1)]
        [DescriptionLookup(Default = "Direct Command 177")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 176)]
#endif
        DirectCmds_177,
#if FLEXAPI
        [Offset(1392, 1)]
        [DescriptionLookup(Default = "Direct Command 178")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 177)]
#endif
        DirectCmds_178,
#if FLEXAPI
        [Offset(1393, 1)]
        [DescriptionLookup(Default = "Direct Command 179")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 178)]
#endif
        DirectCmds_179,
#if FLEXAPI
        [Offset(1394, 1)]
        [DescriptionLookup(Default = "Direct Command 180")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 179)]
#endif
        DirectCmds_180,
#if FLEXAPI
        [Offset(1395, 1)]
        [DescriptionLookup(Default = "Direct Command 181")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 180)]
#endif
        DirectCmds_181,
#if FLEXAPI
        [Offset(1396, 1)]
        [DescriptionLookup(Default = "Direct Command 182")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 181)]
#endif
        DirectCmds_182,
#if FLEXAPI
        [Offset(1397, 1)]
        [DescriptionLookup(Default = "Direct Command 183")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 182)]
#endif
        DirectCmds_183,
#if FLEXAPI
        [Offset(1398, 1)]
        [DescriptionLookup(Default = "Direct Command 184")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 183)]
#endif
        DirectCmds_184,
#if FLEXAPI
        [Offset(1399, 1)]
        [DescriptionLookup(Default = "Direct Command 185")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 184)]
#endif
        DirectCmds_185,
#if FLEXAPI
        [Offset(1400, 1)]
        [DescriptionLookup(Default = "Direct Command 186")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 185)]
#endif
        DirectCmds_186,
#if FLEXAPI
        [Offset(1401, 1)]
        [DescriptionLookup(Default = "Direct Command 187")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 186)]
#endif
        DirectCmds_187,
#if FLEXAPI
        [Offset(1402, 1)]
        [DescriptionLookup(Default = "Direct Command 188")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 187)]
#endif
        DirectCmds_188,
#if FLEXAPI
        [Offset(1403, 1)]
        [DescriptionLookup(Default = "Direct Command 189")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 188)]
#endif
        DirectCmds_189,
#if FLEXAPI
        [Offset(1404, 1)]
        [DescriptionLookup(Default = "Direct Command 190")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 189)]
#endif
        DirectCmds_190,
#if FLEXAPI
        [Offset(1405, 1)]
        [DescriptionLookup(Default = "Direct Command 191")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 190)]
#endif
        DirectCmds_191,
#if FLEXAPI
        [Offset(1406, 1)]
        [DescriptionLookup(Default = "Direct Command 192")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 191)]
#endif
        DirectCmds_192,
#if FLEXAPI
        [Offset(1407, 1)]
        [DescriptionLookup(Default = "Direct Command 193")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 192)]
#endif
        DirectCmds_193,
#if FLEXAPI
        [Offset(1408, 1)]
        [DescriptionLookup(Default = "Direct Command 194")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 193)]
#endif
        DirectCmds_194,
#if FLEXAPI
        [Offset(1409, 1)]
        [DescriptionLookup(Default = "Direct Command 195")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 194)]
#endif
        DirectCmds_195,
#if FLEXAPI
        [Offset(1410, 1)]
        [DescriptionLookup(Default = "Direct Command 196")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 195)]
#endif
        DirectCmds_196,
#if FLEXAPI
        [Offset(1411, 1)]
        [DescriptionLookup(Default = "Direct Command 197")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 196)]
#endif
        DirectCmds_197,
#if FLEXAPI
        [Offset(1412, 1)]
        [DescriptionLookup(Default = "Direct Command 198")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 197)]
#endif
        DirectCmds_198,
#if FLEXAPI
        [Offset(1413, 1)]
        [DescriptionLookup(Default = "Direct Command 199")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 198)]
#endif
        DirectCmds_199,
#if FLEXAPI
        [Offset(1414, 1)]
        [DescriptionLookup(Default = "Direct Command 200")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 199)]
#endif
        DirectCmds_200,
#if FLEXAPI
        [Offset(1415, 1)]
        [DescriptionLookup(Default = "Direct Command 201")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 200)]
#endif
        DirectCmds_201,
#if FLEXAPI
        [Offset(1416, 1)]
        [DescriptionLookup(Default = "Direct Command 202")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 201)]
#endif
        DirectCmds_202,
#if FLEXAPI
        [Offset(1417, 1)]
        [DescriptionLookup(Default = "Direct Command 203")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 202)]
#endif
        DirectCmds_203,
#if FLEXAPI
        [Offset(1418, 1)]
        [DescriptionLookup(Default = "Direct Command 204")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 203)]
#endif
        DirectCmds_204,
#if FLEXAPI
        [Offset(1419, 1)]
        [DescriptionLookup(Default = "Direct Command 205")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 204)]
#endif
        DirectCmds_205,
#if FLEXAPI
        [Offset(1420, 1)]
        [DescriptionLookup(Default = "Direct Command 206")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 205)]
#endif
        DirectCmds_206,
#if FLEXAPI
        [Offset(1421, 1)]
        [DescriptionLookup(Default = "Direct Command 207")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 206)]
#endif
        DirectCmds_207,
#if FLEXAPI
        [Offset(1422, 1)]
        [DescriptionLookup(Default = "Direct Command 208")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 207)]
#endif
        DirectCmds_208,
#if FLEXAPI
        [Offset(1423, 1)]
        [DescriptionLookup(Default = "Direct Command 209")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 208)]
#endif
        DirectCmds_209,
#if FLEXAPI
        [Offset(1424, 1)]
        [DescriptionLookup(Default = "Direct Command 210")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 209)]
#endif
        DirectCmds_210,
#if FLEXAPI
        [Offset(1425, 1)]
        [DescriptionLookup(Default = "Direct Command 211")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 210)]
#endif
        DirectCmds_211,
#if FLEXAPI
        [Offset(1426, 1)]
        [DescriptionLookup(Default = "Direct Command 212")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 211)]
#endif
        DirectCmds_212,
#if FLEXAPI
        [Offset(1427, 1)]
        [DescriptionLookup(Default = "Direct Command 213")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 212)]
#endif
        DirectCmds_213,
#if FLEXAPI
        [Offset(1428, 1)]
        [DescriptionLookup(Default = "Direct Command 214")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 213)]
#endif
        DirectCmds_214,
#if FLEXAPI
        [Offset(1429, 1)]
        [DescriptionLookup(Default = "Direct Command 215")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 214)]
#endif
        DirectCmds_215,
#if FLEXAPI
        [Offset(1430, 1)]
        [DescriptionLookup(Default = "Direct Command 216")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 215)]
#endif
        DirectCmds_216,
#if FLEXAPI
        [Offset(1431, 1)]
        [DescriptionLookup(Default = "Direct Command 217")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 216)]
#endif
        DirectCmds_217,
#if FLEXAPI
        [Offset(1432, 1)]
        [DescriptionLookup(Default = "Direct Command 218")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 217)]
#endif
        DirectCmds_218,
#if FLEXAPI
        [Offset(1433, 1)]
        [DescriptionLookup(Default = "Direct Command 219")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 218)]
#endif
        DirectCmds_219,
#if FLEXAPI
        [Offset(1434, 1)]
        [DescriptionLookup(Default = "Direct Command 220")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 219)]
#endif
        DirectCmds_220,
#if FLEXAPI
        [Offset(1435, 1)]
        [DescriptionLookup(Default = "Direct Command 221")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 220)]
#endif
        DirectCmds_221,
#if FLEXAPI
        [Offset(1436, 1)]
        [DescriptionLookup(Default = "Direct Command 222")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 221)]
#endif
        DirectCmds_222,
#if FLEXAPI
        [Offset(1437, 1)]
        [DescriptionLookup(Default = "Direct Command 223")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 222)]
#endif
        DirectCmds_223,
#if FLEXAPI
        [Offset(1438, 1)]
        [DescriptionLookup(Default = "Direct Command 224")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 223)]
#endif
        DirectCmds_224,
#if FLEXAPI
        [Offset(1439, 1)]
        [DescriptionLookup(Default = "Direct Command 225")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 224)]
#endif
        DirectCmds_225,
#if FLEXAPI
        [Offset(1440, 1)]
        [DescriptionLookup(Default = "Direct Command 226")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 225)]
#endif
        DirectCmds_226,
#if FLEXAPI
        [Offset(1441, 1)]
        [DescriptionLookup(Default = "Direct Command 227")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 226)]
#endif
        DirectCmds_227,
#if FLEXAPI
        [Offset(1442, 1)]
        [DescriptionLookup(Default = "Direct Command 228")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 227)]
#endif
        DirectCmds_228,
#if FLEXAPI
        [Offset(1443, 1)]
        [DescriptionLookup(Default = "Direct Command 229")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 228)]
#endif
        DirectCmds_229,
#if FLEXAPI
        [Offset(1444, 1)]
        [DescriptionLookup(Default = "Direct Command 230")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 229)]
#endif
        DirectCmds_230,
#if FLEXAPI
        [Offset(1445, 1)]
        [DescriptionLookup(Default = "Direct Command 231")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 230)]
#endif
        DirectCmds_231,
#if FLEXAPI
        [Offset(1446, 1)]
        [DescriptionLookup(Default = "Direct Command 232")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 231)]
#endif
        DirectCmds_232,
#if FLEXAPI
        [Offset(1447, 1)]
        [DescriptionLookup(Default = "Direct Command 233")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 232)]
#endif
        DirectCmds_233,
#if FLEXAPI
        [Offset(1448, 1)]
        [DescriptionLookup(Default = "Direct Command 234")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 233)]
#endif
        DirectCmds_234,
#if FLEXAPI
        [Offset(1449, 1)]
        [DescriptionLookup(Default = "Direct Command 235")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 234)]
#endif
        DirectCmds_235,
#if FLEXAPI
        [Offset(1450, 1)]
        [DescriptionLookup(Default = "Direct Command 236")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 235)]
#endif
        DirectCmds_236,
#if FLEXAPI
        [Offset(1451, 1)]
        [DescriptionLookup(Default = "Direct Command 237")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 236)]
#endif
        DirectCmds_237,
#if FLEXAPI
        [Offset(1452, 1)]
        [DescriptionLookup(Default = "Direct Command 238")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 237)]
#endif
        DirectCmds_238,
#if FLEXAPI
        [Offset(1453, 1)]
        [DescriptionLookup(Default = "Direct Command 239")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 238)]
#endif
        DirectCmds_239,
#if FLEXAPI
        [Offset(1454, 1)]
        [DescriptionLookup(Default = "Direct Command 240")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 239)]
#endif
        DirectCmds_240,
#if FLEXAPI
        [Offset(1455, 1)]
        [DescriptionLookup(Default = "Direct Command 241")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 240)]
#endif
        DirectCmds_241,
#if FLEXAPI
        [Offset(1456, 1)]
        [DescriptionLookup(Default = "Direct Command 242")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 241)]
#endif
        DirectCmds_242,
#if FLEXAPI
        [Offset(1457, 1)]
        [DescriptionLookup(Default = "Direct Command 243")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 242)]
#endif
        DirectCmds_243,
#if FLEXAPI
        [Offset(1458, 1)]
        [DescriptionLookup(Default = "Direct Command 244")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 243)]
#endif
        DirectCmds_244,
#if FLEXAPI
        [Offset(1459, 1)]
        [DescriptionLookup(Default = "Direct Command 245")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 244)]
#endif
        DirectCmds_245,
#if FLEXAPI
        [Offset(1460, 1)]
        [DescriptionLookup(Default = "Direct Command 246")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 245)]
#endif
        DirectCmds_246,
#if FLEXAPI
        [Offset(1461, 1)]
        [DescriptionLookup(Default = "Direct Command 247")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 246)]
#endif
        DirectCmds_247,
#if FLEXAPI
        [Offset(1462, 1)]
        [DescriptionLookup(Default = "Direct Command 248")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 247)]
#endif
        DirectCmds_248,
#if FLEXAPI
        [Offset(1463, 1)]
        [DescriptionLookup(Default = "Direct Command 249")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 248)]
#endif
        DirectCmds_249,
#if FLEXAPI
        [Offset(1464, 1)]
        [DescriptionLookup(Default = "Direct Command 250")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 249)]
#endif
        DirectCmds_250,
#if FLEXAPI
        [Offset(1465, 1)]
        [DescriptionLookup(Default = "Direct Command 251")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 250)]
#endif
        DirectCmds_251,
#if FLEXAPI
        [Offset(1466, 1)]
        [DescriptionLookup(Default = "Direct Command 252")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 251)]
#endif
        DirectCmds_252,
#if FLEXAPI
        [Offset(1467, 1)]
        [DescriptionLookup(Default = "Direct Command 253")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 252)]
#endif
        DirectCmds_253,
#if FLEXAPI
        [Offset(1468, 1)]
        [DescriptionLookup(Default = "Direct Command 254")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 253)]
#endif
        DirectCmds_254,
#if FLEXAPI
        [Offset(1469, 1)]
        [DescriptionLookup(Default = "Direct Command 255")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 254)]
#endif
        DirectCmds_255,
#if FLEXAPI
        [Offset(1470, 1)]
        [DescriptionLookup(Default = "Direct Command 256")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 255)]
#endif
        DirectCmds_256,
#if FLEXAPI
        [Offset(1471, 1)]
        [DescriptionLookup(Default = "Direct Command 257")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 256)]
#endif
        DirectCmds_257,
#if FLEXAPI
        [Offset(1472, 1)]
        [DescriptionLookup(Default = "Direct Command 258")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 257)]
#endif
        DirectCmds_258,
#if FLEXAPI
        [Offset(1473, 1)]
        [DescriptionLookup(Default = "Direct Command 259")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 258)]
#endif
        DirectCmds_259,
#if FLEXAPI
        [Offset(1474, 1)]
        [DescriptionLookup(Default = "Direct Command 260")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 259)]
#endif
        DirectCmds_260,
#if FLEXAPI
        [Offset(1475, 1)]
        [DescriptionLookup(Default = "Direct Command 261")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 260)]
#endif
        DirectCmds_261,
#if FLEXAPI
        [Offset(1476, 1)]
        [DescriptionLookup(Default = "Direct Command 262")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 261)]
#endif
        DirectCmds_262,
#if FLEXAPI
        [Offset(1477, 1)]
        [DescriptionLookup(Default = "Direct Command 263")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 262)]
#endif
        DirectCmds_263,
#if FLEXAPI
        [Offset(1478, 1)]
        [DescriptionLookup(Default = "Direct Command 264")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 263)]
#endif
        DirectCmds_264,
#if FLEXAPI
        [Offset(1479, 1)]
        [DescriptionLookup(Default = "Direct Command 265")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 264)]
#endif
        DirectCmds_265,
#if FLEXAPI
        [Offset(1480, 1)]
        [DescriptionLookup(Default = "Direct Command 266")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 265)]
#endif
        DirectCmds_266,
#if FLEXAPI
        [Offset(1481, 1)]
        [DescriptionLookup(Default = "Direct Command 267")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 266)]
#endif
        DirectCmds_267,
#if FLEXAPI
        [Offset(1482, 1)]
        [DescriptionLookup(Default = "Direct Command 268")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 267)]
#endif
        DirectCmds_268,
#if FLEXAPI
        [Offset(1483, 1)]
        [DescriptionLookup(Default = "Direct Command 269")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 268)]
#endif
        DirectCmds_269,
#if FLEXAPI
        [Offset(1484, 1)]
        [DescriptionLookup(Default = "Direct Command 270")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 269)]
#endif
        DirectCmds_270,
#if FLEXAPI
        [Offset(1485, 1)]
        [DescriptionLookup(Default = "Direct Command 271")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 270)]
#endif
        DirectCmds_271,
#if FLEXAPI
        [Offset(1486, 1)]
        [DescriptionLookup(Default = "Direct Command 272")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 271)]
#endif
        DirectCmds_272,
#if FLEXAPI
        [Offset(1487, 1)]
        [DescriptionLookup(Default = "Direct Command 273")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 272)]
#endif
        DirectCmds_273,
#if FLEXAPI
        [Offset(1488, 1)]
        [DescriptionLookup(Default = "Direct Command 274")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 273)]
#endif
        DirectCmds_274,
#if FLEXAPI
        [Offset(1489, 1)]
        [DescriptionLookup(Default = "Direct Command 275")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 274)]
#endif
        DirectCmds_275,
#if FLEXAPI
        [Offset(1490, 1)]
        [DescriptionLookup(Default = "Direct Command 276")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 275)]
#endif
        DirectCmds_276,
#if FLEXAPI
        [Offset(1491, 1)]
        [DescriptionLookup(Default = "Direct Command 277")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 276)]
#endif
        DirectCmds_277,
#if FLEXAPI
        [Offset(1492, 1)]
        [DescriptionLookup(Default = "Direct Command 278")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 277)]
#endif
        DirectCmds_278,
#if FLEXAPI
        [Offset(1493, 1)]
        [DescriptionLookup(Default = "Direct Command 279")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 278)]
#endif
        DirectCmds_279,
#if FLEXAPI
        [Offset(1494, 1)]
        [DescriptionLookup(Default = "Direct Command 280")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 279)]
#endif
        DirectCmds_280,
#if FLEXAPI
        [Offset(1495, 1)]
        [DescriptionLookup(Default = "Direct Command 281")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 280)]
#endif
        DirectCmds_281,
#if FLEXAPI
        [Offset(1496, 1)]
        [DescriptionLookup(Default = "Direct Command 282")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 281)]
#endif
        DirectCmds_282,
#if FLEXAPI
        [Offset(1497, 1)]
        [DescriptionLookup(Default = "Direct Command 283")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 282)]
#endif
        DirectCmds_283,
#if FLEXAPI
        [Offset(1498, 1)]
        [DescriptionLookup(Default = "Direct Command 284")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 283)]
#endif
        DirectCmds_284,
#if FLEXAPI
        [Offset(1499, 1)]
        [DescriptionLookup(Default = "Direct Command 285")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 284)]
#endif
        DirectCmds_285,
#if FLEXAPI
        [Offset(1500, 1)]
        [DescriptionLookup(Default = "Direct Command 286")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 285)]
#endif
        DirectCmds_286,
#if FLEXAPI
        [Offset(1501, 1)]
        [DescriptionLookup(Default = "Direct Command 287")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 286)]
#endif
        DirectCmds_287,
#if FLEXAPI
        [Offset(1502, 1)]
        [DescriptionLookup(Default = "Direct Command 288")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 287)]
#endif
        DirectCmds_288,
#if FLEXAPI
        [Offset(1503, 1)]
        [DescriptionLookup(Default = "Direct Command 289")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 288)]
#endif
        DirectCmds_289,
#if FLEXAPI
        [Offset(1504, 1)]
        [DescriptionLookup(Default = "Direct Command 290")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 289)]
#endif
        DirectCmds_290,
#if FLEXAPI
        [Offset(1505, 1)]
        [DescriptionLookup(Default = "Direct Command 291")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 290)]
#endif
        DirectCmds_291,
#if FLEXAPI
        [Offset(1506, 1)]
        [DescriptionLookup(Default = "Direct Command 292")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 291)]
#endif
        DirectCmds_292,
#if FLEXAPI
        [Offset(1507, 1)]
        [DescriptionLookup(Default = "Direct Command 293")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 292)]
#endif
        DirectCmds_293,
#if FLEXAPI
        [Offset(1508, 1)]
        [DescriptionLookup(Default = "Direct Command 294")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 293)]
#endif
        DirectCmds_294,
#if FLEXAPI
        [Offset(1509, 1)]
        [DescriptionLookup(Default = "Direct Command 295")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 294)]
#endif
        DirectCmds_295,
#if FLEXAPI
        [Offset(1510, 1)]
        [DescriptionLookup(Default = "Direct Command 296")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 295)]
#endif
        DirectCmds_296,
#if FLEXAPI
        [Offset(1511, 1)]
        [DescriptionLookup(Default = "Direct Command 297")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 296)]
#endif
        DirectCmds_297,
#if FLEXAPI
        [Offset(1512, 1)]
        [DescriptionLookup(Default = "Direct Command 298")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 297)]
#endif
        DirectCmds_298,
#if FLEXAPI
        [Offset(1513, 1)]
        [DescriptionLookup(Default = "Direct Command 299")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 298)]
#endif
        DirectCmds_299,
#if FLEXAPI
        [Offset(1514, 1)]
        [DescriptionLookup(Default = "Direct Command 300")]
        [HideIfDefault]
        [Tree("Direct Commands\\Execute", ControlType.CheckBox, Assets.DirectCommands, 8, 5, 299)]
#endif
        DirectCmds_300,
#if FLEXAPI
        [Offset(1515, 1)]
        [Description("Replace Serial")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 0)]
#endif
        ASSA_REPLACE_SERIAL,
#if FLEXAPI
        [Offset(1516, 1)]
        [Description("Confirm Door")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 2)]
#endif
        ASSA_CONFIRM_DOOR,
#if FLEXAPI
        [Offset(1517, 1)]
        [Description("Reset Access Point")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 3)]
#endif
        ASSA_RESET_ACCESS_POINT,
#if FLEXAPI
        [Offset(1518, 1)]
        [Description("Re-Load Provisioning Data")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 4)]
#endif
        ASSA_REPROV_DATA,
#if FLEXAPI
        [Offset(1519, 1)]
        [Description("Initialize Device")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 5)]
#endif
        ASSA_INIT_DEVICE,
#if FLEXAPI
        [Offset(1520, 1)]
        [Description("Pulse/Lock/Unlock")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 6)]
#endif
        ASSA_PULSE_LOCK_UNLOCK,
#if FLEXAPI
        [Offset(1521, 1)]
        [Description("Lockdown/Remove Lockdown")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 7)]
#endif
        ASSA_LOCKDOWN_REMLOCKDOWN,
#if FLEXAPI
        [Offset(1522, 1)]
        [Description("Add/Edit/Remove DSRs")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 8)]
#endif
        ASSA_ADD_DSR,
#if FLEXAPI
        [Offset(1523, 1)]
        [Description("Force Changes to DSR")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 9)]
#endif
        ASSA_FORCE_CHANGES_TODSR,
#if FLEXAPI
        [Offset(1524, 1)]
        [Description("Legacy Access Level Groups")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 2)]
#endif
        ACCESSLEVELS_ACCESSLEVELGROUPS_R,
#if FLEXAPI
        [Offset(1525, 1)]
        [Description("Floors")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 5)]
#endif
        OTHERREPORTS_FLOORS_R,
#if FLEXAPI
        [Offset(1527, 1)]
        [Description("Who Has Access Door(s)")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 7)]
#endif
        OTHERREPORTS_WHOHASACCESSDOORS_R,
#if FLEXAPI
        [Offset(1528, 1)]
        [Description("Load New Access Points")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 10)]
#endif
        ASSA_LOAD_NEW_ACCESS_PNTS,
#if FLEXAPI
        [Offset(1529, 1)]
        [Description("Add/Edit/Remove Access Modes")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 11)]
#endif
        ASSA_ADD_ACCESS_MODE,
#if FLEXAPI
        [Offset(1530, 1)]
        [Description("Card Formats")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 16)]
#endif
        HARDWARESETTINGS_CARDFORMAT_R,
#if FLEXAPI
        [Offset(1531, 1)]
        [Description("Access Levels Last Used")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 8)]
#endif
        ACCESSLEVELS_LASTUSED_R,
#if FLEXAPI
        [Offset(1532, 1)]
        [Description("Assa and Allegion Doors")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 17)]
#endif
        HARDWARESETTINGS_ASSA_R,
#if FLEXAPI
        [Offset(1533, 1)]
        [Description("Access Level Group Assignments")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 4)]
#endif
        ACCESSLEVELS_ACCESSLEVELGROUPASSIGNED_R,
#if FLEXAPI
        [Offset(1534, 1)]
        [Description("Personnel - Summary")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 5)]
#endif
        PERSONNELREPORTS_SUMMARY_R,
#if FLEXAPI
        [Offset(1535, 1)]
        [Description("DMP Receiver Transactions")]
        [Tree("Reports\\Events", ControlType.CheckBox, Assets.Reports, 4, 2, 2)]
#endif
        EVENTREPORTS_DMP_TRANS_R,
#if FLEXAPI
        [Offset(1536, 1)]
        [Description("Bosch Receiver Transactions")]
        [Tree("Reports\\Events", ControlType.CheckBox, Assets.Reports, 4, 2, 3)]
#endif
        EVENTREPORTS_BOSCH_TRANS_R,
#if FLEXAPI
        [Offset(1537, 2)]
        [Description("Middle")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 2)]
#endif
        PersonnelFieldMiddleName,
#if FLEXAPI
        [Offset(1539, 1)]
        [Description("Add Node")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 0)]
#endif
        STENTOFON_ADDNODE,
#if FLEXAPI
        [Offset(1540, 1)]
        [Description("Edit Node")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 1)]
#endif
        STENTOFON_EDITNODE,
#if FLEXAPI
        [Offset(1541, 1)]
        [Description("Delete Node")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 2)]
#endif
        STENTOFON_DELNODE,
#if FLEXAPI
        [Offset(1542, 1)]
        [Description("Add Station")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 3)]
#endif
        STENTOFON_ADDSTN,
#if FLEXAPI
        [Offset(1543, 1)]
        [Description("Edit Station")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 4)]
#endif
        STENTOFON_EDITSTN,
#if FLEXAPI
        [Offset(1544, 1)]
        [Description("Delete Station")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 5)]
#endif
        STENTOFON_DELSTN,
#if FLEXAPI
        [Offset(1545, 1)]
        [Description("Make Call")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 6)]
#endif
        STENTOFON_MAKECALL,
#if FLEXAPI
        [Offset(1546, 1)]
        [Description("Cancel Call")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 7)]
#endif
        STENTOFON_CANCELCALL,
#if FLEXAPI
        [Offset(1547, 1)]
        [Description("Answer Call")]
        [Tree("Hardware\\Stentofon", ControlType.CheckBox, Assets.Stentofon, Assets.Controller, 0, 14, 8)]
#endif
        STENTOFON_ANSWERCALL,
#if FLEXAPI
        [Offset(1548, 1)]
        [Description("Global Access Level Details")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 3)]
#endif
        ACCESSLEVELS_GLOBALALDETAILS_R,
#if FLEXAPI
        [Offset(1549, 1)]
        [Description("Controllers DST (SSP)")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 3)]
#endif
        HARDWARE_SSP_DST_R,
#if FLEXAPI
        [Offset(1551, 1)]
        [Description("Allow adding Cross Tenant Cardholders to Global Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 19)]
#endif
        AllowCrossTenantAccessLevel,
#if FLEXAPI
        [Offset(1552, 1)]
        [Description("Show Cardholder Name on Cross Tenant Access Level")]
        [Tree("Access Levels", ControlType.CheckBox, Assets.AccessLevels, 1, 20)]
#endif
        AllowNameOnCrossTenantAccessLevel,
#if FLEXAPI
        [Offset(1553, 1)]
        [Description("Door Alerts")]
        [Tree("Hardware\\Door", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 0)]
#endif
        DoorAlertCfg,
#if FLEXAPI
        [Offset(1554, 1)]
        [Description("Personnel - Daily Card Usage")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 6)]
#endif
        PERSONNELREPORTS_CARD_USAGE_R,
#if FLEXAPI
        [Offset(1555, 1)]
        [Description("Add Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 0)]
#endif
        TKEAddGroup,
#if FLEXAPI
        [Offset(1556, 1)]
        [Description("Remove Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 1)]
#endif
        TKERemoveGroup,
#if FLEXAPI
        [Offset(1557, 1)]
        [Description("Edit Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 2)]
#endif
        TKEEditGroup,
#if FLEXAPI
        [Offset(1558, 1)]
        [Description("Add Floor")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 3)]
#endif
        TKEAddFloor,
#if FLEXAPI
        [Offset(1559, 1)]
        [Description("Remove Floor")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 4)]
#endif
        TKERemFloor,
#if FLEXAPI
        [Offset(1560, 1)]
        [Description("Edit Floor")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 5)]
#endif
        TKEEditFloor,
#if FLEXAPI
        [Offset(1561, 1)]
        [Description("Add Floor Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 6)]
#endif
        TKEAddFloorGrp,
#if FLEXAPI
        [Offset(1562, 1)]
        [Description("Remove Floor Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 7)]
#endif
        TKERemFloorGrp,
#if FLEXAPI
        [Offset(1563, 1)]
        [Description("Edit Floor Group")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 8)]
#endif
        TKEEditFloorGrp,
#if FLEXAPI
        [Offset(1564, 1)]
        [Description("Control Floors")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 9)]
#endif
        TKECtrlFloors,
#if FLEXAPI
        [Offset(1565, 1)]
        [Description("Add Kiosk")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 10)]
#endif
        TKEAddKiosk,
#if FLEXAPI
        [Offset(1566, 1)]
        [Description("Remove Kiosk")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 11)]
#endif
        TKERemKiosk,
#if FLEXAPI
        [Offset(1567, 1)]
        [Description("Edit Kiosk")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 12)]
#endif
        TKEEditKiosk,
#if FLEXAPI
        [Offset(1568, 1)]
        [Description("Link Door to Kiosk")]
        [Tree("Hardware\\ThyssenKrupp", ControlType.CheckBox, Assets.ThyssenKrupp, Assets.Controller, 0, 15, 13)]
#endif
        TKELinkDoorToKiosk,
#if FLEXAPI
        [Offset(1569, 1)]
        [Description("Confirm Door")]
        [Tree("Hardware\\Isonas Doors", ControlType.CheckBox, Assets.Isonas, Assets.Controller, 0, 16, 0)]
#endif
        IsonasConfirmDoor,
#if FLEXAPI
        [Offset(1570, 1)]
        [Secondary(1572, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Time Schedules\\Time Schedule Set", ControlType.ComboBox, Assets.TimeSchedule, Assets.Controller, 0, 3, 3, 0)]
#endif
        AllowEditTS,
#if FLEXAPI
        [Offset(1571, 1)]
        [Secondary(1573, 1)]
        [Description("Allow Edit Properties")]
        [Tree("Hardware\\Holidays\\Holiday Set", ControlType.ComboBox, Assets.Holidays, Assets.Controller, 0, 4, 0, 0)]
#endif
        AllowEditHS,
#if FLEXAPI
        [Offset(1574, 1)]
        [Description("Allow Design")]
        [Tree("Graphics", ControlType.CheckBox, Assets.Graphics, 7, 5)]
#endif
        GraphicsAllowDesign,
#if FLEXAPI
        [Offset(1575, 2)]
        [Description("User Personnel Group")]
        [Tree("Personnel\\Personnel Fields", ControlType.ComboBox, Assets.Personnel, 2, 2, 6)]
#endif
        UserPersonnelGrp,
#if FLEXAPI
        [Offset(1577, 1)]
        [Description("Upload Firmware")]
        [Tree("Hardware\\Isonas Doors", ControlType.CheckBox, Assets.Isonas, Assets.Controller, 0, 16, 1)]
#endif
        IsonasUploadFirmware,
#if FLEXAPI
        [Offset(1578, 1)]
        [Description("Create Door Subgroup")]
        [Tree("Hardware\\Door", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 1)]
#endif
        CreateDoorSubgroup,
#if FLEXAPI
        [Offset(1579, 1)]
        [Description("Modify Door Subgroup")]
        [Tree("Hardware\\Door", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 2)]
#endif
        ModifyDoorSubgroup,
#if FLEXAPI
        [Offset(1580, 1)]
        [Description("Remove Door Subgroup")]
        [Tree("Hardware\\Door", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 3)]
#endif
        RemoveDoorSubgroup,
#if FLEXAPI
        [Offset(1581, 1)]
        [Description("View Door Subgroup Report")]
        [Tree("Hardware\\Door", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 2, 4)]
#endif
        ViewDoorSubgroup,
#if FLEXAPI
        [Offset(1582, 1)]
        [Description("Discover Panel")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 0)]
#endif
        BoschPnl_DiscHdw,
#if FLEXAPI
        [Offset(1583, 1)]
        [Description("Add Panel")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 1)]
#endif
        BoschPnl_AddPanel,
#if FLEXAPI
        [Offset(1584, 1)]
        [Description("Delete Panel")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 2)]
#endif
        BoschPnl_DelPanel,
#if FLEXAPI
        [Offset(1585, 1)]
        [Description("Edit Panel")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 3)]
#endif
        BoschPnl_EditPanel,
#if FLEXAPI
        [Offset(1586, 1)]
        [Description("Edit Area")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 4)]
#endif
        BoschPnl_EditArea,
#if FLEXAPI
        [Offset(1587, 1)]
        [Description("Edit Point")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 5)]
#endif
        BoschPnl_EditPoint,
#if FLEXAPI
        [Offset(1588, 1)]
        [Description("Edit Output")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 6)]
#endif
        BoschPnl_EditOutput,
#if FLEXAPI
        [Offset(1589, 1)]
        [Description("Arm Area")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 7)]
#endif
        BoschPnl_Arm,
#if FLEXAPI
        [Offset(1590, 1)]
        [Description("Disarm Area")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 8)]
#endif
        BoschPnl_DisArm,
#if FLEXAPI
        [Offset(1591, 1)]
        [Description("Bypass Point")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 9)]
#endif
        BoschPnl_Bypass,
#if FLEXAPI
        [Offset(1592, 1)]
        [Description("Unbypass Point")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 10)]
#endif
        BoschPnl_Unbypass,
#if FLEXAPI
        [Offset(1593, 1)]
        [Description("Activate Output")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 11)]
#endif
        BoschPnl_Activate,
#if FLEXAPI
        [Offset(1594, 1)]
        [Description("Deactiave Output")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 12)]
#endif
        BoschPnl_DeActivate,
#if FLEXAPI
        [Offset(1595, 1)]
        [Description("Web Login")]
        [Tree("Hardware\\Controller", ControlType.CheckBox, Assets.Controller, 0, 8, 11)]
#endif
        AllowWebLogin,
#if FLEXAPI
        [Offset(1596, 1)]
        [Description("Add Authority Level Group")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 13)]
#endif
        BoschPnl_AddGroupLevel,
#if FLEXAPI
        [Offset(1597, 1)]
        [Description("Remove Authority Level Group")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 14)]
#endif
        BoschPnl_RemGroupLevel,
#if FLEXAPI
        [Offset(1598, 1)]
        [Description("Add/Remove User")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 15)]
#endif
        BoschPnl_AddRemUser,
#if FLEXAPI
        [Offset(1599, 1)]
        [Description("Allow Passcode Editing")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 16)]
#endif
        BoschPnl_UniquePasscode,
#if FLEXAPI
        [Offset(1600, 1)]
        [Description("Download Panel Users")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 17)]
#endif
        BoschPnl_DownloadAllUsers,
#if FLEXAPI
        [Offset(1601, 1)]
        [Description("Silence Bells")]
        [Tree("Hardware\\Bosch Panels", ControlType.CheckBox, Assets.Bosch, Assets.Controller, 0, 17, 18)]
#endif
        BoschPnl_SilenceBells,
#if FLEXAPI
        [Offset(1602, 1)]
        [Description("Enroll BLE Credentials")]
        [Tree("Hardware\\Isonas Doors", ControlType.CheckBox, Assets.Isonas, Assets.Controller, 0, 16, 2)]
#endif
        ISONAS_ENROLL_BLE,
#if FLEXAPI
        [Offset(1603, 1)]
        [Description("Allow Export Video and Email")]
        [Tree("Hardware\\DVRs and Cameras", ControlType.CheckBox, Assets.DVRsandCameras, Assets.Controller, 0, 10, 6)]
#endif
        DvrExpNEmail,
#if FLEXAPI
        [Offset(1606, 1)]
        [Description("Add Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 0)]
#endif
        KONE_ADDGROUP,
#if FLEXAPI
        [Offset(1607, 1)]
        [Description("Remove Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 1)]
#endif
        KONE_REMOVEGROUP,
#if FLEXAPI
        [Offset(1608, 1)]
        [Description("Edit Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 2)]
#endif
        KONE_EDITGROUP,
#if FLEXAPI
        [Offset(1609, 1)]
        [Description("Add Floor")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 3)]
#endif
        KONE_ADD_FLOOR,
#if FLEXAPI
        [Offset(1610, 1)]
        [Description("Remove Floor")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 4)]
#endif
        KONE_REM_FLOOR,
#if FLEXAPI
        [Offset(1611, 1)]
        [Description("Edit Floor")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 5)]
#endif
        KONE_EDIT_FLOOR,
#if FLEXAPI
        [Offset(1612, 1)]
        [Description("Add Floor Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 6)]
#endif
        KONE_ADD_FLOORGRP,
#if FLEXAPI
        [Offset(1613, 1)]
        [Description("Remove Floor Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 7)]
#endif
        KONE_REM_FLOORGRP,
#if FLEXAPI
        [Offset(1614, 1)]
        [Description("Edit Floor Group")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 8)]
#endif
        KONE_EDIT_FLOORGRP,
#if FLEXAPI
        [Offset(1615, 1)]
        [Description("Control Floors")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 9)]
#endif
        KONE_CTRLFLOORS,
#if FLEXAPI
        [Offset(1616, 1)]
        [Description("Add DOP")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 10)]
#endif
        KONE_ADDDOP,
#if FLEXAPI
        [Offset(1617, 1)]
        [Description("Remove DOP")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 11)]
#endif
        KONE_REMOVEDOP,
#if FLEXAPI
        [Offset(1618, 1)]
        [Description("Edit DOP")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 12)]
#endif
        KONE_EDITDOP,
#if FLEXAPI
        [Offset(1619, 1)]
        [Description("Link Door to DOP")]
        [Tree("Hardware\\Kone", ControlType.CheckBox, Assets.Kone, Assets.Controller, 0, 18, 13)]
#endif
        KONE_LINK_DOORTODOP,
#if FLEXAPI
        [Offset(1620, 1)]
        [Description("Clear Selected Alarm")]
        [Tree("Alarms", ControlType.CheckBox, Assets.Alarms, 3, 3)]
#endif
        AlarmsClearSel,
#if FLEXAPI
        [Offset(1621, 1)]
        [Description("Personnel - Printed Badges")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 7)]
#endif
        PERSONNELREPORTS_BADGES_R,
#if FLEXAPI
        [Offset(1622, 1)]
        [Description("Allow Has Access To")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 15)]
#endif
        AllowHasAccess,
#if FLEXAPI
        [Offset(1623, 1)]
        [Description("Add Site")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 0)]
#endif
        ENGAGE_ADDSITE,
#if FLEXAPI
        [Offset(1624, 1)]
        [Description("Engage Sites")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 18)]
#endif
        ENGAGE_REPORT_R,
#if FLEXAPI
        [Offset(1625, 1)]
        [Description("Edit Schindler Settings")]
        [Tree("Hardware\\Schindler Elevator", ControlType.CheckBox, Assets.Schindler, Assets.Controller, 0, 20, 0)]
#endif
        SCHINDLER_SETTINGS,
#if FLEXAPI
        [Offset(1626, 1)]
        [Description("Maintain Master Groups")]
        [Tree("Hardware\\Schindler Elevator", ControlType.CheckBox, Assets.Schindler, Assets.Controller, 0, 20, 1)]
#endif
        SCHINDLER_MASTERGROUPS,
#if FLEXAPI
        [Offset(1627, 1)]
        [Description("Remove Site")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 1)]
#endif
        ENGAGE_REMSITE,
#if FLEXAPI
        [Offset(1628, 1)]
        [Description("Edit Site")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 3)]
#endif
        ENGAGE_EDITSITE,
#if FLEXAPI
        [Offset(1629, 1)]
        [Description("Unlink All Doors")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 6)]
#endif
        ENGAGE_UNLINK_DOORS,
#if FLEXAPI
        [Offset(1630, 1)]
        [Description("Edit Gateway")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 4)]
#endif
        ENGAGE_EDITGTWY,
#if FLEXAPI
        [Offset(1631, 1)]
        [Description("Edit Door")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 5)]
#endif
        ENGAGE_EDITDOOR,
#if FLEXAPI
        [Offset(1632, 2)]
        [Description("Override Card")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 32)]
#endif
        CardField38,
#if FLEXAPI
        [Offset(1634, 1)]
        [Description("Status Station")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 10)]
#endif
        OTHERREPORTS_STATIONSTATUS_R,
#if FLEXAPI
        [Offset(1635, 1)]
        [Description("Assa Access Levels")]
        [Tree("Reports\\Access", ControlType.CheckBox, Assets.Reports, 4, 0, 9)]
#endif
        ASSA_ACCESSLEVELS_R,
#if FLEXAPI
        [Offset(1636, 1)]
        [Description("Allow Non Tenant Hardware on Cardholder Trace History")]
        [Tree("Personnel\\Personnel Actions", ControlType.CheckBox, Assets.Personnel, 2, 0, 16)]
#endif
        AllowNonTenantHdwOnCardholderTraceHistory,
#if FLEXAPI
        [Offset(1637, 1)]
        [Description("Personnel - Schindler")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 8)]
#endif
        PERSONNELREPORTS_PersonnelSchindler_R,
#if FLEXAPI
        [Offset(1638, 1)]
        [Description("Info Ready")]
        [Tree("Hardware\\Schindler Elevator", ControlType.CheckBox, Assets.Schindler, Assets.Controller, 0, 20, 2)]
#endif
        SCHINDLER_InfoReady,
#if FLEXAPI
        [Offset(1639, 1)]
        [Description("Invite New User")]
        [Tree("Hardware\\EngageIP", ControlType.CheckBox, Assets.EngageIP, Assets.Controller, 0, 19, 2)]
#endif
        EngageInviteNewUser,
#if FLEXAPI
        [Offset(1640, 1)]
        [Description("Allow Door De-Commissioning")]
        [Tree("Hardware\\Isonas Doors", ControlType.CheckBox, Assets.Isonas, Assets.Controller, 0, 16, 3)]
#endif
        AllowDoorDeCommissioning,
#if FLEXAPI
        [Offset(1641, 1)]
        [Description("Allow Door Auditing")]
        [Tree("Hardware\\Isonas Doors", ControlType.CheckBox, Assets.Isonas, Assets.Controller, 0, 16, 4)]
#endif
        AllowDoorAuditing,
#if FLEXAPI
        [Offset(1642, 1)]
        [Description("HBM")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 5)]
#endif
        RptSystemHBM,
#if FLEXAPI
        [Offset(1643, 1)]
        [Description("Allow Edit Types")]
        [Tree("Hardware\\Holidays\\Holiday", ControlType.ComboBox, Assets.TimeSchedule, Assets.Controller, 0, 4, 1, 1)]
#endif
        AllowEditHolidayTypes,
#if FLEXAPI
        [Offset(1644, 1)]
        [Description("Add/Change Direct Command Password")]
        [Tree("Direct Commands", ControlType.CheckBox, Assets.DirectCommands, 8, 4)]
#endif
        AddChangeDirectCmdPwd,
#if FLEXAPI
        [Offset(1645, 1)]
        [Description("Operators SSP Assignments")]
        [Tree("Reports\\Hardware Settings", ControlType.CheckBox, Assets.Reports, 4, 3, 19)]
#endif
        OPERATOR_SSP_R,
#if FLEXAPI
        [Offset(1646, 1)]
        [Description("Alarm Grid To Front")]
        [Tree("Alarms", ControlType.CheckBox, Assets.Alarms, 3, 1)]
#endif
        AlarmsApp2Front_OnAlm,
#if FLEXAPI
        [Offset(1647, 1)]
        [Description("Analyze DNA/DSR Synchronization")]
        [Tree("Hardware\\Assa Specific", ControlType.CheckBox, Assets.ACM, Assets.Controller, 0, 13, 1)]
#endif
        ASSA_ALLOW_SYNCUTILITY,
#if FLEXAPI
        [Offset(1648, 1)]
        [Description("Operators Profile")]
        [Tree("Reports\\System", ControlType.CheckBox, Assets.Reports, 4, 6, 11)]
#endif
        OTHERREPORTS_OPERATORSPROFILES_R,
#if FLEXAPI
        [Offset(1649, 1)]
        [Description("Run Low Priority Tools")]
        [Tree("Built-In Tools", ControlType.CheckBox, Assets.Gear, 12, 0)]
#endif
        BUILTIN_TOOLS_LOW,
#if FLEXAPI
        [Offset(1650, 1)]
        [Description("Run Medium Priority Tools")]
        [Tree("Built-In Tools", ControlType.CheckBox, Assets.Gear, 12, 1)]
#endif
        BUILTIN_TOOLS_MED,
#if FLEXAPI
        [Offset(1651, 1)]
        [Description("Run High Priority Tools")]
        [Tree("Built-In Tools", ControlType.CheckBox, Assets.Gear, 12, 2)]
#endif
        BUILTIN_TOOLS_HIGH,
#if FLEXAPI
        [Offset(1652, 1)]
        [Description("Allow ACT ID Enrollment")]
        [Tree("Hardware\\ACT ID", ControlType.CheckBox, Assets.ActID, Assets.Controller, 0, 21, 0)]
#endif
        AllowWaveLynxEnrollment,
#if FLEXAPI
        [Offset(1653, 1)]
        [Description("Personnel - CardLess")]
        [Tree("Reports\\Personnel", ControlType.CheckBox, Assets.Reports, 4, 4, 9)]
#endif
        PERSONNELREPORTS_CARDLESS_R,
#if FLEXAPI
        [Offset(1654, 2)]
        [Description("Non-Use Exclusion")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 33)]
#endif
        CardField39,
#if FLEXAPI
        [Offset(1656, 1)]
        [DescriptionLookup(Default = "Custom Report 21")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 22)]
#endif
        CUSTOM21_R,
#if FLEXAPI
        [Offset(1657, 1)]
        [DescriptionLookup(Default = "Custom Report 22")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 23)]
#endif
        CUSTOM22_R,
#if FLEXAPI
        [Offset(1658, 1)]
        [DescriptionLookup(Default = "Custom Report 23")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 24)]
#endif
        CUSTOM23_R,
#if FLEXAPI
        [Offset(1659, 1)]
        [DescriptionLookup(Default = "Custom Report 24")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 25)]
#endif
        CUSTOM24_R,
#if FLEXAPI
        [Offset(1660, 1)]
        [DescriptionLookup(Default = "Custom Report 25")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 26)]
#endif
        CUSTOM25_R,
#if FLEXAPI
        [Offset(1661, 1)]
        [DescriptionLookup(Default = "Custom Report 26")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 27)]
#endif
        CUSTOM26_R,
#if FLEXAPI
        [Offset(1662, 1)]
        [DescriptionLookup(Default = "Custom Report 27")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 28)]
#endif
        CUSTOM27_R,
#if FLEXAPI
        [Offset(1663, 1)]
        [DescriptionLookup(Default = "Custom Report 28")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 29)]
#endif
        CUSTOM28_R,
#if FLEXAPI
        [Offset(1664, 1)]
        [DescriptionLookup(Default = "Custom Report 29")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 30)]
#endif
        CUSTOM29_R,
#if FLEXAPI
        [Offset(1665, 1)]
        [DescriptionLookup(Default = "Custom Report 30")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 31)]
#endif
        CUSTOM30_R,
#if FLEXAPI
        [Offset(1666, 1)]
        [DescriptionLookup(Default = "Custom Report 31")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 32)]
#endif
        CUSTOM31_R,
#if FLEXAPI
        [Offset(1667, 1)]
        [DescriptionLookup(Default = "Custom Report 32")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 33)]
#endif
        CUSTOM32_R,
#if FLEXAPI
        [Offset(1668, 1)]
        [DescriptionLookup(Default = "Custom Report 33")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 34)]
#endif
        CUSTOM33_R,
#if FLEXAPI
        [Offset(1669, 1)]
        [DescriptionLookup(Default = "Custom Report 34")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 35)]
#endif
        CUSTOM34_R,
#if FLEXAPI
        [Offset(1670, 1)]
        [DescriptionLookup(Default = "Custom Report 35")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 36)]
#endif
        CUSTOM35_R,
#if FLEXAPI
        [Offset(1671, 1)]
        [DescriptionLookup(Default = "Custom Report 36")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 37)]
#endif
        CUSTOM36_R,
#if FLEXAPI
        [Offset(1672, 1)]
        [DescriptionLookup(Default = "Custom Report 37")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 38)]
#endif
        CUSTOM37_R,
#if FLEXAPI
        [Offset(1673, 1)]
        [DescriptionLookup(Default = "Custom Report 38")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 39)]
#endif
        CUSTOM38_R,
#if FLEXAPI
        [Offset(1674, 1)]
        [DescriptionLookup(Default = "Custom Report 39")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 40)]
#endif
        CUSTOM39_R,
#if FLEXAPI
        [Offset(1675, 1)]
        [DescriptionLookup(Default = "Custom Report 40")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 41)]
#endif
        CUSTOM40_R,
#if FLEXAPI
        [Offset(1676, 1)]
        [DescriptionLookup(Default = "Custom Report 41")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 42)]
#endif
        CUSTOM41_R,
#if FLEXAPI
        [Offset(1677, 1)]
        [DescriptionLookup(Default = "Custom Report 42")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 43)]
#endif
        CUSTOM42_R,
#if FLEXAPI
        [Offset(1678, 1)]
        [DescriptionLookup(Default = "Custom Report 43")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 44)]
#endif
        CUSTOM43_R,
#if FLEXAPI
        [Offset(1679, 1)]
        [DescriptionLookup(Default = "Custom Report 44")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 45)]
#endif
        CUSTOM44_R,
#if FLEXAPI
        [Offset(1680, 1)]
        [DescriptionLookup(Default = "Custom Report 45")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 46)]
#endif
        CUSTOM45_R,
#if FLEXAPI
        [Offset(1681, 1)]
        [DescriptionLookup(Default = "Custom Report 46")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 47)]
#endif
        CUSTOM46_R,
#if FLEXAPI
        [Offset(1682, 1)]
        [DescriptionLookup(Default = "Custom Report 47")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 48)]
#endif
        CUSTOM47_R,
#if FLEXAPI
        [Offset(1683, 1)]
        [DescriptionLookup(Default = "Custom Report 48")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 49)]
#endif
        CUSTOM48_R,
#if FLEXAPI
        [Offset(1684, 1)]
        [DescriptionLookup(Default = "Custom Report 49")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 50)]
#endif
        CUSTOM49_R,
#if FLEXAPI
        [Offset(1685, 1)]
        [DescriptionLookup(Default = "Custom Report 50")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 51)]
#endif
        CUSTOM50_R,
#if FLEXAPI
        [Offset(1686, 1)]
        [DescriptionLookup(Default = "Custom Report 51")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 52)]
#endif
        CUSTOM51_R,
#if FLEXAPI
        [Offset(1687, 1)]
        [DescriptionLookup(Default = "Custom Report 52")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 53)]
#endif
        CUSTOM52_R,
#if FLEXAPI
        [Offset(1688, 1)]
        [DescriptionLookup(Default = "Custom Report 53")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 54)]
#endif
        CUSTOM53_R,
#if FLEXAPI
        [Offset(1689, 1)]
        [DescriptionLookup(Default = "Custom Report 54")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 55)]
#endif
        CUSTOM54_R,
#if FLEXAPI
        [Offset(1690, 1)]
        [DescriptionLookup(Default = "Custom Report 55")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 56)]
#endif
        CUSTOM55_R,
#if FLEXAPI
        [Offset(1691, 1)]
        [DescriptionLookup(Default = "Custom Report 56")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 57)]
#endif
        CUSTOM56_R,
#if FLEXAPI
        [Offset(1692, 1)]
        [DescriptionLookup(Default = "Custom Report 57")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 58)]
#endif
        CUSTOM57_R,
#if FLEXAPI
        [Offset(1693, 1)]
        [DescriptionLookup(Default = "Custom Report 58")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 59)]
#endif
        CUSTOM58_R,
#if FLEXAPI
        [Offset(1694, 1)]
        [DescriptionLookup(Default = "Custom Report 59")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 60)]
#endif
        CUSTOM59_R,
#if FLEXAPI
        [Offset(1695, 1)]
        [DescriptionLookup(Default = "Custom Report 60")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 61)]
#endif
        CUSTOM60_R,
#if FLEXAPI
        [Offset(1696, 1)]
        [DescriptionLookup(Default = "Custom Report 61")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 62)]
#endif
        CUSTOM61_R,
#if FLEXAPI
        [Offset(1697, 1)]
        [DescriptionLookup(Default = "Custom Report 62")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 63)]
#endif
        CUSTOM62_R,
#if FLEXAPI
        [Offset(1698, 1)]
        [DescriptionLookup(Default = "Custom Report 63")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 64)]
#endif
        CUSTOM63_R,
#if FLEXAPI
        [Offset(1699, 1)]
        [DescriptionLookup(Default = "Custom Report 64")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 65)]
#endif
        CUSTOM64_R,
#if FLEXAPI
        [Offset(1700, 1)]
        [DescriptionLookup(Default = "Custom Report 65")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 66)]
#endif
        CUSTOM65_R,
#if FLEXAPI
        [Offset(1701, 1)]
        [DescriptionLookup(Default = "Custom Report 66")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 67)]
#endif
        CUSTOM66_R,
#if FLEXAPI
        [Offset(1702, 1)]
        [DescriptionLookup(Default = "Custom Report 67")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 68)]
#endif
        CUSTOM67_R,
#if FLEXAPI
        [Offset(1703, 1)]
        [DescriptionLookup(Default = "Custom Report 68")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 69)]
#endif
        CUSTOM68_R,
#if FLEXAPI
        [Offset(1704, 1)]
        [DescriptionLookup(Default = "Custom Report 69")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 70)]
#endif
        CUSTOM69_R,
#if FLEXAPI
        [Offset(1705, 1)]
        [DescriptionLookup(Default = "Custom Report 70")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 71)]
#endif
        CUSTOM70_R,
#if FLEXAPI
        [Offset(1706, 1)]
        [DescriptionLookup(Default = "Custom Report 71")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 72)]
#endif
        CUSTOM71_R,
#if FLEXAPI
        [Offset(1707, 1)]
        [DescriptionLookup(Default = "Custom Report 72")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 73)]
#endif
        CUSTOM72_R,
#if FLEXAPI
        [Offset(1708, 1)]
        [DescriptionLookup(Default = "Custom Report 73")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 74)]
#endif
        CUSTOM73_R,
#if FLEXAPI
        [Offset(1709, 1)]
        [DescriptionLookup(Default = "Custom Report 74")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 75)]
#endif
        CUSTOM74_R,
#if FLEXAPI
        [Offset(1710, 1)]
        [DescriptionLookup(Default = "Custom Report 75")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 76)]
#endif
        CUSTOM75_R,
#if FLEXAPI
        [Offset(1711, 1)]
        [DescriptionLookup(Default = "Custom Report 76")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 77)]
#endif
        CUSTOM76_R,
#if FLEXAPI
        [Offset(1712, 1)]
        [DescriptionLookup(Default = "Custom Report 77")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 78)]
#endif
        CUSTOM77_R,
#if FLEXAPI
        [Offset(1713, 1)]
        [DescriptionLookup(Default = "Custom Report 78")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 79)]
#endif
        CUSTOM78_R,
#if FLEXAPI
        [Offset(1714, 1)]
        [DescriptionLookup(Default = "Custom Report 79")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 80)]
#endif
        CUSTOM79_R,
#if FLEXAPI
        [Offset(1715, 1)]
        [DescriptionLookup(Default = "Custom Report 80")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 81)]
#endif
        CUSTOM80_R,
#if FLEXAPI
        [Offset(1716, 1)]
        [DescriptionLookup(Default = "Custom Report 81")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 82)]
#endif
        CUSTOM81_R,
#if FLEXAPI
        [Offset(1717, 1)]
        [DescriptionLookup(Default = "Custom Report 82")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 83)]
#endif
        CUSTOM82_R,
#if FLEXAPI
        [Offset(1718, 1)]
        [DescriptionLookup(Default = "Custom Report 83")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 84)]
#endif
        CUSTOM83_R,
#if FLEXAPI
        [Offset(1719, 1)]
        [DescriptionLookup(Default = "Custom Report 84")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 85)]
#endif
        CUSTOM84_R,
#if FLEXAPI
        [Offset(1720, 1)]
        [DescriptionLookup(Default = "Custom Report 85")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 86)]
#endif
        CUSTOM85_R,
#if FLEXAPI
        [Offset(1721, 1)]
        [DescriptionLookup(Default = "Custom Report 86")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 87)]
#endif
        CUSTOM86_R,
#if FLEXAPI
        [Offset(1722, 1)]
        [DescriptionLookup(Default = "Custom Report 87")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 88)]
#endif
        CUSTOM87_R,
#if FLEXAPI
        [Offset(1723, 1)]
        [DescriptionLookup(Default = "Custom Report 88")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 89)]
#endif
        CUSTOM88_R,
#if FLEXAPI
        [Offset(1724, 1)]
        [DescriptionLookup(Default = "Custom Report 89")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 90)]
#endif
        CUSTOM89_R,
#if FLEXAPI
        [Offset(1725, 1)]
        [DescriptionLookup(Default = "Custom Report 90")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 91)]
#endif
        CUSTOM90_R,
#if FLEXAPI
        [Offset(1726, 1)]
        [DescriptionLookup(Default = "Custom Report 91")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 92)]
#endif
        CUSTOM91_R,
#if FLEXAPI
        [Offset(1727, 1)]
        [DescriptionLookup(Default = "Custom Report 92")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 93)]
#endif
        CUSTOM92_R,
#if FLEXAPI
        [Offset(1728, 1)]
        [DescriptionLookup(Default = "Custom Report 93")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 94)]
#endif
        CUSTOM93_R,
#if FLEXAPI
        [Offset(1729, 1)]
        [DescriptionLookup(Default = "Custom Report 94")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 95)]
#endif
        CUSTOM94_R,
#if FLEXAPI
        [Offset(1730, 1)]
        [DescriptionLookup(Default = "Custom Report 95")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 96)]
#endif
        CUSTOM95_R,
#if FLEXAPI
        [Offset(1731, 1)]
        [DescriptionLookup(Default = "Custom Report 96")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 97)]
#endif
        CUSTOM96_R,
#if FLEXAPI
        [Offset(1732, 1)]
        [DescriptionLookup(Default = "Custom Report 97")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 98)]
#endif
        CUSTOM97_R,
#if FLEXAPI
        [Offset(1733, 1)]
        [DescriptionLookup(Default = "Custom Report 98")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 99)]
#endif
        CUSTOM98_R,
#if FLEXAPI
        [Offset(1734, 1)]
        [DescriptionLookup(Default = "Custom Report 99")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 100)]
#endif
        CUSTOM99_R,
#if FLEXAPI
        [Offset(1735, 1)]
        [DescriptionLookup(Default = "Custom Report 100")]
        [Tree("Reports\\Custom", ControlType.CheckBox, Assets.Reports, 4, 7, 101)]
#endif
        CUSTOM100_R,
#if FLEXAPI
        [Offset(1736, 1)]
        [Description("Allow OmniKey Enrollment")]
        [Tree("Hardware\\OmniKey", ControlType.CheckBox, Assets.HIDOmniKey, Assets.Controller, 0, 22, 0)]
#endif
        AllowOmniKeyEnrollment,
#if FLEXAPI
        [Offset(1737, 2)]
        [Description("Credential Format")]
        [Tree("Personnel\\Card Fields", ControlType.ComboBox, Assets.Personnel, 2, 4, 34)]
#endif
        CardField40,
#if FLEXAPI
        [Offset(1739, 1)]
        [Description("Add Category")]
        [Tree("Hardware\\External Hardware", ControlType.CheckBox, Assets.ExtHardware, Assets.Controller, 0, 23, 0)]
#endif
        ExtHdwAddCategory,
#if FLEXAPI
        [Offset(1740, 1)]
        [Description("Remove Category")]
        [Tree("Hardware\\External Hardware", ControlType.CheckBox, Assets.ExtHardware, Assets.Controller, 0, 23, 1)]
#endif
        ExtHdwRemoveCategory,
#if FLEXAPI
        [Offset(1741, 1)]
        [Description("Add Hardware")]
        [Tree("Hardware\\External Hardware", ControlType.CheckBox, Assets.ExtHardware, Assets.Controller, 0, 23, 2)]
#endif
        ExtHdwAddHardware,
#if FLEXAPI
        [Offset(1742, 1)]
        [Description("Remove Hardware")]
        [Tree("Hardware\\External Hardware", ControlType.CheckBox, Assets.ExtHardware, Assets.Controller, 0, 23, 3)]
#endif
        ExtHdwAddRemoveHardware,
#if FLEXAPI
        [Offset(1743, 1)]
        [Description("Edit Custom Events")]
        [Tree("Hardware\\External Hardware", ControlType.CheckBox, Assets.ExtHardware, Assets.Controller, 0, 23, 4)]
#endif
        ExtHdwEdtiCustomEvents,
    }
}
// ReSharper enable CheckNamespace
// ReSharper enable InconsistentNaming
// ReSharper enable UnusedMember.Global
