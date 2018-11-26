using Todo.Providers;

namespace Todo.Services
{
    public class GravatarService : IGravatarService
    {
        private readonly IHashProvider _hashProvider;

        public GravatarService(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        public string GetImgUrl(string emailAddress)
        {
            return $"{GetServiceUrl()}/{GetHash(emailAddress)}?{GetImageSizeParam()}";
        }

        private string GetHash(string emailAddress)
        {
            return _hashProvider.GetHash(emailAddress);
        }

        private static string GetServiceUrl()
        {
            return "https://www.gravatar.com/avatar";
        }

        private static string GetImageSizeParam()
        {
            const int defaultImageSize = 30;

            return $"s={defaultImageSize}";
        }
    }
}