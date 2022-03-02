using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullServiceNameServiceProviderExtensionTests")]
    public class NullServiceNameServiceProviderExtensionTests
    {
        [Test]
        public void ShouldThrowWhenCallingGetNamedServiceWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedService(typeof(ITestService), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetNamedServiceWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedService<ITestService>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowWhenCallingGetRequiredNamedServiceWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetRequiredNamedService(typeof(ITestService), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetRequiredNamedServiceWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetRequiredNamedService<ITestService>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowWhenCallingGetNamedServicesWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedServices(typeof(ITestService), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetNamedServicesWithNullServiceName()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedServices<ITestService>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }
    }
}
