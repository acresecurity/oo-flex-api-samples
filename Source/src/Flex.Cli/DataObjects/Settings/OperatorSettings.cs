
namespace Flex.DataObjects.Settings
{
    public partial class OperatorSettings
    {
        public bool? DarkMode { get; set; }

        public string NameFormatting { get; set; } = "{{ personnel | FullName }}";
    }
}
