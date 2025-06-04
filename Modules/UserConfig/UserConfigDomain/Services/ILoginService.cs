
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
        /// Authenticates the user with the provided username and password.  
        /// </summary>  
        /// <param name="username">The username of the user.</param>  
        /// <param name="password">The password of the user.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains the access token.</returns>  
        Task<string> LoginAsync(string username, string password);
    }



    /// <summary>  
    /// Provides implementation for login services.  
    /// </summary>  
    public class LoginService : ILoginService
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
        /// Authenticates the user with the provided username and password.  
        /// </summary>  
        /// <param name="username">The username of the user.</param>  
        /// <param name="password">The password of the user.</param>  
        /// <returns>A task that represents the asynchronous operation. The task result contains the access token.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when the token is not found in the response.</exception>  
        public async Task<string> LoginAsync(string username, string password)
        {
            TokenResponseDTO tokenResponse = await _loginService.LoginAsync(username, password).ConfigureAwait(true);

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
            {
                throw new InvalidOperationException("Token not found in response");
            }

            return tokenResponse.AccessToken;
        }
    }
}
