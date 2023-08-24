using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Flex.DataObjects.Settings
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PhotoNameOption : int
    {
        [Description("Not used")]
        NotUsed = 0,

        [Description("Card number")]
        CardNumber = 1, // Not used

        [Description("Date of photo")]
        DateOfPhoto = 2,

        [Description("Employee number")]
        EmployeeNumber = 3, // SSN

        [Description("First name")]
        FirstName = 4,

        [Description("Last name")]
        LastName = 5,

        [Description("Other")]
        Other = 6,

        [Description("Photo number")]
        PhotoNumber = 7,

        [Description("Site ID")]
        SiteID = 8, // Not Used

        [Description("Site name")]
        SiteName = 9, // Not Used

        [Description("Tenant ID")]
        TenantID = 10,

        [Description("Time of Photo")]
        TimeOfPhoto = 11,

        [Description("Unique ID")]
        UniqueID = 12,

        [Description("Employee ID")]
        EmpID = 13,
    }
}
