using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceApi_dotNetFramework.ServicesImplementation
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"something went wrong calling the Api: {response.ReasonPhrase}");
            }

            var dataString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}