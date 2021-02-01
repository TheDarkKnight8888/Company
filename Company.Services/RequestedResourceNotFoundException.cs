using System;
using System.Runtime.Serialization;

namespace Company.Services
{
    [Serializable]
    public class RequestedResourceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/>.
        /// </summary>
        public RequestedResourceNotFoundException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/> with the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RequestedResourceNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedResourceNotFoundException"/> with the specified error message
        /// and a reference to the innier exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RequestedResourceNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }


        protected RequestedResourceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
