using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using Shouldly;
using Todo.Providers;

namespace Todo.Tests
{
    public class ExceptionHandlingGravatarProfileProviderTests
    {
        private IProfileProvider _decoratedProfileProvider;
        private IProfileProvider _exceptionHandlingProfileProvider;

        [SetUp]
        public void SetUp()
        {
            _decoratedProfileProvider = Substitute.For<IProfileProvider>();

            _exceptionHandlingProfileProvider = new ExceptionHandlingGravatarProfileProvider(_decoratedProfileProvider);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task CallsDecoratedGetImageUrl(string emailAddress)
        {
            _decoratedProfileProvider.GetImageUrl(emailAddress).Returns(Result.Ok("anyurl"));

            var result = await _exceptionHandlingProfileProvider.GetImageUrl(emailAddress);

            result.IsSuccess.ShouldBeTrue();

            await _decoratedProfileProvider.Received().GetImageUrl(emailAddress);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task HandlesExceptionFromDecoratedGetImageUrl(string emailAddress)
        {
            _decoratedProfileProvider.GetImageUrl(emailAddress).Throws(new ArgumentException());

            var result = await _exceptionHandlingProfileProvider.GetImageUrl(emailAddress);

            result.IsFailure.ShouldBeTrue();

            await _decoratedProfileProvider.Received().GetImageUrl(emailAddress);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task CallsDecoratedGetDisplayName(string emailAddress)
        {
            _decoratedProfileProvider.GetDisplayName(emailAddress).Returns(Result.Ok("anyname"));

            var result = await _exceptionHandlingProfileProvider.GetDisplayName(emailAddress);

            result.IsSuccess.ShouldBeTrue();

            await _decoratedProfileProvider.Received().GetDisplayName(emailAddress);
        }

        [Test]
        [TestCase("anyemail")]
        public async Task HandlesExceptionFromDecoratedGetDisplayName(string emailAddress)
        {
            _decoratedProfileProvider.GetDisplayName(emailAddress).Throws(new ArgumentException());

            var result = await _exceptionHandlingProfileProvider.GetDisplayName(emailAddress);

            result.IsFailure.ShouldBeTrue();

            await _decoratedProfileProvider.Received().GetDisplayName(emailAddress);
        }
    }
}
