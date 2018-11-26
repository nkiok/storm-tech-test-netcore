using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Todo.Services
{
    public class CachedGravatarService : IGravatarService
    {
        private readonly IGravatarService _decoratedGravatarService;
        private readonly ConcurrentDictionary<string, string> _cache;

        public CachedGravatarService(IGravatarService decoratedGravatarService)
        {
            _decoratedGravatarService = decoratedGravatarService;

            _cache = new ConcurrentDictionary<string, string>();
        }

        public string GetImgUrl(string emailAddress)
        {
            return _decoratedGravatarService.GetImgUrl(emailAddress);
        }

        public async Task<string> GetProfileDisplayName(string emailAddress)
        {
            if (_cache.TryGetValue(emailAddress, out var profileDisplayName)) return profileDisplayName;

            var result = await _decoratedGravatarService.GetProfileDisplayName(emailAddress);

            _cache.TryAdd(emailAddress, result);

            return result;
        }
    }
}
