using System.Net;
using System.Text.Json;

namespace HttpClientWrapper.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string RawBody { get; set; } = "";
        public bool IsSuccess => ((int)StatusCode >= 200 && (int)StatusCode < 300);

        public T? Deserialize<T>()
        {
            if (string.IsNullOrWhiteSpace(RawBody))
                return default;

            return JsonSerializer.Deserialize<T>(RawBody);
        }

        public override string ToString()
        {
            return $"Status: {(int)StatusCode} ({StatusCode})\nBody:\n{RawBody}";
        }
    }
}
