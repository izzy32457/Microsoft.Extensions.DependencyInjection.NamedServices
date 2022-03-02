using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullServiceNameServiceCollectionExtensionsTests")]
    public class NullServiceNameServiceCollectionExtensionsTests
    {
        #region Argument Exception For Service Collection Transient Extensions

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), null!, typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService, TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(TestService1), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService, TestService1>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        #endregion

        #region Argument Exception For Service Collection Scoped Extensions

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), null!, typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService, TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(TestService1), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService, TestService1>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        #endregion

        #region Argument Exception For Service Collection Singleton Extensions

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), null!, typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithGenericServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService, TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(TestService1), null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithGenericServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<TestService1>(null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService, TestService1>(null!, _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), instance, null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithGenericServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>(instance, null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameWhenCallingAddSingletonWithImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(instance, null!));

            ex!.ParamName.Should().Be("serviceName");
        }

        #endregion
    }
}
