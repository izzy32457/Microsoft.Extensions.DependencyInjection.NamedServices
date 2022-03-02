using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "SupportNamedServiceExtensionsTests")]
    public class SupportNamedServiceExtensionsTests
    {
        #region ISupportNamedServices

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGetNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();

            _ = services.GetNamedService(typeof(ITestService), "test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedService(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGenericGetNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();

            _ = services.GetNamedService<ITestService>("test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedService(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();
            ((ISupportNamedServices)services).GetNamedService(typeof(ITestService), Arg.Any<string>())
                .Returns(new TestService1());

            _ = services.GetRequiredNamedService(typeof(ITestService), "test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedService(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGenericGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();
            ((ISupportNamedServices) services).GetNamedService(typeof(ITestService), Arg.Any<string>())
                .Returns(new TestService1());

            _ = services.GetRequiredNamedService<ITestService>("test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedService(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldThrowFromNativeRetrievalIfAvailableForGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();

            var ex = Assert.Throws<InvalidOperationException>(() => _ = services.GetRequiredNamedService(typeof(ITestService), "test-service"));

            ex!.Message.Should().Be("No service of type 'Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService' with the name 'test-service' was found.");
        }

        [Test]
        public void ShouldThrowFromUseNativeRetrievalIfAvailableForGenericGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();
            
            var ex = Assert.Throws<InvalidOperationException>(() => _ = services.GetRequiredNamedService<ITestService>("test-service"));

            ex!.Message.Should().Be("No service of type 'Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService' with the name 'test-service' was found.");
        }

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGetNamedServices()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();

            _ = services.GetNamedServices(typeof(ITestService), "test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedServices(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldUseNativeRetrievalIfAvailableForGenericGetNamedServices()
        {
            var services = Substitute.For<IServiceProvider, ISupportNamedServices>();

            _ = services.GetNamedServices<ITestService>("test-service");

            ((ISupportNamedServices)services).Received(1).GetNamedServices(typeof(ITestService), "test-service");
        }

        #endregion

        #region ISupportRequiredNamedServices


        [Test]
        public void ShouldUseNativeRequiresRetrievalIfAvailableForGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportRequiredNamedServices>();

            _ = services.GetRequiredNamedService(typeof(ITestService), "test-service");

            ((ISupportRequiredNamedServices)services).Received(1).GetRequiredNamedService(typeof(ITestService), "test-service");
        }

        [Test]
        public void ShouldUseNativeRequiresRetrievalIfAvailableForGenericGetRequiredNamedService()
        {
            var services = Substitute.For<IServiceProvider, ISupportRequiredNamedServices>();

            _ = services.GetRequiredNamedService<ITestService>("test-service");

            ((ISupportRequiredNamedServices)services).Received(1).GetRequiredNamedService(typeof(ITestService), "test-service");
        }

        #endregion
    }
}
