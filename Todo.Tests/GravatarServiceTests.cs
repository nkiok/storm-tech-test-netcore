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
        [TestCase("anyemail", "https://www.someurl.com/someavatarroute/anyhash?s=30", "anyname")]
        public async Task ReturnsExpectedProfileInfo(string emailAddress, string expectedUrl, string expectedName)
        {
            _profileProvider.GetImageUrl(emailAddress).Returns(Result.Ok(expectedUrl));

            _profileProvider.GetDisplayName(emailAddress).Returns(Result.Ok(expectedName));

            var result = await _gravatarService.GetProfileInfo(emailAddress);

            await _profileProvider.Received().GetImageUrl(emailAddress);

            await _profileProvider.Received().GetDisplayName(emailAddress);

            result.AvatarUrl.ShouldBe(expectedUrl);
            result.DisplayName.ShouldBe(expectedName);
            result.ProfileIdentifier.EmailAddress.ShouldBe(emailAddress);
        }

        [Test]
        [TestCase("anyemail", "anyname")]
        public async Task ProfileImageUrlIsEmptyWhenGetImageUrlFails(string emailAddress, string expectedName)
        {
            _profileProvider.GetImageUrl(emailAddress).Returns(Result.Fail<string>("error"));

            _profileProvider.GetDisplayName(emailAddress).Returns(Result.Ok(expectedName));

            var result = await _gravatarService.GetProfileInfo(emailAddress);

            await _profileProvider.Received().GetImageUrl(emailAddress);

            await _profileProvider.Received().GetDisplayName(emailAddress);

            result.AvatarUrl.ShouldBeEmpty();
            result.DisplayName.ShouldBe(expectedName);
            result.ProfileIdentifier.EmailAddress.ShouldBe(emailAddress);
        }

        [Test]
        [TestCase("anyemail", "https://www.someurl.com/someavatarroute/anyhash?s=30")]
        public async Task ProfileDisplayNameIsEmptyWhenGetDisplayNameFails(string emailAddress, string expectedUrl)
        {
            _profileProvider.GetImageUrl(emailAddress).Returns(Result.Ok(expectedUrl));

            _profileProvider.GetDisplayName(emailAddress).Returns(Result.Fail<string>("error"));

            var result = await _gravatarService.GetProfileInfo(emailAddress);

            await _profileProvider.Received().GetImageUrl(emailAddress);

            await _profileProvider.Received().GetDisplayName(emailAddress);

            result.AvatarUrl.ShouldBe(expectedUrl);
            result.DisplayName.ShouldBeEmpty();
            result.ProfileIdentifier.EmailAddress.ShouldBe(emailAddress);
        }
    }
}
