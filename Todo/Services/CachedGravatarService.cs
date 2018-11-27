using System.Collections.Concurrent;
using System.Threading.Tasks;
using Todo.Models;

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

        public async Task<string> GetProfileImageUrl(string emailAddress)
        {
            return await _decoratedGravatarService.GetProfileImageUrl(emailAddress);
        }

        public async Task<string> GetProfileDisplayName(string emailAddress)
        {
            if (_cache.TryGetValue(emailAddress, out var profileDisplayName)) return profileDisplayName;

            var displayName = await _decoratedGravatarService.GetProfileDisplayName(emailAddress);

            _cache.TryAdd(emailAddress, displayName);

            return displayName;
        }

        public async Task<ProfileInfo> GetProfileInfo(string emailAddress)
        {
            return await _decoratedGravatarService.GetProfileInfo(emailAddress);
        }
    }
}
