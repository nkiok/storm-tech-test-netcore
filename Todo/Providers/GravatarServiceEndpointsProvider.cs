namespace Todo.Providers
{
    public class GravatarServiceEndpointsProvider : IServiceEndpointsProvider
    {
        private readonly IHashProvider _hashProvider;
        private readonly IBaseUrlProvider _baseUrlProvider;

        public GravatarServiceEndpointsProvider(IHashProvider hashProvider, IBaseUrlProvider baseUrlProvider)
        {
            _hashProvider = hashProvider;
            _baseUrlProvider = baseUrlProvider;
        }

        public string GetBaseUrl()
        {
            return _baseUrlProvider.GetBaseUrl();
        }

        public string GetAvatarRoute(string resource)
        {
            return $"/avatar/{_hashProvider.GetHash(resource)}";
        }

        public string GetProfileRoute(string resource)
        {
            return $"/{_hashProvider.GetHash(resource)}.json";
        }
    }
}
