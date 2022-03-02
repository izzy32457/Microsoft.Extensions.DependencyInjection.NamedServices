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
    [TestFixture(Category = "NullImplementationTypeServiceCollectionExtensionsTests")]
    public class NullImplementationTypeServiceCollectionExtensionsTests
    {
        [Test]
        public void ShouldThrowForNullImplementationTypeWhenCallingAddTransientWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddTransient(typeof(ITestService), "test-service", (Type)null!));

            ex!.ParamName.Should().Be("implementationType");
        }

        [Test]
        public void ShouldThrowForNullImplementationTypeWhenCallingAddScopedWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddScoped(typeof(ITestService), "test-service", (Type)null!));

            ex!.ParamName.Should().Be("implementationType");
        }

        [Test]
        public void ShouldThrowForNullImplementationTypeWhenCallingAddSingletonWithServiceAndImplementationType()
        {
            var services = Substitute.For<IServiceCollection>();

            var ex = Assert.Throws<ArgumentNullException>(() => services.AddSingleton(typeof(ITestService), "test-service", (Type)null!));

            ex!.ParamName.Should().Be("implementationType");
        }
    }
}
