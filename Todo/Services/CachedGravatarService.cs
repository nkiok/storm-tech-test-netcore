using System.Collections.Concurrent;
using System.Threading.Tasks;
using Todo.Models;

namespace Todo.Services
{
    public class CachedGravatarService : IGravatarService
    {
        private readonly IGravatarService _decoratedGravatarService;
        private readonly ConcurrentDictionary<string, ProfileInfo> _cache;

        public CachedGravatarService(IGravatarService decoratedGravatarService)
        {
            _decoratedGravatarService = decoratedGravatarService;

            _cache = new ConcurrentDictionary<string, ProfileInfo>();
        }

        public async Task<ProfileInfo> GetProfileInfo(string emailAddress)
        {
            if (_cache.TryGetValue(emailAddress, out var cachedProfileInfo)) return cachedProfileInfo;

            var profileInfo = await _decoratedGravatarService.GetProfileInfo(emailAddress);

            _cache.TryAdd(emailAddress, profileInfo);

            return profileInfo;
        }
    }
}
