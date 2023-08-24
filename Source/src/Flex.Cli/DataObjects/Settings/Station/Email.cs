
namespace Flex.DataObjects.Settings.Station
{
    public partial class Email
    {
        public string AddressMessage { get; set; }

        public string CardMessage { get; set; }

        public string DeadmanMessage { get; set; }

        public string SMTPAuth { get; set; }

        public string SMTPFromAddress { get; set; }

        public string SMTPServer { get; set; }

        public int? SMTPAuthType { get; set; }
    }
}
