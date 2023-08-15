
namespace Flex.DataObjects.Hardware
{
    public partial class AccessLevelGroup
    {
        /// <summary>
        /// Informs you if the operator that requested the access level group has the rights to assign it to a credential.
        /// </summary>
        public virtual bool? CanAssign { get; set; }

        /// <summary>
        /// A system assigned access description for the AccessLevelGroup
        /// </summary>
        public virtual string Description { get; set; }

        public virtual AccessLevelType GroupType { get; set; }

        /// <summary>
        /// A display name of the access level group
        /// </summary>
        public virtual string Name { get; set; }

        public virtual SecurityLevel SecurityLevel { get; set; }

        public virtual Escort? Escort { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual Guid UniqueKey { get; set; }

        public virtual DateTime? Activation { get; set; }

        public virtual DateTime? Deactivation { get; set; }

        public IEnumerable<AccessLevelGroupItem> Items { get; set; } = Array.Empty<AccessLevelGroupItem>();
    }
}