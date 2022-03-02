using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
// ReSharper disable InvokeAsExtensionMethod - explicitly testing extension methods

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NamedServiceFactoryExtensionsTests
    {
        [Test]
        public void ShouldGetNamedServiceFromFactory()
        {
            var testService = new TestService1();
            var namedTestService = WrapTestServiceInstance(typeof(ITestService), "name", testService);
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).Returns(namedTestService);
            var factory = new NamedServiceFactory(sp);

            var service = NamedServiceFactoryExtensions.GetNamedService<ITestService>(factory, "name");

            service.Should().Be(testService);
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldReturnNullIfNoServiceFound()
        {
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).ReturnsNull();
            var factory = new NamedServiceFactory(sp);

            var service = NamedServiceFactoryExtensions.GetNamedService<ITestService>(factory, "name");

            service.Should().BeNull();
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldRetrieveRequiredNamedServiceForLegacyProvider()
        {
            var testService = new TestService1();
            var namedTestService = WrapTestServiceInstance(typeof(ITestService), "name", testService);
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).Returns(namedTestService);
            var factory = new NamedServiceFactory(sp);

            var service = NamedServiceFactoryExtensions.GetNamedService<ITestService>(factory, "name");

            service.Should().Be(testService);
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldThrowIfNoRequiredServiceFound()
        {
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).ReturnsNull();
            var factory = new NamedServiceFactory(sp);

            var ex = Assert.Throws<NullReferenceException>(() => NamedServiceFactoryExtensions.GetRequiredNamedService<ITestService>(factory, "name"));

            ex!.Message.Should().Be("No services registered for Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService with a name of 'name'.");
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldRetrieveAllNamedServicesForLegacyProvider()
        {
            var testService = new TestService1();
            var testService2 = new TestService1();
            var sp = Substitute.For<IServiceProvider, ISupportRequiredService>();
            var enumerableServiceType = GetEnumerableNamedServiceType(typeof(ITestService), "name");
            ((ISupportRequiredService)sp).GetRequiredService(Arg.Is(enumerableServiceType))
                .Returns(WrapTestServiceInstancesAsEnumerable(typeof(ITestService), "name", testService, testService2));
            var factory = new NamedServiceFactory(sp);

            var services = NamedServiceFactoryExtensions.GetNamedServices<ITestService>(factory, "name");

            services.Should().HaveCount(2)
                .And.Contain(testService)
                .And.Contain(testService2);
            sp.Received(1).GetRequiredService(enumerableServiceType);
        }

        [Test]
        public void ShouldThrowForNullServiceName()
        {
            var sp = Substitute.For<IServiceProvider>();
            var factory = new NamedServiceFactory(sp);

            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetNamedService<ITestService>(factory, null!));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetNamedServices<ITestService>(factory, null!));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetRequiredNamedService<ITestService>(factory, null!));

            ex1!.ParamName.Should().Be("serviceName");
            ex2!.ParamName.Should().Be("serviceName");
            ex3!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullNamedServiceFactory()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetNamedService<ITestService>(null!, "name"));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetNamedServices<ITestService>(null!, "name"));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceFactoryExtensions.GetRequiredNamedService<ITestService>(null!, "name"));

            ex1!.ParamName.Should().Be("factory");
            ex2!.ParamName.Should().Be("factory");
            ex3!.ParamName.Should().Be("factory");
        }

        private static INamedService WrapTestServiceInstance(Type serviceType, string name, object serviceInstance)
            => NamedServiceManager.CreateNamedServiceImplementation(serviceType, serviceInstance, name);

        private static Type GetEnumerableNamedServiceType(Type serviceType, string name)
            => NamedServiceManager.CreateNamedServiceEnumerableInterface(serviceType, name);

        private static IEnumerable<INamedService> WrapTestServiceInstancesAsEnumerable(Type serviceType, string name, params object[] serviceInstance)
            => serviceInstance.Select(s => WrapTestServiceInstance(serviceType, name, s));
    }
}
