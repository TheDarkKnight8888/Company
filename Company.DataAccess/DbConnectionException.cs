using System;
using System.Runtime.Serialization;

namespace Company.DataAccess
{
    [Serializable]
    public class DbConnectionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionException"/>.
        /// </summary>
        public DbConnectionException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionException"/> with the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public DbConnectionException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnectionException"/> with the specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DbConnectionException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DbConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
