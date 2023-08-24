
namespace Flex.DataObjects.Cardholder
{
    public partial class Photo
    {
        /// <summary>
        /// Is this a badge photo
        /// </summary>
        public bool Badge { get; set; }

        /// <summary>
        /// Is this photo contain biometric data
        /// </summary>
        public bool Biometric { get; set; }

        /// <summary>
        /// Is this the default photo
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// User defined description of the photo
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Is this photo for display purposes
        /// </summary>
        public bool Display { get; set; }

        /// <summary>
        /// What is the sort order for this photo
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Does the photo contain signature information
        /// </summary>
        public bool Signature { get; set; }

        /// <summary>
        /// Unique identifier for the record
        /// </summary>
        public Guid UniqueKey { get; set; }

        /// <summary>
        /// URL of photo
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Unique identifier for the cardholder
        /// </summary>
        public Guid PersonnelKey { get; set; }

        /// <summary>
        /// Identifier for the photo (internal use)
        /// </summary>
        public int PhotoId { get; set; }

    }
}