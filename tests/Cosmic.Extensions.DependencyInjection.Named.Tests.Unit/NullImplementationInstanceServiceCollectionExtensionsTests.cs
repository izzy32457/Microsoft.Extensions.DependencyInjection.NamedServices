using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
// ReSharper disable RedundantCast

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullImplementationInstanceServiceCollectionExtensionsTests")]
    public class NullImplementationInstanceServiceCollectionExtensionsTests
    {
        [Test]
        public void ShouldThrowForNullImplementationInstanceWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), (TestService1)null!, "test-service"));

            ex!.ParamName.Should().Be("implementationInstance");
        }

        [Test]
        public void ShouldThrowForNullImplementationInstanceWhenCallingAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>((TestService1)null!, "test-service"));

            ex!.ParamName.Should().Be("implementationInstance");
        }

        [Test]
        public void ShouldThrowForNullImplementationInstanceWhenCallingAddSingletonWithImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton((TestService1)null!, "test-service"));

            ex!.ParamName.Should().Be("implementationInstance");
        }
    }
}
