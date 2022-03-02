using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "SupportNamedServiceExtensionsTests")]
    public class SupportNamedServiceDescriptorExtensionsTests
    {
        #region Service Collection Transient Extensions

        [Test]
        public void ShouldAddTransientWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient(typeof(ITestService), "test-service", typeof(TestService1));

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient(typeof(ITestService), "test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient<ITestService, TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientNamedServiceToWithServiceTypeAndName()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient(typeof(TestService1), "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient<TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient<ITestService>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddTransient<ITestService, TestService1>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Transient));
        }

        #endregion

        #region Service Collection Scoped Extensions

        [Test]
        public void ShouldAddScopedWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped(typeof(ITestService), "test-service", typeof(TestService1));

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped(typeof(ITestService), "test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped<ITestService, TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithServiceType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped(typeof(TestService1), "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped<TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped<ITestService>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddScoped<ITestService, TestService1>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Scoped));
        }

        #endregion

        #region Service Collection Singleton Extensions

        [Test]
        public void ShouldAddSingletonWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1));

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton(typeof(ITestService), "test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton<ITestService, TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton(typeof(TestService1), "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton<TestService1>("test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton<ITestService>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();

            services.AddSingleton<ITestService, TestService1>("test-service", _ => new TestService1());

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();
            var instance = new TestService1();

            services.AddSingleton(typeof(ITestService), instance, "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();
            var instance = new TestService1();

            services.AddSingleton<ITestService>(instance, "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(ITestService)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection, ISupportNamedServiceDescriptor>();
            var instance = new TestService1();

            services.AddSingleton(instance, "test-service");

            ((ISupportNamedServiceDescriptor)services).Received(1).Add(Arg.Is<NamedServiceDescriptor>(nsd =>
                nsd.ServiceType == typeof(TestService1)
                && nsd.ServiceName == "test-service"
                && nsd.Lifetime == ServiceLifetime.Singleton));
        }

        #endregion
    }
}
