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
// ReSharper disable ReturnValueOfPureMethodIsNotUsed - using mocks for testing

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NamedServiceFactoryTests")]
    public class NamedServiceFactoryTests
    {
        [Test]
        public void ShouldThrowIfConstructingWithNullServiceProvider()
        {
            NamedServiceFactory? sut = null;

            var ex = Assert.Throws<ArgumentNullException>(() => sut = new NamedServiceFactory(null!));

            ex!.ParamName.Should().Be("services");
            sut.Should().BeNull();
        }

        [Test]
        public void ShouldConstructNamedServiceFactory()
        {
            var sp = Substitute.For<IServiceProvider>();

            var sut = new NamedServiceFactory(sp);

            sut.Should().NotBeNull();
        }

        [Test]
        public void ShouldThrowConstructingNamedServiceFactory()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => _ = new NamedServiceFactory(null!));

            ex!.ParamName.Should().Be("services");
        }

        [Test]
        public void ShouldRetrieveNamedService()
        {
            var testService = new TestService1();
            var sp = Substitute.For<IServiceProvider, ISupportNamedServices>();
            ((ISupportNamedServices) sp).GetNamedService(typeof(ITestService), Arg.Any<string>()).Returns(testService);
            var sut = new NamedServiceFactory(sp);

            var service = sut.GetNamedService(typeof(ITestService), "name");

            service.Should().Be(testService);
            sp.Received(1).GetNamedService(typeof(ITestService), "name");
        }

        [Test]
        public void ShouldRetrieveNamedServiceForLegacyProvider()
        {
            var testService = new TestService1();
            var namedTestService = WrapTestServiceInstance(typeof(ITestService), "name", testService);
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).Returns(namedTestService);
            var sut = new NamedServiceFactory(sp);

            var service = sut.GetNamedService(typeof(ITestService), "name");

            service.Should().Be(testService);
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldReturnNullIfNoServiceFound()
        {
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).ReturnsNull();
            var sut = new NamedServiceFactory(sp);

            var service = sut.GetNamedService(typeof(ITestService), "name");

            service.Should().BeNull();
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldRetrieveRequiredNamedService()
        {
            var testService = new TestService1();
            var sp = Substitute.For<IServiceProvider, ISupportRequiredNamedServices>();
            ((ISupportRequiredNamedServices)sp).GetRequiredNamedService(typeof(ITestService), Arg.Any<string>()).Returns(testService);
            var sut = new NamedServiceFactory(sp);

            var service = sut.GetRequiredNamedService(typeof(ITestService), "name");

            service.Should().Be(testService);
            sp.Received(1).GetRequiredNamedService(typeof(ITestService), "name");
        }

        [Test]
        public void ShouldRetrieveRequiredNamedServiceForLegacyProvider()
        {
            var testService = new TestService1();
            var namedTestService = WrapTestServiceInstance(typeof(ITestService), "name", testService);
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).Returns(namedTestService);
            var sut = new NamedServiceFactory(sp);

            var service = sut.GetRequiredNamedService(typeof(ITestService), "name");

            service.Should().Be(testService);
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldThrowIfNoRequiredServiceFound()
        {
            var sp = Substitute.For<IServiceProvider>();
            sp.GetService(Arg.Any<Type>()).ReturnsNull();
            var sut = new NamedServiceFactory(sp);

            var ex = Assert.Throws<NullReferenceException>(() => sut.GetRequiredNamedService(typeof(ITestService), "name"));

            ex!.Message.Should().Be("No services registered for Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes.ITestService with a name of 'name'.");
            sp.Received(1).GetService(Arg.Is<Type>(a => a.GetGenericTypeDefinition() == typeof(INamedService<,>)));
        }

        [Test]
        public void ShouldRetrieveAllNamedServices()
        {
            var testService = new TestService1();
            var testService2 = new TestService1();
            var sp = Substitute.For<IServiceProvider, ISupportNamedServices>();
            ((ISupportNamedServices)sp).GetNamedServices(typeof(ITestService), "name")
                .Returns(new object[] { testService, testService2 });
            var sut = new NamedServiceFactory(sp);

            var services = sut.GetNamedServices(typeof(ITestService), "name");

            services.Should().HaveCount(2)
                .And.Contain(testService)
                .And.Contain(testService2);
            sp.Received(1).GetNamedServices(typeof(ITestService), "name");
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
            var sut = new NamedServiceFactory(sp);

            var services = sut.GetNamedServices(typeof(ITestService), "name");

            services.Should().HaveCount(2)
                .And.Contain(testService)
                .And.Contain(testService2);
            sp.Received(1).GetRequiredService(enumerableServiceType);
        }

        [Test]
        public void ShouldThrowForNullServiceType()
        {
            var sp = Substitute.For<IServiceProvider, ISupportRequiredNamedServices, ISupportNamedServices>();
            var sut = new NamedServiceFactory(sp);

            var ex1 = Assert.Throws<ArgumentNullException>(() => sut.GetNamedService(null!, "name"));
            var ex2 = Assert.Throws<ArgumentNullException>(() => sut.GetNamedServices(null!, "name"));
            var ex3 = Assert.Throws<ArgumentNullException>(() => sut.GetRequiredNamedService(null!, "name"));

            ex1!.ParamName.Should().Be("serviceType");
            ex2!.ParamName.Should().Be("serviceType");
            ex3!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceName()
        {
            var sp = Substitute.For<IServiceProvider, ISupportRequiredNamedServices, ISupportNamedServices>();
            var sut = new NamedServiceFactory(sp);

            var ex1 = Assert.Throws<ArgumentNullException>(() => sut.GetNamedService(typeof(ITestService), null!));
            var ex2 = Assert.Throws<ArgumentNullException>(() => sut.GetNamedServices(typeof(ITestService), null!));
            var ex3 = Assert.Throws<ArgumentNullException>(() => sut.GetRequiredNamedService(typeof(ITestService), null!));

            ex1!.ParamName.Should().Be("serviceName");
            ex2!.ParamName.Should().Be("serviceName");
            ex3!.ParamName.Should().Be("serviceName");
        }

        private static INamedService WrapTestServiceInstance(Type serviceType, string name, object serviceInstance)
            => NamedServiceManager.CreateNamedServiceImplementation(serviceType, serviceInstance, name);

        private static Type GetEnumerableNamedServiceType(Type serviceType, string name)
            => NamedServiceManager.CreateNamedServiceEnumerableInterface(serviceType, name);

        private static IEnumerable<INamedService> WrapTestServiceInstancesAsEnumerable(Type serviceType, string name, params object[] serviceInstance)
            => serviceInstance.Select(s => WrapTestServiceInstance(serviceType,name, s));
    }
}
