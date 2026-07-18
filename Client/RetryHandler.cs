using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientWrapper.Client
{
    public static class RetryHandler
    {
        public static async Task<HttpResponseMessage> WithRetry(
            Func<Task<HttpResponseMessage>> action,
            int maxRetries = 3,
            int delayMs = 500)
        {
            int attempt = 0;
            Exception? lastException = null;

            while (attempt < maxRetries)
            {
                try
                {
                    return await action();
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    attempt++;
                    if (attempt >= maxRetries)
                        throw;

                    await Task.Delay(delayMs);
                }
            }

            throw lastException ?? new Exception("Unknown error in retry handler.");
        }
    }
}
