using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
// ReSharper disable RedundantCast

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NullImplementationFactoryServiceCollectionExtensionsTests")]
    public class NullImplementationFactoryServiceCollectionExtensionsTests
    {
        #region Argument Exception For Service Collection Transient Extensions
        
        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddTransientWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), "test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddTransientWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddTransientWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient<ITestService, TestService1>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        #endregion

        #region Argument Exception For Service Collection Scoped Extensions
        
        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddScopedWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), "test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddScopedWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddScopedWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped<ITestService, TestService1>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        #endregion

        #region Argument Exception For Service Collection Singleton Extensions
        
        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddSingletonWithServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), "test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddSingletonWithGenericServiceTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryWhenCallingAddSingletonWithGenericServiceAndImplementationTypeAndImplementationFactory()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton<ITestService, TestService1>("test-service", (Func<IServiceProvider, TestService1>)null!));

            ex!.ParamName.Should().Be("implementationFactory");
        }

        #endregion
    }
}
