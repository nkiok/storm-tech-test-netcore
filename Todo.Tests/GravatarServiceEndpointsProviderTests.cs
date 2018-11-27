using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Todo.Providers;

namespace Todo.Tests
{
    public class GravatarServiceEndpointsProviderTests
    {
        private IServiceEndpointsProvider _serviceEndpointsProvider;
        private IHashProvider _hashProvider;
        private IBaseUrlProvider _baseUrlProvider;

        [SetUp]
        public void SetUp()
        {
            _hashProvider = Substitute.For<IHashProvider>();

            _baseUrlProvider = Substitute.For<IBaseUrlProvider>();

            _serviceEndpointsProvider = new GravatarServiceEndpointsProvider(_hashProvider, _baseUrlProvider);
        }

        [Test]
        [TestCase("anybaseurl", "anybaseurl")]
        public void ReturnsExpectedGetBaseUrl(string baseUrl, string expectedBaseUrl)
        {
            _baseUrlProvider.GetBaseUrl().Returns(baseUrl);

            var result = _serviceEndpointsProvider.GetBaseUrl();

            _baseUrlProvider.Received().GetBaseUrl();

            result.ShouldBe(expectedBaseUrl);
        }

        [Test]
        [TestCase("anyemail", "anyhash", "/avatar/anyhash")]
        public void ReturnsExpectedAvatarRoute(string emailAddress, string hash, string expectedRoute)
        {
            _hashProvider.GetHash(emailAddress).Returns(hash);

            var result = _serviceEndpointsProvider.GetAvatarRoute(emailAddress);

            _hashProvider.Received().GetHash(emailAddress);

            result.ShouldBe(expectedRoute);
        }

        [Test]
        [TestCase("anyemail", "anyhash", "/anyhash.json")]
        public void ReturnsExpectedProfileRoute(string emailAddress, string hash, string expectedRoute)
        {
            _hashProvider.GetHash(emailAddress).Returns(hash);

            var result = _serviceEndpointsProvider.GetProfileRoute(emailAddress);

            _hashProvider.Received().GetHash(emailAddress);

            result.ShouldBe(expectedRoute);
        }
    }
}
