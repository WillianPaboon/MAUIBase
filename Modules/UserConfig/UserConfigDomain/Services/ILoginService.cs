
using Domain.Models.ValueObjects;
using UserConfigDomain.Models.Login;
using UserConfigInfrastructure.Models.Login;
using UserConfigInfrastructure.Repositories.WebService;

namespace UserConfigDomain.Services
{
    /// <summary>  
    /// Defines the contract for login services.  
    /// </summary>  
    public interface ILoginService
    {
        /// <summary>  
        /// Authenticates the user with the provided credentials.  
        /// </summary>
        /// <param name="credentials">The user credentials containing email and password.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains the access token.</returns>  
        Task<string> LoginAsync(UserCredentials credentials);
    }

    /// <summary>  
    /// Provides implementation for login services.  
    /// </summary>  
    internal class LoginService : ILoginService
    {
        private ILoginWebService _loginService;

        /// <summary>  
        /// Initializes a new instance of the <see cref="LoginService"/> class.  
        /// </summary>  
        /// <param name="loginService">The web service used for login operations.</param>  
        public LoginService(ILoginWebService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>  
        /// Authenticates the user with the provided credentials.  
        /// </summary>  
        /// <param name="credentials">The user credentials containing email and password.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains the access token.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when the token is not found in the response.</exception>  
        public async Task<string> LoginAsync(UserCredentials credentials)
        {
            TokenResponseDTO tokenResponse = await _loginService.LoginAsync(credentials.EmailValue, credentials.PassValue).ConfigureAwait(true);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("Token not found in response");
            }

            return tokenResponse.AccessToken;
        }
    }
}
