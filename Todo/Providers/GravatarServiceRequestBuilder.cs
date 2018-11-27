using System.Net.Http;

namespace Todo.Providers
{
    public class GravatarServiceRequestBuilder : IRequestBuilder
    {
        public HttpRequestMessage BuildRequestMessage(HttpMethod httpMethod, string requestUri)
        {
            return new HttpRequestMessage(httpMethod, requestUri);
        }
    }
}
