using System;
using System.Threading.Tasks;
using HttpClientWrapper.Client;

namespace HttpClientWrapper.Examples
{
    public static class BasicUsageExample
    {
        public static void Run()
        {
            Console.WriteLine("BasicUsageExample:");

            Task.Run(async () =>
            {
                using var client = new ApiClient("https://jsonplaceholder.typicode.com");

                var response = await client.GetAsync("/users/1");
                Console.WriteLine(response);

                if (response.IsSuccess)
                    Console.WriteLine("  Request succeeded.");
                else
                    Console.WriteLine("  Request failed.");
            }).GetAwaiter().GetResult();
        }
    }
}
