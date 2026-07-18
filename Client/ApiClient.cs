using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpClientWrapper.Models;

namespace HttpClientWrapper.Client
{
    public class ApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public ApiClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public void AddHeader(string name, string value)
        {
            if (_httpClient.DefaultRequestHeaders.Contains(name))
                _httpClient.DefaultRequestHeaders.Remove(name);

            _httpClient.DefaultRequestHeaders.Add(name, value);
        }

        public async Task<ApiResponse> GetAsync(string path, int timeoutMs = 5000, int retries = 1)
        {
            var response = await RetryHandler.WithRetry(
                () => TimeoutHandler.WithTimeout(
                    ct => _httpClient.GetAsync(path, ct),
                    timeoutMs),
                retries);

            return await ToApiResponse(response);
        }

        public async Task<ApiResponse> PostJsonAsync(string path, string jsonBody, int timeoutMs = 5000, int retries = 1)
        {
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await RetryHandler.WithRetry(
                () => TimeoutHandler.WithTimeout(
                    ct => _httpClient.PostAsync(path, content, ct),
                    timeoutMs),
                retries);

            return await ToApiResponse(response);
        }

        private static async Task<ApiResponse> ToApiResponse(HttpResponseMessage message)
        {
            var body = await message.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                StatusCode = message.StatusCode,
                RawBody = body
            };
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
