using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Todo.Http;

namespace Todo.Providers
{
    public class GravatarProfileProvider : IProfileProvider
    {
        private readonly IServiceEndpointsProvider _serviceEndpointsProvider;
        private readonly IHttpClient _httpClient;
        private readonly IRequestBuilder _requestBuilder;

        public GravatarProfileProvider(IServiceEndpointsProvider serviceEndpointsProvider, IHttpClient httpClient, IRequestBuilder requestBuilder)
        {
            _serviceEndpointsProvider = serviceEndpointsProvider;
            _httpClient = httpClient;
            _requestBuilder = requestBuilder;
        }

        public Task<Result<string>> GetImageUrl(string profileIdentifier)
        {
            return Task.FromResult(Result.Ok($"{GetBaseServiceUrl()}{GetAvatarRoute(profileIdentifier)}?{GetImageSizeParam()}"));
        }

        public async Task<Result<string>> GetDisplayName(string profileIdentifier)
        {
            try
            {
                var request = _requestBuilder.BuildRequestMessage(profileIdentifier);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode) return Result.Fail<string>($"{response.StatusCode}-{response.ReasonPhrase}");

                var gravatarProfileResponse = await response.Content.ReadAsStringAsync();

                dynamic gravatarProfile = JsonConvert.DeserializeObject(gravatarProfileResponse);

                var displayName = Convert.ToString(gravatarProfile.entry[0].displayName);

                return Result.Ok(displayName);
            }
            catch (Exception ex)
            {
                return Result.Fail<string>(ex.Message);
            }
        }

        private string GetBaseServiceUrl()
        {
            return _serviceEndpointsProvider.GetBaseUrl();
        }

        private string GetAvatarRoute(string resource)
        {
            return _serviceEndpointsProvider.GetAvatarRoute(resource);
        }

        private static string GetImageSizeParam()
        {
            const int defaultImageSize = 30;

            return $"s={defaultImageSize}";
        }


    }
}
