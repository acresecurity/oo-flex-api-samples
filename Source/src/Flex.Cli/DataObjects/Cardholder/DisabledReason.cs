
namespace Flex.DataObjects.Cardholder
{
    public partial class DisabledReason
    {
        public virtual DateTime? Created { get; set; }

        public virtual string Description { get; set; }

        public virtual int SystemType { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual short UniqueKey { get; set; }

        public virtual DateTime? Updated { get; set; }
    }
}
