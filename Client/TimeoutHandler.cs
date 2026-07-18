using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientWrapper.Client
{
    public static class TimeoutHandler
    {
        public static async Task<HttpResponseMessage> WithTimeout(
            Func<CancellationToken, Task<HttpResponseMessage>> action,
            int timeoutMs = 5000)
        {
            using var cts = new CancellationTokenSource(timeoutMs);

            try
            {
                return await action(cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException($"Request timed out after {timeoutMs} ms.");
            }
        }
    }
}
