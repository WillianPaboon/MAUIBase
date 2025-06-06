using CommonShared.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Models.ValueObjects.Primitives
{
    /// <summary>
    /// Represents an email value object with validation.
    /// </summary>
    public class Email
    {
        private const string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        private string _value = string.Empty;

        /// <summary>
        /// Gets or sets the email value. Setting the value also validates it.
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                if(string.IsNullOrEmpty(value))
                    throw new EmailAddressException($"Please, insert your user data");

                if (!Regex.IsMatch(value, EmailRegexPattern))
                    throw new EmailAddressException($"invalid email: {value}");

                _value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class with the specified email value.
        /// </summary>
        /// <param name="email">The email value to initialize with.</param>
        public Email(string email) => Value = email;
    }
}
