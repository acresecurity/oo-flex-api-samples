
namespace Common.DataObjects
{
    public class AccessLevelGroup
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

        public virtual int RightsMask { get; set; }

        public virtual SecurityLevel SecurityLevel { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public virtual Guid UniqueKey { get; set; }
    }
}