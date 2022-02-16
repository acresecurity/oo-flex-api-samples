
namespace Common.DataObjects
{
    /// <summary>
    /// Validation errors that were sent by the server
    /// </summary>
    public class ValidationError
    {
        public string Field { get; set; }

        /// <summary>The error message</summary>
        public string Message { get; set; }

        /// <summary>The property value that caused the failure.</summary>
        public string AttemptedValue { get; set; }
    }
}
