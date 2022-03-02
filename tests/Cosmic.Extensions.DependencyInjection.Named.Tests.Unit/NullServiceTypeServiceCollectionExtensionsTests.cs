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
    [TestFixture(Category = "NullServiceTypeServiceCollectionExtensionsTests")]
    public class NullServiceTypeServiceCollectionExtensionsTests
    {
        #region Argument Exception For Service Collection Transient Extensions

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddTransientWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(null!, "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(null!, "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddTransientWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        #endregion

        #region Argument Exception For Service Collection Scoped Extensions

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddScopedWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(null!, "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(null!, "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddScopedWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        #endregion

        #region Argument Exception For Service Collection Singleton Extensions

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(null!, "test-service", typeof(TestService1)));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(null!, "test-service", _ => new TestService1()));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddSingletonWithServiceType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(null!, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeWhenCallingAddSingletonWithServiceTypeAndImplementationInstance()
        {
            var services = Substitute.For<IServiceCollection>();
            var instance = new TestService1();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(null!, instance, "test-service"));

            ex!.ParamName.Should().Be("serviceType");
        }

        #endregion
    }
}
