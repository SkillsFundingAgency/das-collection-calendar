using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace SFA.DAS.CollectionCalendar.AcceptanceTests
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
        public static async Task<(HttpStatusCode, T)> GetValueAsync<T>(this HttpClient client, string url, CancellationToken cancellationToken = default)
        {
            using var response = await client.GetAsync(url, cancellationToken);
            return await ProcessResponse<T>(response);
        }

        private static async Task<(HttpStatusCode, T)> ProcessResponse<T>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent)
                return (response.StatusCode, default);

            var content = await response.Content.ReadAsStringAsync();
            var responseValue = JsonSerializer.Deserialize<T>(content, JsonOptions);

            return (response.StatusCode, responseValue);
        }
    }
}
