using System;
using System.Net.Http;
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

        public GravatarProfileProvider(IServiceEndpointsProvider serviceEndpointsProvider, IHttpClient httpClient)
        {
            _serviceEndpointsProvider = serviceEndpointsProvider;
            _httpClient = httpClient;
        }

        public Task<Result<string>> GetImageUrl(string profileIdentifier)
        {
            return Task.FromResult(Result.Ok($"{GetBaseServiceUrl()}{GetAvatarRoute(profileIdentifier)}?{GetImageSizeParam()}"));
        }

        public async Task<Result<string>> GetDisplayName(string profileIdentifier)
        {
            try
            {
                var requestUri = $"{GetBaseServiceUrl()}{GetProfileRoute(profileIdentifier)}";

                var message = new HttpRequestMessage(HttpMethod.Get, requestUri);

                var response = await _httpClient.SendAsync(message);

                if (!response.IsSuccessStatusCode)
                    return Result.Fail<string>($"{response.StatusCode}-{response.ReasonPhrase}");

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

        private string GetProfileRoute(string resource)
        {
            return _serviceEndpointsProvider.GetProfileRoute(resource);
        }

        private static string GetImageSizeParam()
        {
            const int defaultImageSize = 30;

            return $"s={defaultImageSize}";
        }
    }
}
