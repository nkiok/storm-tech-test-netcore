using System.Net.Http;
using System.Threading.Tasks;

namespace Todo.Http
{
    public interface IHttpClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage message);
    }
}
