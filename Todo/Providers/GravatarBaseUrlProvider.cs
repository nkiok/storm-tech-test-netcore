namespace Todo.Providers
{
    public class GravatarBaseUrlProvider : IBaseUrlProvider
    {
        public string GetBaseUrl()
        {
            return "https://www.gravatar.com";
        }
    }
}
