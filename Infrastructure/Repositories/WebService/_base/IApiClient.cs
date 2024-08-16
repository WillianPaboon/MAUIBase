namespace Infrastructure.Repositories.WebService._base
{
    internal interface IApiClient : IDisposable
    {
        Task<T> GetAsync<T>(string endpoint, string token = null);
        Task<T> PostAsync<T>(string endpoint, object data, string token = null);
        Task<T> PutAsync<T>(string endpoint, object data, string token = null);
        Task<T> DeleteAsync<T>(string endpoint, string token = null);
        T GetServiceResponse<T>(HttpResponseMessage response);
        void LogRequestAndResponse<T>(string endpoint, HttpMethod httpMethod, object data, HttpResponseMessage response);
        void SetToken(string token);
    }
}
