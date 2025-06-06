using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonShared.Exceptions
{
    /// <summary>  
    /// Represents an exception that occurs when there is a problem with an email address.  
    /// </summary>  
    public class EmailAddressException : Exception
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="EmailAddressException"/> class.  
        /// </summary>  
        public EmailAddressException()
        {
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="EmailAddressException"/> class with a specified error message.  
        /// </summary>  
        /// <param name="message">The message that describes the error.</param>  
        public EmailAddressException(string message) : base(message)
        {
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="EmailAddressException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.  
        /// </summary>  
        /// <param name="message">The message that describes the error.</param>  
        /// <param name="innerException">The exception that is the cause of the current exception.</param>  
        public EmailAddressException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
