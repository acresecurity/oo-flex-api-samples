
namespace Common.DataObjects
{
    public class Event
    {
        public int? CameraId { get; set; }

        public Guid? CardholderKey { get; set; }

        public string GSACredential { get; set; }

        public bool Display { get; set; }

        public int EventDescriptionId { get; set; }

        public string FirstName { get; set; }

        public string HardwareAddress { get; set; }

        public string HardwareDescription { get; set; }

        public virtual string HardwareType { get; set; }

        public Guid? HardwareUniqueKey { get; set; }

        public string LastName { get; set; }

        public decimal CardNumber { get; set; }

        public int FacilityCode { get; set; }

        public DateTime? Panel { get; set; }

        public DateTime? Transaction { get; set; }

        public string TransactionData { get; set; }

        public int UniqueId { get; set; }
    }
}
