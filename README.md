# HttpClientWrapper

A small, beginner-friendly C# HTTP client wrapper designed to make API calls cleaner and more reusable for QA engineers and developers.

## Features

- Simple `ApiClient` with `GetAsync` and `PostJsonAsync`
- Built-in retry logic
- Built-in timeout handling
- Unified `ApiResponse` model with `IsSuccess` and JSON deserialization
- Header injection for API keys, tokens, etc.

## Example

```csharp
using var client = new ApiClient("https://jsonplaceholder.typicode.com");
var response = await client.GetAsync("/users/1");

if (response.IsSuccess)
{
    Console.WriteLine(response.RawBody);
}
