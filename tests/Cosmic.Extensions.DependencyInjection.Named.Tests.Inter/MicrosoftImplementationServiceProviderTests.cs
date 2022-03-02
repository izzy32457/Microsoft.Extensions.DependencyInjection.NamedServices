using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Inter.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Inter
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "MicrosoftImplementationServiceProviderTests")]
    public class MicrosoftImplementationServiceProviderTests
    {
        #region Service Collection Transient Extensions

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddTransient(typeof(ITestService), "test-service", typeof(TestService1));
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>();
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddTransient(typeof(ITestService), "test-service", _ => actualInstance);
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(actualInstance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithGenericServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddTransient<ITestService, TestService1>("test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>();
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientNamedServiceToWithServiceTypeAndName()
        {
            var builder = new ServiceCollection();
            builder.AddTransient(typeof(TestService1), "test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(TestService1));
            var nullInstance2 = services.GetNamedService(typeof(ITestService), "test-service");
            var instance = services.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>();
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithGenericServiceType()
        {
            var builder = new ServiceCollection();
            builder.AddTransient<TestService1>("test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(TestService1));
            var nullInstance2 = services.GetNamedService(typeof(ITestService), "test-service");
            var instance = services.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>();
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddTransient<ITestService>("test-service", _ => actualInstance);
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(actualInstance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddTransient<ITestService, TestService1>("test-service", _ => actualInstance);
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(actualInstance);
        }

        #endregion

        #region Service Collection Scoped Extensions

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddScoped(typeof(ITestService), "test-service", typeof(TestService1));
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(ITestService));
            var instance = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddScoped(typeof(ITestService), "test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(ITestService));
            var instance = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithGenericServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddScoped<ITestService, TestService1>("test-service");
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(ITestService));
            var instance = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedNamedServiceToWithServiceTypeAndName()
        {
            var builder = new ServiceCollection();
            builder.AddScoped(typeof(TestService1), "test-service");
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(TestService1));
            var nullInstance2 = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance = scope.ServiceProvider.GetNamedService(typeof(TestService1), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithGenericServiceType()
        {
            var builder = new ServiceCollection();
            builder.AddScoped<TestService1>("test-service");
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(TestService1));
            var nullInstance2 = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance = scope.ServiceProvider.GetNamedService(typeof(TestService1), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddScoped<ITestService>("test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(ITestService));
            var instance = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddScoped<ITestService, TestService1>("test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();
            using var scope = services.CreateScope();
            using var scope2 = services.CreateScope();

            var nullInstance = scope.ServiceProvider.GetService(typeof(ITestService));
            var instance = scope.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = scope2.ServiceProvider.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance2);
            instance2.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.NotBe(instance);
        }

        #endregion

        #region Service Collection Singleton Extensions

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1));
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton(typeof(ITestService), "test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithGenericServiceAndImplementationType()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton<ITestService, TestService1>("test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonNamedServiceToWithServiceTypeAndName()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton(typeof(TestService1), "test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(TestService1));
            var nullInstance2 = services.GetNamedService(typeof(ITestService), "test-service");
            var instance = services.GetNamedService(typeof(TestService1), "test-service");
            var instance2 = services.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithGenericServiceType()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton<TestService1>("test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(TestService1));
            var nullInstance2 = services.GetNamedService(typeof(ITestService), "test-service");
            var instance = services.GetNamedService(typeof(TestService1), "test-service");
            var instance2 = services.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton<ITestService>("test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton<ITestService, TestService1>("test-service", _ => new TestService1());
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddSingleton(typeof(ITestService), actualInstance, "test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2)
                .And.Be(actualInstance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddSingleton<ITestService>(actualInstance, "test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(ITestService), "test-service");
            var instance2 = services.GetNamedService(typeof(ITestService), "test-service");

            nullInstance.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2)
                .And.Be(actualInstance);
        }

        [Test]
        public void ShouldReturnServiceWhenCallingAddSingletonWithImplementationInstance()
        {
            var actualInstance = new TestService1();
            var builder = new ServiceCollection();
            builder.AddSingleton(actualInstance, "test-service");
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetService(typeof(TestService1));
            var nullInstance2 = services.GetService(typeof(ITestService));
            var instance = services.GetNamedService(typeof(TestService1), "test-service");
            var instance2 = services.GetNamedService(typeof(TestService1), "test-service");

            nullInstance.Should().BeNull();
            nullInstance2.Should().BeNull();
            instance.Should().NotBeNull()
                .And.BeOfType<TestService1>()
                .And.Be(instance2)
                .And.Be(actualInstance);
        }

        #endregion

        #region Service Collection Enumerable Extensions

        [Test]
        public void ShouldReturnMultipleServicesWhenMoreThanOneIsRegistered()
        {
            var builder = new ServiceCollection();
            builder.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1));
            builder.AddSingleton(typeof(ITestService), "test-service", typeof(TestService2));
            var services = builder.BuildServiceProvider();

            var nullInstance = services.GetServices(typeof(ITestService));
            var instances = services.GetNamedServices(typeof(ITestService), "test-service");

            nullInstance.Should().NotBeNull()
                .And.HaveCount(0);
            instances.Should().NotBeNull()
                .And.HaveCount(2)
                .And.ContainSingle(s => s != null && s.GetType() == typeof(TestService1))
                .And.ContainSingle(s => s != null && s.GetType() == typeof(TestService2));
        }

        #endregion
    }
}
