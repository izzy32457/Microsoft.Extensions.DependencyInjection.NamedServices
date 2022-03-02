using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "ServiceProviderExtensionsTests")]
    public class ServiceProviderExtensionsTests
    {
        [Test]
        public void ShouldWrapServiceTypeForBasicProvider()
        {
            var services = Substitute.For<IServiceProvider>();

            _ = services.GetNamedService(typeof(ITestService), "test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }

        [Test]
        public void ShouldWrapServiceTypeForBasicProviderWithGenericType()
        {
            var services = Substitute.For<IServiceProvider>();

            _ = services.GetNamedService<ITestService>("test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }

        [Test]
        public void ShouldWrapRequiredServiceTypeForBasicProvider()
        {
            var services = Substitute.For<IServiceProvider>();
            services.GetService(Arg.Any<Type>())
                .Returns(NamedServiceManager.CreateNamedServiceImplementation(typeof(ITestService), new TestService1(), "test-service"));

            _ = services.GetRequiredNamedService(typeof(ITestService), "test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }

        [Test]
        public void ShouldWrapRequiredServiceTypeForBasicProviderWithGenericType()
        {
            var services = Substitute.For<IServiceProvider>();
            services.GetService(Arg.Any<Type>())
                .Returns(NamedServiceManager.CreateNamedServiceImplementation(typeof(ITestService), new TestService1(), "test-service"));

            _ = services.GetRequiredNamedService<ITestService>("test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }

        [Test]
        public void ShouldThrowForBasicProviderWhenNoServiceIsRegistered()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<InvalidOperationException>(() => _ = services.GetRequiredNamedService(typeof(ITestService), "test-service"));

            ex!.Message.Should().Be("No service for type 'Cosmic.Extensions.DependencyInjection.INamedService`2[Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService,test-service]' has been registered.");
        }

        [Test]
        public void ShouldThrowForBasicProviderWhenNoServiceIsRegisteredWithGenericType()
        {
            var services = Substitute.For<IServiceProvider>();

            var ex = Assert.Throws<InvalidOperationException>(() => _ = services.GetRequiredNamedService<ITestService>("test-service"));

            ex!.Message.Should().Be("No service for type 'Cosmic.Extensions.DependencyInjection.INamedService`2[Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService,test-service]' has been registered.");
        }

        [Test]
        public void ShouldWrapEnumerableServiceTypeForBasicProvider()
        {
            var services = Substitute.For<IServiceProvider>();
            services.GetService(Arg.Is<Type>(t => t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                .Returns(new List<object?>());

            _ = services.GetNamedServices(typeof(ITestService), "test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                && st.GenericTypeArguments[0].GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0].GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[0].GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }

        [Test]
        public void ShouldWrapEnumerableServiceTypeForBasicProviderWithGenericType()
        {
            var services = Substitute.For<IServiceProvider>();
            services.GetService(Arg.Is<Type>(t => t.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
                .Returns(new List<object?>());

            _ = services.GetNamedServices<ITestService>("test-service");

            services.Received(1).GetService(Arg.Is<Type>(st =>
                st.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                && st.GenericTypeArguments[0].GetGenericTypeDefinition() == typeof(INamedService<,>)
                && st.GenericTypeArguments[0].GenericTypeArguments[0] == typeof(ITestService)
                && st.GenericTypeArguments[0].GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")));
        }
    }
}
