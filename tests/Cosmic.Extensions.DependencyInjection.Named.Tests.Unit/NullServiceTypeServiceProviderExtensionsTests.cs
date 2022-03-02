using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullServiceTypeServiceProviderExtensionsTests")]
    public class NullServiceTypeServiceProviderExtensionsTests
    {
        [Test]
        public void ShouldThrowWhenCallingGetNamedServiceWithNullServiceType()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedService(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowWhenCallingGetRequiredNamedServiceWithNullServiceType()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetRequiredNamedService(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowWhenCallingGetNamedServicesWithNullServiceType()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedServices(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }
    }
}
