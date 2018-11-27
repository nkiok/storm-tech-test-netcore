using System.Net.Http;
using System.Threading.Tasks;

namespace Todo.Http
{
    public class HttpClientFacade : IHttpClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage message)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "TodoApp");

            return _httpClient.SendAsync(message);
        }
    }
}