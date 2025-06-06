using CommonShared.Exceptions;
using Domain.Models.ValueObjects.Primitives;
using System.Text.RegularExpressions;

namespace Domain.Models.ValueObjects
{
    /// <summary>  
    /// Represents the credentials of a user, including email and password.  
    /// </summary>  
    public class UserCredentials
    {
        private readonly Regex _hasUpperCase = new Regex(@"[A-Z]");
        private readonly Regex _hasLowerCase = new Regex(@"[a-z]");
        private readonly Regex _hasNumber = new Regex(@"[0-9]");
        private readonly Regex _hasSymbol = new Regex(@"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]");
        private const int _minimumLength = 8;

        private string _passwordValue = string.Empty;
        private Email _emailValue;

        /// <summary>  
        /// Gets or sets the email value associated with the user credentials.
        /// </summary>  
        public string EmailValue
        {
            get => _emailValue.Value;
        }

        /// <summary>  
        /// Gets or sets the password value associated with the user credentials.  
        /// </summary>  
        public string PassValue
        {
            get => _passwordValue;
            set
            {
                ExecuteEvaluations(value);
                _passwordValue = value;
            }
        }


        /// <summary>  
        /// Initializes a new instance of the <see cref="UserCredentials"/> class with specified email and password.  
        /// </summary>  
        /// <param name="email">The email address of the user.</param>  
        /// <param name="password">The password of the user.</param>  
        public UserCredentials(string email, string password)
        {
            _emailValue = new(email);
            PassValue = password;
        }

        /// <summary>  
        /// Executes evaluations to determine the validity of the user password.  
        /// </summary>  
        /// <param name="passwordValue">The password value to evaluate.</param>  
        private void ExecuteEvaluations(string passwordValue)
        {
            if(string.IsNullOrWhiteSpace(passwordValue) || passwordValue.Length < _minimumLength)
                throw new PasswordException($"Password must be at least {_minimumLength} characters long.");

            if (!_hasUpperCase.IsMatch(passwordValue))
                throw new PasswordException("Password must contain at least one uppercase letter.");

            if (!_hasLowerCase.IsMatch(passwordValue))
                throw new PasswordException($"Password must contain at least one lowercase letter.");

            if (!_hasSymbol.IsMatch(passwordValue))
                throw new PasswordException("Password must contain at least one special character.");

            if (!_hasNumber.IsMatch(passwordValue))
                throw new PasswordException("Password must contain at least one number.");
        }
    }
}
