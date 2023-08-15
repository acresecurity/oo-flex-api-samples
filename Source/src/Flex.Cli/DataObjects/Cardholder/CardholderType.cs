
namespace Flex.DataObjects.Cardholder
{
    public partial class CardholderType
    {
        /// <summary>
        /// System friendly description of cardholder type
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual short UniqueId { get; set; }

        /// <summary>
        /// Determines if the user as access to this cardholder type
        /// </summary>
        public virtual bool CanAssign { get; set; }
    }
}
