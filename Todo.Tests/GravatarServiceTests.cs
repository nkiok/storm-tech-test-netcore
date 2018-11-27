using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Todo.Providers;
using Todo.Services;

namespace Todo.Tests
{
    public class GravatarServiceTests
    {
        private IGravatarService _gravatarService;
        private IHashProvider _hashProvider;
        private IServiceEndpointsProvider _serviceEndpointsProvider;

        [SetUp]
        public void SetUp()
        {
            _hashProvider = Substitute.For<IHashProvider>();

            _serviceEndpointsProvider = Substitute.For<IServiceEndpointsProvider>();

            _gravatarService = new GravatarService(_hashProvider, _serviceEndpointsProvider);
        }

        [Test]
        [TestCase("anyemail", "anyhash", "https://www.gravatar.com", "/someroute/", "https://www.gravatar.com/someroute/anyhash?s=30")]
        public void ReturnsExpectedImgUrl(string email, string hash, string baseUrl, string avatarRoute, string expectedResult)
        {
            _hashProvider.GetHash(email).Returns(hash);

            _serviceEndpointsProvider.GetBaseUrl().Returns(baseUrl);

            _serviceEndpointsProvider.GetAvatarRoute().Returns(avatarRoute);

            var result = _gravatarService.GetImgUrl(email);

            _hashProvider.Received().GetHash(Arg.Any<string>());

            _serviceEndpointsProvider.Received().GetBaseUrl();

            _serviceEndpointsProvider.Received().GetAvatarRoute();

            result.ShouldBe(expectedResult);
        }

        private static IDictionary<string, string> SomeRoutes(string someroute)
        {
            return new Dictionary<string, string>()
            { { someroute, someroute}};
        }
    }
}
