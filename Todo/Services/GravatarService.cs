using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Providers;

namespace Todo.Services
{
    public class GravatarService : IGravatarService
    {
        private readonly IHashProvider _hashProvider;

        public GravatarService(IHashProvider hashProvider)
        {
            _hashProvider = hashProvider;
        }

        public string GetImgUrl(string emailAddress)
        {
            return $"{GetBaseServiceUrl()}/avatar/{GetHash(emailAddress)}?{GetImageSizeParam()}";
        }

        public async Task<string> GetProfileDisplayName(string emailAddress)
        {
            var requestUri = $"{GetHash(emailAddress)}.json";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(GetBaseServiceUrl());

                client.DefaultRequestHeaders.Add("User-Agent", "TodoApp");

                try
                {
                    var response = await client.GetAsync(requestUri);

                    if (!response.IsSuccessStatusCode) return string.Empty;

                    var gravatarProfileResponse = await response.Content.ReadAsStringAsync();

                    dynamic gravatarProfile = JsonConvert.DeserializeObject(gravatarProfileResponse);

                    return gravatarProfile.entry[0].displayName;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        private string GetHash(string emailAddress)
        {
            return _hashProvider.GetHash(emailAddress);
        }

        private static string GetBaseServiceUrl()
        {
            return "https://www.gravatar.com";
        }

        private static string GetImageSizeParam()
        {
            const int defaultImageSize = 30;

            return $"s={defaultImageSize}";
        }
    }
}