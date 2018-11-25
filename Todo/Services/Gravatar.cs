using Todo.Providers;

namespace Todo.Services
{
    public class Gravatar
    {
        private readonly IHashProvider _hashProvider;

        public Gravatar(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        public string GetHash(string emailAddress)
        {
            return _hashProvider.GetHash(emailAddress);
        }
    }
}