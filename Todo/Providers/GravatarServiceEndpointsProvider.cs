namespace Todo.Providers
{
    public class GravatarServiceEndpointsProvider : IServiceEndpointsProvider
    {
        public string GetBaseUrl()
        {
            return "https://www.gravatar.com";
        }

        public string GetAvatarRoute()
        {
            return "/avatar/";
        }

        public string GetProfileRoute()
        {
            return "/";
        }
    }
}
