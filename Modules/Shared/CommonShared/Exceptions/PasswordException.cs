namespace CommonShared.Exceptions
{
    /// <summary>  
    /// Represents an exception that occurs when there is a problem related to passwords.  
    /// </summary>  
    public class PasswordException : Exception
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="PasswordException"/> class with a specified error message.  
        /// </summary>  
        /// <param name="message">The message that describes the error.</param>  
        public PasswordException(string message) : base(message)
        {
        }
                
        /// <summary>  
        /// Initializes a new instance of the <see cref="PasswordException"/> class with a specified error message and an inner exception.  
        /// </summary>  
        /// <param name="message">The message that describes the error.</param>  
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>  
        public PasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="PasswordException"/> class without parameters.  
        /// </summary>  
        public PasswordException()
        {
        }
    }
}
