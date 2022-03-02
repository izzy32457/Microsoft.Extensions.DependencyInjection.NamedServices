using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullServiceCollectionExtensionsTests")]
    public class NullServiceCollectionExtensionsTests
    {
        #region Argument Exception For Service Collection Transient Extensions

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithGenericServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService, TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(TestService1), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithGenericServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService, TestService1>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        #endregion

        #region Argument Exception For Service Collection Scoped Extensions

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithGenericServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService, TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(TestService1), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithGenericServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService, TestService1>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        #endregion

        #region Argument Exception For Service Collection Singleton Extensions

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithGenericServiceAndImplementationType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService, TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(TestService1), "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithGenericServiceType()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<TestService1>("test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            IServiceCollection services = null!;

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService, TestService1>("test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            IServiceCollection services = null!;
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), instance, "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            IServiceCollection services = null!;
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>(instance, "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldThrowForNullServiceCollectionWhenCallingAddSingletonWithImplementationInstance()
        {
            IServiceCollection services = null!;
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(instance, "test-service"));

            ex!.ParamName.Should().Be("services");
        }

        #endregion
    }
}
