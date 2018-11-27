using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        public async Task<string> GetProfileImageUrl(string emailAddress)
        {
            var result = await _profileProvider.GetImageUrl(emailAddress);

            return result.IsSuccess
                ? result.Value
                : string.Empty;
        }

        public async Task<string> GetProfileDisplayName(string emailAddress)
        {
            var result = await _profileProvider.GetDisplayName(emailAddress);

            return result.IsSuccess
                ? result.Value
                : string.Empty;
        }

        public async Task<ProfileInfo> GetProfileInfo(string emailAddress)
        {
            var tasks = new List<Task<Result<string>>>
            {
                _profileProvider.GetImageUrl(emailAddress),
                _profileProvider.GetDisplayName(emailAddress)
            }
            .ToArray();

            var results = await Task.WhenAll(tasks);

            return new ProfileInfo()
            {
                ProfileIdentifier = new ProfileIdentifier() { EmailAddress =  emailAddress},
                AvatarUrl = results[0].IsSuccess ? results[0].Value : string.Empty,
                DisplayName = results[1].IsSuccess ? results[1].Value : string.Empty,
            };
        }
    }
}