using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "ServiceCollectionExtensionsTests")]
    public class ServiceCollectionExtensionsTests
    {
        #region Service Collection Transient Extensions

        [Test]
        public void ShouldAddTransientWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient(typeof(ITestService), "test-service", typeof(TestService1));

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient(typeof(ITestService), "test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient<ITestService, TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientNamedServiceToWithServiceTypeAndName()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient(typeof(TestService1), "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient<TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient<ITestService>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        [Test]
        public void ShouldAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddTransient<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient));
        }

        #endregion

        #region Service Collection Scoped Extensions

        [Test]
        public void ShouldAddScopedWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped(typeof(ITestService), "test-service", typeof(TestService1));

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped(typeof(ITestService), "test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped<ITestService, TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped(typeof(TestService1), "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped<TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped<ITestService>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        [Test]
        public void ShouldAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddScoped<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped));
        }

        #endregion

        #region Service Collection Singleton Extensions

        [Test]
        public void ShouldAddSingletonWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1));

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton(typeof(ITestService), "test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton<ITestService, TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton(typeof(TestService1), "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton<TestService1>("test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton<ITestService>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            services.AddSingleton<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            services.AddSingleton(typeof(ITestService), instance, "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            services.AddSingleton<ITestService>(instance, "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        [Test]
        public void ShouldAddSingletonWithImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            services.AddSingleton(instance, "test-service");

            services.Received(1).Add(Arg.Is<ServiceDescriptor>(sd =>
                sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton));
        }

        #endregion
    }
}