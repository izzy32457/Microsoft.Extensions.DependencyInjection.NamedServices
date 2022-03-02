using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullServiceProviderExtensionTests")]
    public class NullServiceProviderExtensionTests
    {
        [Test]
        public void ShouldThrowWhenCallingGetNamedServiceWithNullProvider()
        {
            IServiceProvider services = null!;
            
            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedService(typeof(ITestService), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetNamedServiceWithNullProvider()
        {
            IServiceProvider services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedService<ITestService>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowWhenCallingGetRequiredNamedServiceWithNullProvider()
        {
            IServiceProvider services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetRequiredNamedService(typeof(ITestService), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetRequiredNamedServiceWithNullProvider()
        {
            IServiceProvider services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetRequiredNamedService<ITestService>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowWhenCallingGetNamedServicesWithNullProvider()
        {
            IServiceProvider services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedServices(typeof(ITestService), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowWhenCallingGenericGetNamedServicesWithNullProvider()
        {
            IServiceProvider services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.GetNamedServices<ITestService>("test-service"));

            ex!.ParamName.Should().Be("services");
        }
    }
}
