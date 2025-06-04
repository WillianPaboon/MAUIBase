using CommonShared.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Infrastructure.Repositories.WebService._base
{
    /// <summary>
    /// Provides an implementation of the <see cref="IApiClient"/> interface for interacting with web services.
    /// </summary>
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiClient"/> class.
        /// </summary>
        /// <param name="httpClientFactory"> client Factory. Please register client in MauiProgram.cs </param>
        public ApiClient(HttpClient httpClientFactory)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            _httpClient = httpClientFactory;
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string endpoint, string? token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Get, endpoint, null, token).ConfigureAwait(true);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Get, null, response);
            return GetServiceResponse<T>(response);
        }

        /// <inheritdoc/>
        public async Task<T> PostFormAsync<T>(string endpoint, HttpContent content)
        {
            var response = await _httpClient.PostAsync(new Uri(endpoint), content).ConfigureAwait(true);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Post, null, response);
            return GetServiceResponse<T>(response);
        }

        /// <inheritdoc/>
        /// If you need to send a encoded content, please send data object with type: Dictionary<string-string>
        public async Task<T> PostAsync<T>(string endpoint, object data, string? token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Post, endpoint, data, token).ConfigureAwait(true);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Post, data, response);
            return GetServiceResponse<T>(response);
        }

        /// <inheritdoc/>
        public async Task<T> PutAsync<T>(string endpoint, object data, string? token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Put, endpoint, data, token).ConfigureAwait(true);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Put, data, response);
            return GetServiceResponse<T>(response);
        }

        /// <inheritdoc/>
        public async Task<T> DeleteAsync<T>(string endpoint, string? token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Delete, endpoint, null, token).ConfigureAwait(true);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Delete, null, response);
            return GetServiceResponse<T>(response);
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> SendCustomRequestAsync(HttpRequestMessage request)
        {
            return await _httpClient.SendAsync(request).ConfigureAwait(true);
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string endpoint, object? data, string? token = null)
        {
            CancellationToken cancellationToken = default;

            using (var request = new HttpRequestMessage(method, $"{_httpClient.BaseAddress}/{endpoint}"))
            {
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                if (data != null)
                {
                    HttpContent? content = null;
                    if (data is Dictionary<string, string> dictionaryData)
                    {
                        content = new FormUrlEncodedContent(dictionaryData);
                    }
                    else
                    {
                        var jsonContent = JsonSerializer.Serialize(data);
                        content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    }
                    request.Content = content;
                }
                return await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
            };
        }

        private static T GetServiceResponse<T>(HttpResponseMessage response)
        {
            ArgumentNullException.ThrowIfNull(response);

            // Manejo de la respuesta según el estado HTTP
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    var deserializedResult = JsonSerializer.Deserialize<T>(jsonString);

                    // Si el resultado deserializado es nulo, devolver la cadena "Ok"
                    return deserializedResult != null ? deserializedResult : (T)Convert.ChangeType("Ok", typeof(T));
                case HttpStatusCode.BadRequest:
                    throw new WebServiceErrorException(response.Content.ReadAsStringAsync().Result);
                case HttpStatusCode.NotFound:
                    throw new WebServiceErrorException("Service not found");
                case HttpStatusCode.InternalServerError:
                    throw new WebServiceErrorException("Internal Server Error");
                case HttpStatusCode.Unauthorized:
                    throw new WebServiceErrorException("Unauthorized");
                case HttpStatusCode.Forbidden:
                    throw new WebServiceErrorException("Forbidden");
                case HttpStatusCode.ServiceUnavailable:
                    throw new WebServiceErrorException("Service Unavailable");
                default:
                    throw new WebServiceErrorException($"Unhandled Status Code: {response.StatusCode}");
            }
        }

        private void LogRequestAndResponse<T>(string endpoint, HttpMethod httpMethod, object? data, HttpResponseMessage response)
        {
            // Registro de información de la solicitud y la respuesta
            Console.WriteLine($"Request to: {_httpClient.BaseAddress}/{endpoint}");
            Console.WriteLine($"HTTP Method: {httpMethod}");
            Console.WriteLine($"Request JSON: {JsonSerializer.Serialize(data)}");

            if (response != null)
            {
                Console.WriteLine($"Response JSON: {response.Content.ReadAsStringAsync().Result}");
            }
            else
            {
                Console.WriteLine("Response: No response received");
            }

            Console.WriteLine("------------------------------");
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the resources used by the <see cref="ApiClient"/> class.
        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            _httpClient.Dispose();

            isDisposed = true;
        }
    }
}
