using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Todo.Http;
using Todo.Providers;

namespace Todo.Tests
{
    public class GravatarProfileProviderTests
    {
        private IServiceEndpointsProvider _serviceEndpointsProvider;
        private IHttpClient _httpClient;
        private IRequestBuilder _requestBuilder;
        private IProfileProvider _profileProvider;

        [SetUp]
        public void SetUp()
        {
            _serviceEndpointsProvider = Substitute.For<IServiceEndpointsProvider>();

            _httpClient = Substitute.For<IHttpClient>();

            _requestBuilder = Substitute.For<IRequestBuilder>();

            _profileProvider = new GravatarProfileProvider(_serviceEndpointsProvider, _httpClient, _requestBuilder);
        }

        [Test]
        [TestCase("anyemail", "anyurl", "/anyroute/anyhash", "anyurl/anyroute/anyhash?s=30")]
        public async Task ReturnsExpectedImageUrl(string emailAddress, string baseUrl, string route, string expectedResult)
        {
            _serviceEndpointsProvider.GetBaseUrl().Returns(baseUrl);

            _serviceEndpointsProvider.GetAvatarRoute(emailAddress).Returns(route);

            var imageUrl = await _profileProvider.GetImageUrl(emailAddress);

            _serviceEndpointsProvider.Received().GetBaseUrl();

            _serviceEndpointsProvider.Received().GetAvatarRoute(emailAddress);

            imageUrl.IsSuccess.ShouldBeTrue();

            imageUrl.Value.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase("anyemail", "anyurl", "/anyroute/anyhash.json", "anyurl/anyroute/anyhash.json", "{\"entry\":[{\"displayName\":\"anyname\"}]}", "anyname")]
        public async Task ReturnsExpectedDisplayName(string emailAddress, string baseUrl, string route, string requestUri, string responseContent, string expectedName)
        {
            _serviceEndpointsProvider.GetBaseUrl().Returns(baseUrl);

            _serviceEndpointsProvider.GetProfileRoute(emailAddress).Returns(route);

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            _requestBuilder.BuildRequestMessage(HttpMethod.Get, requestUri).Returns(request);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(responseContent)
            };

            _httpClient.SendAsync(request).Returns(response);

            var displayName = await _profileProvider.GetDisplayName(emailAddress);

            _serviceEndpointsProvider.Received().GetBaseUrl();

            _serviceEndpointsProvider.Received().GetProfileRoute(emailAddress);

            _requestBuilder.Received().BuildRequestMessage(HttpMethod.Get, requestUri);

            await _httpClient.Received().SendAsync(request);

            displayName.IsSuccess.ShouldBeTrue();

            displayName.Value.ShouldBe(expectedName);
        }

        [Test]
        [TestCase("anyemail", "anyurl", "/anyroute/anyhash.json", "anyurl/anyroute/anyhash.json")]
        public async Task ReturnsEmptyDisplayNameWhenHttpRequestFails(string emailAddress, string baseUrl, string route, string requestUri)
        {
            _serviceEndpointsProvider.GetBaseUrl().Returns(baseUrl);

            _serviceEndpointsProvider.GetProfileRoute(emailAddress).Returns(route);

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            _requestBuilder.BuildRequestMessage(HttpMethod.Get, requestUri).Returns(request);

            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            _httpClient.SendAsync(request).Returns(response);

            var displayName = await _profileProvider.GetDisplayName(emailAddress);

            _serviceEndpointsProvider.Received().GetBaseUrl();

            _serviceEndpointsProvider.Received().GetProfileRoute(emailAddress);

            _requestBuilder.Received().BuildRequestMessage(HttpMethod.Get, requestUri);

            await _httpClient.Received().SendAsync(request);

            displayName.IsFailure.ShouldBeTrue();
        }
    }
}
