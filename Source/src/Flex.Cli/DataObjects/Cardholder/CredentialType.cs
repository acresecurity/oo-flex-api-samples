
namespace Flex.DataObjects.Cardholder
{
    public partial class CredentialType
    {
        public virtual DateTime? Created { get; set; }

        public virtual string Description { get; set; }

        public virtual int SystemType { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual int UniqueId { get; set; }

        public virtual DateTime? Updated { get; set; }

        /// <summary>
        /// Determines if the user as access to this cardholder type
        /// </summary>
        public virtual bool CanAssign { get; set; }
    }
}
