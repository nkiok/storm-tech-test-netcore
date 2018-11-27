using System.Net.Http;

namespace Todo.Providers
{
    public class GravatarServiceRequestBuilder : IRequestBuilder
    {
        private readonly IServiceEndpointsProvider _serviceEndpointsProvider;

        public GravatarServiceRequestBuilder(IServiceEndpointsProvider serviceEndpointsProvider)
        {
            _serviceEndpointsProvider = serviceEndpointsProvider;
        }

        public HttpRequestMessage BuildRequestMessage(string profileIdentifier)
        {
            var requestUri = $"{GetBaseServiceUrl()}{GetProfileRoute(profileIdentifier)}";

            return new HttpRequestMessage(HttpMethod.Get, requestUri);
        }

        private string GetBaseServiceUrl()
        {
            return _serviceEndpointsProvider.GetBaseUrl();
        }

        private string GetProfileRoute(string resource)
        {
            return _serviceEndpointsProvider.GetProfileRoute(resource);
        }
    }
}
