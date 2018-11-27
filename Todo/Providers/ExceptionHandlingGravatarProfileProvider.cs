using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Todo.Providers
{
    public class ExceptionHandlingGravatarProfileProvider : IProfileProvider
    {
        private readonly IProfileProvider _decoratedProfileProvider;

        public ExceptionHandlingGravatarProfileProvider(IProfileProvider decoratedProfileProvider)
        {
            _decoratedProfileProvider = decoratedProfileProvider;
        }

        public async Task<Result<string>> GetImageUrl(string profileIdentifier)
        {
            try
            {
                return await _decoratedProfileProvider.GetImageUrl(profileIdentifier);
            }
            catch (Exception e)
            {
                return Result.Fail<string>(e.Message);
            }
        }

        public async Task<Result<string>> GetDisplayName(string profileIdentifier)
        {
            try
            {
                return await _decoratedProfileProvider.GetDisplayName(profileIdentifier);
            }
            catch (Exception e)
            {
                return Result.Fail<string>(e.Message);
            }
        }
    }
}
