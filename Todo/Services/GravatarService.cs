using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Todo.Models;
using Todo.Providers;

namespace Todo.Services
{
    public class GravatarService : IGravatarService
    {
        private readonly IProfileProvider _profileProvider;

        public GravatarService(IProfileProvider profileProvider)
        {
            _profileProvider = profileProvider;
        }

        public async Task<ProfileInfo> GetProfileInfo(string profileIdentifier)
        {
            var tasks = new List<Task<Result<string>>>
            {
                _profileProvider.GetImageUrl(profileIdentifier),
                _profileProvider.GetDisplayName(profileIdentifier)
            }
            .ToArray();

            var results = await Task.WhenAll(tasks);

            return new ProfileInfo()
            {
                ProfileIdentifier = new ProfileIdentifier() { EmailAddress = profileIdentifier },
                AvatarUrl = results[0].IsSuccess ? results[0].Value : string.Empty,
                DisplayName = results[1].IsSuccess ? results[1].Value : string.Empty,
            };
        }
    }
}