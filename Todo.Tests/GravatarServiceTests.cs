using System.Threading.Tasks;
using CSharpFunctionalExtensions;
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
        private IProfileProvider _profileProvider;

        [SetUp]
        public void SetUp()
        {
            _profileProvider = Substitute.For<IProfileProvider>();

            _gravatarService = new GravatarService(_profileProvider);
        }

        [Test]
        [TestCase("anyemail", "https://www.someurl.com/someavatarroute/anyhash?s=30")]
        public async Task ReturnsExpectedProfileImageUrl(string email, string expectedResult)
        {
            _profileProvider.GetImageUrl(email).Returns(Result.Ok(expectedResult));

            var result = await _gravatarService.GetProfileImageUrl(email);

            await _profileProvider.Received().GetImageUrl(email);

            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase("anyemail", "https://www.someurl.com/someavatarroute/anyhash?s=30", "anyname")]
        public async Task ReturnsExpectedProfileInfo(string emailAddress, string expectedUrl, string expectedName)
        {
            _profileProvider.GetImageUrl(emailAddress).Returns(Result.Ok(expectedUrl));

            _profileProvider.GetDisplayName(emailAddress).Returns(Result.Ok(expectedName));

            var result = await _gravatarService.GetProfileInfo(emailAddress);

            await _profileProvider.Received().GetImageUrl(emailAddress);

            await _profileProvider.Received().GetDisplayName(emailAddress);

            result.AvatarUrl.ShouldBe(expectedUrl);
            result.ProfileIdentifier.Username.ShouldBe(expectedName);
            result.ProfileIdentifier.EmailAddress.ShouldBe(emailAddress);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task ReturnsEmptyWhenGetImageUrlFails(string email)
        {
            _profileProvider.GetImageUrl(email).Returns(Result.Fail<string>("error"));

            var result = await _gravatarService.GetProfileImageUrl(email);

            await _profileProvider.Received().GetImageUrl(email);

            result.ShouldBeEmpty();
        }

        [Test]
        [TestCase("anyemail", "anyname")]
        public async Task ReturnsExpectedProfileDisplayName(string email, string expectedResult)
        {
            _profileProvider.GetDisplayName(email).Returns(Result.Ok(expectedResult));

            var result = await _gravatarService.GetProfileDisplayName(email);

            await _profileProvider.Received().GetDisplayName(email);

            result.ShouldBe(expectedResult);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task ReturnsEmptyWhenGetDisplayNameFails(string email)
        {
            _profileProvider.GetDisplayName(email).Returns(Result.Fail<string>("error"));

            var result = await _gravatarService.GetProfileDisplayName(email);

            await _profileProvider.Received().GetDisplayName(email);

            result.ShouldBeEmpty();
        }
    }
}
