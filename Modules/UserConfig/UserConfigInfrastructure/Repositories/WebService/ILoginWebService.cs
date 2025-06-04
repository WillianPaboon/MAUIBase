using Infrastructure.Repositories.WebService._base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserConfigInfrastructure.Models.Login;

namespace UserConfigInfrastructure.Repositories.WebService
{
    /// <summary>  
    /// Interface for login web service operations.  
    /// </summary>  
    public interface ILoginWebService
    {
        /// <summary>  
        /// Authenticates a user and retrieves a token response.  
        /// </summary>  
        /// <param name="username">The username of the user.</param>  
        /// <param name="password">The password of the user.</param>  
        /// <returns>A <see cref="TokenResponseDTO"/> containing the authentication token.</returns>  
        Task<TokenResponseDTO> LoginAsync(string username, string password);
    }
    /// <summary>
    /// Implementation of the login web service operations.
    /// </summary>
    public class LoginWebService : ILoginWebService
    {
        private IHttpClientFactory _httpClient;

        //private IApiClient _apiclient;

        public LoginWebService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            //_apiclient = apiClient ?? throw new ArgumentNullException(nameof(apiClient), "ApiClient cannot be null");
        }
        /// <inheritdoc/>  
        public async Task<TokenResponseDTO> LoginAsync(string username, string password)
        {
            TokenResponseDTO? response = default;
            Dictionary<string, string> request = new Dictionary<string, string>
            {
                ["grant_type"] = "password",
                ["client_id"] = "10a4204e-6bb7-4a21-908c-48b052ae8560",
                ["username"] = username,
                ["password"] = password,
                ["scope"] = "https://argosau.onmicrosoft.com/argosoneloginusaqa/general"
            };

            using (IApiClient client = new UserConfigClient(_httpClient))
            {
                response = await client.PostAsync<TokenResponseDTO>("argosau.onmicrosoft.com/B2C_1_Multitenant_ROPC_ArgosONE_AUTH/oauth2/v2.0/token", request).ConfigureAwait(true);
            }

            if (response.AccessToken == null)
            {
                throw new Exception("Token not found in response");
            }

            return response;
        }
    }
}
