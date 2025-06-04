namespace Infrastructure.Repositories.WebService._base
{
    /// <summary>  
    /// Interface for API client operations.  
    /// </summary>  
    public interface IApiClient : IDisposable
    {
        /// <summary>  
        /// Sends a custom HTTP request asynchronously.  
        /// </summary>  
        /// <param name="request">The HTTP request message.</param>  
        /// <returns>A task representing the asynchronous operation, containing the HTTP response message.</returns>  
        Task<HttpResponseMessage> SendCustomRequestAsync(HttpRequestMessage request);

        /// <summary>  
        /// Sends a GET request to the specified endpoint asynchronously.  
        /// </summary>  
        /// <typeparam name="T">The type of the response data.</typeparam>  
        /// <param name="endpoint">The API endpoint.</param>  
        /// <param name="token">The authorization token (optional).</param>  
        /// <returns>A task representing the asynchronous operation, containing the response data.</returns>  
        Task<T> GetAsync<T>(string endpoint, string? token = null);

        /// <summary>  
        /// Sends a POST request to the specified endpoint asynchronously.  
        /// </summary>  
        /// <typeparam name="T">The type of the response data.</typeparam>  
        /// <param name="endpoint">The API endpoint.</param>  
        /// <param name="data">The data to be sent in the request body.</param>  
        /// <param name="token">The authorization token (optional).</param>  
        /// <returns>A task representing the asynchronous operation, containing the response data.</returns>  
        Task<T> PostAsync<T>(string endpoint, object data, string? token = null);

        /// <summary>  
        /// Sends a PUT request to the specified endpoint asynchronously.  
        /// </summary>  
        /// <typeparam name="T">The type of the response data.</typeparam>  
        /// <param name="endpoint">The API endpoint.</param>  
        /// <param name="data">The data to be sent in the request body.</param>  
        /// <param name="token">The authorization token (optional).</param>  
        /// <returns>A task representing the asynchronous operation, containing the response data.</returns>  
        Task<T> PutAsync<T>(string endpoint, object data, string? token = null);

        /// <summary>  
        /// Sends a DELETE request to the specified endpoint asynchronously.  
        /// </summary>  
        /// <typeparam name="T">The type of the response data.</typeparam>  
        /// <param name="endpoint">The API endpoint.</param>  
        /// <param name="token">The authorization token (optional).</param>  
        /// <returns>A task representing the asynchronous operation, containing the response data.</returns>  
        Task<T> DeleteAsync<T>(string endpoint, string? token = null);
    }
}
