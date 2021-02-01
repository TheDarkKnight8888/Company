using System;
using System.Runtime.Serialization;

namespace Company.DataAccess
{
    [Serializable]
    public class ExpressionConvertingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionConvertingException"/>.
        /// </summary>
        public ExpressionConvertingException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionConvertingException"/> with the specified error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public ExpressionConvertingException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionConvertingException"/> with the specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ExpressionConvertingException(string message, Exception innerException)
            : base(message, innerException) { }

        protected ExpressionConvertingException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
