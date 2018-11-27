namespace Todo.Providers
{
    public interface IServiceEndpointsProvider
    {
        string GetBaseUrl();

        string GetAvatarRoute(string resource);

        string GetProfileRoute(string resource);
    }
}
