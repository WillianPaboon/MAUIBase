using CommonShared.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Repositories.WebService._base
{
    internal class ApiClient: IApiClient
    {
        private readonly HttpClient _httpClient;
        private string _token = string.Empty;

        public ApiClient(string baseUrl, TimeSpan timeout = default)
        { 
            //specify to use TLS 1.2 as default connection
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpClientHandler httpClientHandler = new()
            {
                UseProxy = true,
                Proxy = WebRequest.DefaultWebProxy,
            };

            //ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            //CertificateValidatorHelper.ValidateServerCertificate(sender: message, certificate: cert, chain: chain, sslPolicyErrors: errors)

            //httpClientHandler.ServerCertificateCustomValidationCallback = ValidatorCertificateHelper.ValidateServerCertificate;

            _httpClient = new HttpClient( httpClientHandler)
            {
                Timeout = timeout == default ? TimeSpan.FromSeconds(30) : timeout,
                BaseAddress = new Uri(baseUrl)
            };
        }

        public void SetToken(string token)
        {
            _token = token;
        }

        public async Task<T> GetAsync<T>(string endpoint, string token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Get, endpoint, null, token);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Get, null, response);
            return GetServiceResponse<T>(response);
        }

        public async Task<T> PostAsync<T>(string endpoint, object data, string token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Post, endpoint, data, token);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Post, data, response);
            return GetServiceResponse<T>(response);
        }

        public async Task<T> PutAsync<T>(string endpoint, object data, string token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Put, endpoint, data, token);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Put, data, response);
            return GetServiceResponse<T>(response);
        }

        public async Task<T> DeleteAsync<T>(string endpoint, string token = null)
        {
            HttpResponseMessage response = await SendRequest(HttpMethod.Delete, endpoint, null, token);
            LogRequestAndResponse<T>(endpoint, HttpMethod.Delete, null, response);
            return GetServiceResponse<T>(response);
        }


        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string endpoint, object data, string? token = null)
        {
            string? jsonData = data != null ? JsonSerializer.Serialize(data) : null;

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri($"{_httpClient.BaseAddress}/{endpoint}")
            };

            if (!string.IsNullOrEmpty(token) || !string.IsNullOrEmpty(_token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token ?? _token);
            }

            if (data != null)
            {
                request.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            }

            return await _httpClient.SendAsync(request);
        }

        public T GetServiceResponse<T>(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }

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


        public void LogRequestAndResponse<T>(string endpoint, HttpMethod httpMethod, object data, HttpResponseMessage response)
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

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
