using System.Net.Http;

namespace Todo.Providers
{
    public interface IRequestBuilder
    {
        HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, string requestUri);
    }
}
