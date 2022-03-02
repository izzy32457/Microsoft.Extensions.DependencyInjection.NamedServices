using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Inter.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Inter
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "MicrosoftImplementationServiceCollectionTests")]
    public class MicrosoftImplementationServiceCollectionTests
    {
        #region Service Collection Transient Extensions

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(ITestService), "test-service", typeof(TestService1));

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(ITestService), "test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithGenericServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddTransient<ITestService, TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientNamedServiceToWithServiceTypeAndName()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(TestService1), "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithGenericServiceType()
        {
            var services = new ServiceCollection();

            services.AddTransient<TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddTransient<ITestService>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddTransient<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Transient);
        }

        #endregion

        #region Service Collection Scoped Extensions

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddScoped(typeof(ITestService), "test-service", typeof(TestService1));

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddScoped(typeof(ITestService), "test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithGenericServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITestService, TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithServiceType()
        {
            var services = new ServiceCollection();

            services.AddScoped(typeof(TestService1), "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithGenericServiceType()
        {
            var services = new ServiceCollection();

            services.AddScoped<TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITestService>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddScoped<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Scoped);
        }

        #endregion

        #region Service Collection Singleton Extensions

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1));

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddSingleton(typeof(ITestService), "test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithGenericServiceAndImplementationType()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITestService, TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithServiceType()
        {
            var services = new ServiceCollection();

            services.AddSingleton(typeof(TestService1), "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithGenericServiceType()
        {
            var services = new ServiceCollection();

            services.AddSingleton<TestService1>("test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITestService>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ITestService, TestService1>("test-service", _ => new TestService1());

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = new ServiceCollection();
            var instance = new TestService1();

            services.AddSingleton(typeof(ITestService), instance, "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var services = new ServiceCollection();
            var instance = new TestService1();

            services.AddSingleton<ITestService>(instance, "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(ITestService)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        [Test]
        public void ShouldRegisterServiceWhenCallingAddSingletonWithImplementationInstance()
        {
            var services = new ServiceCollection();
            var instance = new TestService1();

            services.AddSingleton(instance, "test-service");

            services.Count.Should().Be(2);
            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
            services.Should().ContainSingle(sd =>
                sd.ServiceType.IsGenericType
                && sd.ServiceType.GetGenericTypeDefinition() == typeof(INamedService<,>)
                && sd.ServiceType.GenericTypeArguments[0] == typeof(TestService1)
                && sd.ServiceType.GenericTypeArguments[1] == NamedServiceManager.CreateNamedServiceKeyType("test-service")
                && sd.Lifetime == ServiceLifetime.Singleton);
        }

        #endregion

        #region Collection Service Factory Registration

        [Test]
        public void ShouldAddSingleServiceFactory()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(ITestService), "test-service", typeof(TestService1));
            services.AddTransient(typeof(ITestService), "test-service2", typeof(TestService1));

            services.Should().ContainSingle(sd => sd.ServiceType == typeof(NamedServiceFactory));
        }

        #endregion
    }
}
