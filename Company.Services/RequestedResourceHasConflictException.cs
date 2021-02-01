using System;
using System.Runtime.Serialization;

namespace Company.Services
{
    /// <summary>
    /// Represents an exception class of has conflict error.
    /// </summary>
    [Serializable]
    public class RequestedResourceHasConflictException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/>.
        /// </summary>
        public RequestedResourceHasConflictException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/> with the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RequestedResourceHasConflictException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/> with the specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RequestedResourceHasConflictException(string message, Exception innerException)
            : base(message, innerException) { }

        protected RequestedResourceHasConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
