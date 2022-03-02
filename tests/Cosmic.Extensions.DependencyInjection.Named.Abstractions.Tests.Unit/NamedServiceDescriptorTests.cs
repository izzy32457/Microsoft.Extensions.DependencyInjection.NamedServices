using System;
using System.Diagnostics.CodeAnalysis;
using Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit
{
    [ExcludeFromCodeCoverage]
    [TestFixture(Category = "NamedServiceDescriptorTests")]
    public class NamedServiceDescriptorTests
    {
        [TestCase(ServiceLifetime.Transient)]
        [TestCase(ServiceLifetime.Scoped)]
        [TestCase(ServiceLifetime.Singleton)]
        public void ShouldThrowForNullServiceNameDuringConstruction(ServiceLifetime lifetime)
        {
            object testInstance = new TestService1();

            var ctor1 = Assert.Throws<ArgumentNullException>(
                () => _ = new NamedServiceDescriptor(typeof(ITestService), null!, typeof(TestService1), lifetime));
            var ctor2 = Assert.Throws<ArgumentNullException>(
                () => _ = new NamedServiceDescriptor(typeof(ITestService), null!, testInstance));
            var ctor3 = Assert.Throws<ArgumentNullException>(
                () => _ = new NamedServiceDescriptor(typeof(ITestService), null!, _ => testInstance, lifetime));

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            ctor1.Should().NotBeNull();
            ctor1.ParamName.Should().Be("serviceName");
            ctor2.Should().NotBeNull();
            ctor2.ParamName.Should().Be("serviceName");
            ctor3.Should().NotBeNull();
            ctor3.ParamName.Should().Be("serviceName");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [Test]
        public void ShouldCreateCorrectDescriptor()
        {
            var testInstance = new TestService1();

            var inst1 = new NamedServiceDescriptor(typeof(ITestService), "MyService1", typeof(TestService1), ServiceLifetime.Transient);
            var inst2 = new NamedServiceDescriptor(typeof(ITestService), "MyService2", testInstance);
            var inst3 = new NamedServiceDescriptor(typeof(ITestService), "MyService3", _ => testInstance, ServiceLifetime.Scoped);
            
            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("MyService1");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().Be(typeof(TestService1));
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().BeNull();
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("MyService2");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().Be(testInstance);
            inst2.ImplementationFactory.Should().BeNull();
            inst2.GetImplementationType().Should().Be(typeof(TestService1));
            inst3.Should().NotBeNull();
            inst3.ServiceName.Should().Be("MyService3");
            inst3.ServiceType.Should().Be(typeof(ITestService));
            inst3.ImplementationType.Should().BeNull();
            inst3.ImplementationInstance.Should().BeNull();
            inst3.ImplementationFactory.Should().NotBeNull();
            inst3.GetImplementationType().Should().Be(typeof(object));
        }

        [Test]
        public void ShouldCreateCorrectDescriptorUsingStaticHelpers()
        {
            var testInstance = new TestService1();

            var inst1 = NamedServiceDescriptor.Transient<ITestService, TestService1>("name", _ => testInstance);
            var inst2 = NamedServiceDescriptor.Scoped<ITestService, TestService1>("name", _ => testInstance);

            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("name");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().BeNull();
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().NotBeNull();
            inst1.Lifetime.Should().Be(ServiceLifetime.Transient);
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("name");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().BeNull();
            inst2.ImplementationFactory.Should().NotBeNull();
            inst2.Lifetime.Should().Be(ServiceLifetime.Scoped);
            inst2.GetImplementationType().Should().Be(typeof(TestService1));
        }

        [Test]
        public void ShouldCreateCorrectToStringResult()
        {
            var testInstance = new TestService1();

            var inst1 = new NamedServiceDescriptor(typeof(ITestService), "MyService1", typeof(TestService1), ServiceLifetime.Transient);
            var inst2 = new NamedServiceDescriptor(typeof(ITestService), "MyService2", testInstance);
            var inst3 = new NamedServiceDescriptor(typeof(ITestService), "MyService3", _ => testInstance, ServiceLifetime.Scoped);

            inst1.ToString().Should().Be("ServiceType: Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes.ITestService Lifetime: Transient ImplementationType: Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes.TestService1 ServiceName: MyService1");
            inst2.ToString().Should().Be("ServiceType: Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes.ITestService Lifetime: Singleton ImplementationInstance: Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes.TestService1 ServiceName: MyService2");
            inst3.ToString().Should().Be("ServiceType: Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes.ITestService Lifetime: Scoped ImplementationFactory: System.Object <ShouldCreateCorrectToStringResult>b__0(System.IServiceProvider) ServiceName: MyService3");
        }

        [Test]
        public void ShouldReturnCorrectImplementationType()
        {
            var sd1 = NamedServiceDescriptor.Singleton(typeof(ITestService), "sd1", typeof(TestService1));
            var sd2 = NamedServiceDescriptor.Singleton(typeof(ITestService), "sd2", _ => new TestService1());
            var localService = new TestService1();
            var sd3 = NamedServiceDescriptor.Singleton(typeof(ITestService), "sd3", localService);
            var sd4 = NamedServiceDescriptor.Singleton<ITestService, TestService1>("sd4");
            var sd5 = NamedServiceDescriptor.Singleton<ITestService, TestService1>("sd5", _ => new TestService1());

            var result1 = sd1.GetImplementationType();
            var result2 = sd2.GetImplementationType();
            var result3 = sd3.GetImplementationType();
            var result4 = sd4.GetImplementationType();
            var result5 = sd5.GetImplementationType();

            result1.Should().Be(typeof(TestService1));
            result2.Should().Be(typeof(object));
            result3.Should().Be(typeof(TestService1));
            result4.Should().Be(typeof(TestService1));
            result5.Should().Be(typeof(TestService1));
        }

        [Test, Ignore("Unable to properly mock ServiceDescriptor base class to inject the relevant properties.")]
        public void ShouldReturnCorrectImplementationTypeForInvalidDescriptor()
        {
            var mockSdNull = NamedServiceDescriptorTestWrapper.CreateMockedNamedServiceDescriptor("null");
            
            var result = mockSdNull.GetImplementationType();

            result.Should().BeNull();
        }

        [Test]
        public void ShouldCreateCorrectTransientDescriptor()
        {
            var testInstance = new TestService1();

            var inst1 = NamedServiceDescriptor.Transient(typeof(ITestService), "MyService1", typeof(TestService1));
            var inst2 = NamedServiceDescriptor.Transient(typeof(ITestService), "MyService2", _ => testInstance);
            var inst3 = NamedServiceDescriptor.Transient<ITestService, TestService1>("MyService3");
            var inst4 = NamedServiceDescriptor.Transient<ITestService>("MyService4", _ => testInstance);

            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("MyService1");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().Be(typeof(TestService1));
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().BeNull();
            inst1.Lifetime.Should().Be(ServiceLifetime.Transient);
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("MyService2");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().BeNull();
            inst2.ImplementationFactory.Should().NotBeNull();
            inst2.Lifetime.Should().Be(ServiceLifetime.Transient);
            inst2.GetImplementationType().Should().Be(typeof(object));
            inst3.Should().NotBeNull();
            inst3.ServiceName.Should().Be("MyService3");
            inst3.ServiceType.Should().Be(typeof(ITestService));
            inst3.ImplementationType.Should().Be(typeof(TestService1));
            inst3.ImplementationInstance.Should().BeNull();
            inst3.ImplementationFactory.Should().BeNull();
            inst3.Lifetime.Should().Be(ServiceLifetime.Transient);
            inst3.GetImplementationType().Should().Be(typeof(TestService1));
            inst4.Should().NotBeNull();
            inst4.ServiceName.Should().Be("MyService4");
            inst4.ServiceType.Should().Be(typeof(ITestService));
            inst4.ImplementationType.Should().BeNull();
            inst4.ImplementationInstance.Should().BeNull();
            inst4.ImplementationFactory.Should().NotBeNull();
            inst4.Lifetime.Should().Be(ServiceLifetime.Transient);
            inst4.GetImplementationType().Should().Be(typeof(ITestService));
        }

        [Test]
        public void ShouldCreateCorrectScopedDescriptor()
        {
            var testInstance = new TestService1();

            var inst1 = NamedServiceDescriptor.Scoped(typeof(ITestService), "MyService1", typeof(TestService1));
            var inst2 = NamedServiceDescriptor.Scoped(typeof(ITestService), "MyService2", _ => testInstance);
            var inst3 = NamedServiceDescriptor.Scoped<ITestService, TestService1>("MyService3");
            var inst4 = NamedServiceDescriptor.Scoped<ITestService>("MyService4", _ => testInstance);

            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("MyService1");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().Be(typeof(TestService1));
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().BeNull();
            inst1.Lifetime.Should().Be(ServiceLifetime.Scoped);
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("MyService2");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().BeNull();
            inst2.ImplementationFactory.Should().NotBeNull();
            inst2.Lifetime.Should().Be(ServiceLifetime.Scoped);
            inst2.GetImplementationType().Should().Be(typeof(object));
            inst3.Should().NotBeNull();
            inst3.ServiceName.Should().Be("MyService3");
            inst3.ServiceType.Should().Be(typeof(ITestService));
            inst3.ImplementationType.Should().Be(typeof(TestService1));
            inst3.ImplementationInstance.Should().BeNull();
            inst3.ImplementationFactory.Should().BeNull();
            inst3.Lifetime.Should().Be(ServiceLifetime.Scoped);
            inst3.GetImplementationType().Should().Be(typeof(TestService1));
            inst4.Should().NotBeNull();
            inst4.ServiceName.Should().Be("MyService4");
            inst4.ServiceType.Should().Be(typeof(ITestService));
            inst4.ImplementationType.Should().BeNull();
            inst4.ImplementationInstance.Should().BeNull();
            inst4.ImplementationFactory.Should().NotBeNull();
            inst4.Lifetime.Should().Be(ServiceLifetime.Scoped);
            inst4.GetImplementationType().Should().Be(typeof(ITestService));
        }

        [Test]
        public void ShouldCreateCorrectSingletonDescriptor()
        {
            var testInstance = new TestService1();

            var inst1 = NamedServiceDescriptor.Singleton(typeof(ITestService), "MyService1", typeof(TestService1));
            var inst2 = NamedServiceDescriptor.Singleton(typeof(ITestService), "MyService2", testInstance);
            var inst3 = NamedServiceDescriptor.Singleton(typeof(ITestService), "MyService3", _ => testInstance);
            var inst4 = NamedServiceDescriptor.Singleton<ITestService, TestService1>("MyService4");
            var inst5 = NamedServiceDescriptor.Singleton<ITestService>("MyService5", testInstance);
            var inst6 = NamedServiceDescriptor.Singleton<ITestService, TestService1>("MyService6", _ => testInstance);
            var inst7 = NamedServiceDescriptor.Singleton<ITestService>("MyService7", _ => testInstance);

            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("MyService1");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().Be(typeof(TestService1));
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().BeNull();
            inst1.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("MyService2");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().Be(testInstance);
            inst2.ImplementationFactory.Should().BeNull();
            inst2.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst2.GetImplementationType().Should().Be(typeof(TestService1));
            inst3.Should().NotBeNull();
            inst3.ServiceName.Should().Be("MyService3");
            inst3.ServiceType.Should().Be(typeof(ITestService));
            inst3.ImplementationType.Should().BeNull();
            inst3.ImplementationInstance.Should().BeNull();
            inst3.ImplementationFactory.Should().NotBeNull();
            inst3.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst3.GetImplementationType().Should().Be(typeof(object));
            inst4.Should().NotBeNull();
            inst4.ServiceName.Should().Be("MyService4");
            inst4.ServiceType.Should().Be(typeof(ITestService));
            inst4.ImplementationType.Should().Be(typeof(TestService1));
            inst4.ImplementationInstance.Should().BeNull();
            inst4.ImplementationFactory.Should().BeNull();
            inst4.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst4.GetImplementationType().Should().Be(typeof(TestService1));
            inst5.Should().NotBeNull();
            inst5.ServiceName.Should().Be("MyService5");
            inst5.ServiceType.Should().Be(typeof(ITestService));
            inst5.ImplementationType.Should().BeNull();
            inst5.ImplementationInstance.Should().Be(testInstance);
            inst5.ImplementationFactory.Should().BeNull();
            inst5.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst5.GetImplementationType().Should().Be(typeof(TestService1));
            inst6.Should().NotBeNull();
            inst6.ServiceName.Should().Be("MyService6");
            inst6.ServiceType.Should().Be(typeof(ITestService));
            inst6.ImplementationType.Should().BeNull();
            inst6.ImplementationInstance.Should().BeNull();
            inst6.ImplementationFactory.Should().NotBeNull();
            inst6.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst6.GetImplementationType().Should().Be(typeof(TestService1));
            inst7.Should().NotBeNull();
            inst7.ServiceName.Should().Be("MyService7");
            inst7.ServiceType.Should().Be(typeof(ITestService));
            inst7.ImplementationType.Should().BeNull();
            inst7.ImplementationInstance.Should().BeNull();
            inst7.ImplementationFactory.Should().NotBeNull();
            inst7.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst7.GetImplementationType().Should().Be(typeof(ITestService));
        }

        [Test]
        public void ShouldThrowForNullServiceType()
        {
            var testImplementation = new TestService1();

            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(null!, "name", typeof(TestService1)));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(null!, "name", _ => testImplementation));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(null!, "name", typeof(TestService1)));
            var ex4 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(null!, "name", _ => testImplementation));
            var ex5 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(null!, "name", typeof(TestService1)));
            var ex6 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(null!, "name", _ => testImplementation));
            var ex7 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(null!, "name", testImplementation));

            ex1!.ParamName.Should().Be("serviceType");
            ex2!.ParamName.Should().Be("serviceType");
            ex3!.ParamName.Should().Be("serviceType");
            ex4!.ParamName.Should().Be("serviceType");
            ex5!.ParamName.Should().Be("serviceType");
            ex6!.ParamName.Should().Be("serviceType");
            ex7!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceTypeCallingDescribe()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(null!, "name", typeof(TestService1), ServiceLifetime.Transient));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(null!, "name", sp => sp.GetRequiredService(typeof(ITestService)), ServiceLifetime.Scoped));

            ex1!.ParamName.Should().Be("serviceType");
            ex2!.ParamName.Should().Be("serviceType");
        }

        [Test]
        public void ShouldThrowForNullServiceName()
        {
            var testImplementation = new TestService1();

            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(typeof(ITestService), null!, typeof(TestService1)));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(typeof(ITestService), null!, _ => testImplementation));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(typeof(ITestService), null!, typeof(TestService1)));
            var ex4 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(typeof(ITestService), null!, _ => testImplementation));
            var ex5 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), null!, typeof(TestService1)));
            var ex6 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), null!, _ => testImplementation));
            var ex7 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), null!, testImplementation));
            var ex8 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient<ITestService, TestService1>(null!));
            var ex9 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient<ITestService>(null!, _ => testImplementation));
            var ex10 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient<ITestService, TestService1>(null!, _ => testImplementation));
            var ex11 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped<ITestService, TestService1>(null!));
            var ex12 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped<ITestService>(null!, _ => testImplementation));
            var ex13 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped<ITestService, TestService1>(null!, _ => testImplementation));
            var ex14 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService, TestService1>(null!));
            var ex15 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService>(null!, _ => testImplementation));
            var ex16 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService>(null!, testImplementation));
            var ex17 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService, TestService1>(null!, _ => testImplementation));

            ex1!.ParamName.Should().Be("serviceName");
            ex2!.ParamName.Should().Be("serviceName");
            ex3!.ParamName.Should().Be("serviceName");
            ex4!.ParamName.Should().Be("serviceName");
            ex5!.ParamName.Should().Be("serviceName");
            ex6!.ParamName.Should().Be("serviceName");
            ex7!.ParamName.Should().Be("serviceName");
            ex8!.ParamName.Should().Be("serviceName");
            ex9!.ParamName.Should().Be("serviceName");
            ex10!.ParamName.Should().Be("serviceName");
            ex11!.ParamName.Should().Be("serviceName");
            ex12!.ParamName.Should().Be("serviceName");
            ex13!.ParamName.Should().Be("serviceName");
            ex14!.ParamName.Should().Be("serviceName");
            ex15!.ParamName.Should().Be("serviceName");
            ex16!.ParamName.Should().Be("serviceName");
            ex17!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullServiceNameCallingDescribe()
        {
            var testImplementation = new TestService1();

            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(typeof(ITestService), null!, typeof(TestService1), ServiceLifetime.Transient));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe<ITestService, TestService1>(null!, ServiceLifetime.Transient));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(typeof(ITestService), null!, _ => testImplementation, ServiceLifetime.Scoped));

            ex1!.ParamName.Should().Be("serviceName");
            ex2!.ParamName.Should().Be("serviceName");
            ex3!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldThrowForNullImplementationType()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(typeof(ITestService), "name", (Type)null!));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(typeof(ITestService), "name", (Type)null!));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), "name", (Type)null!));

            ex1!.ParamName.Should().Be("implementationType");
            ex2!.ParamName.Should().Be("implementationType");
            ex3!.ParamName.Should().Be("implementationType");
        }

        [Test]
        public void ShouldThrowForNullImplementationTypeCallingDescribe()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(typeof(ITestService), "name", (Type)null!, ServiceLifetime.Transient));

            ex1!.ParamName.Should().Be("implementationType");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactory()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient(typeof(ITestService), "name", (Func<IServiceProvider, object>)null!));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped(typeof(ITestService), "name", (Func<IServiceProvider, object>)null!));
            var ex3 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), "name", (Func<IServiceProvider, object>)null!));
            var ex4 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient("name", (Func<IServiceProvider, ITestService>)null!));
            var ex5 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped("name", (Func<IServiceProvider, ITestService>)null!));
            var ex6 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton("name", (Func<IServiceProvider, ITestService>)null!));
            // ReSharper disable RedundantCast - test should be explicit
            var ex7 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Transient<ITestService, TestService1>("name", (Func<IServiceProvider, TestService1>)null!));
            var ex8 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Scoped<ITestService, TestService1>("name", (Func<IServiceProvider, TestService1>)null!));
            var ex9 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService, TestService1>("name", (Func<IServiceProvider, TestService1>)null!));
            // ReSharper restore RedundantCast

            ex1!.ParamName.Should().Be("implementationFactory");
            ex2!.ParamName.Should().Be("implementationFactory");
            ex3!.ParamName.Should().Be("implementationFactory");
            ex4!.ParamName.Should().Be("implementationFactory");
            ex5!.ParamName.Should().Be("implementationFactory");
            ex6!.ParamName.Should().Be("implementationFactory");
            ex7!.ParamName.Should().Be("implementationFactory");
            ex8!.ParamName.Should().Be("implementationFactory");
            ex9!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationFactoryCallingDescribe()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(typeof(ITestService), "name", (Func<IServiceProvider, object>)null!, ServiceLifetime.Transient));

            ex1!.ParamName.Should().Be("implementationFactory");
        }

        [Test]
        public void ShouldThrowForNullImplementationInstance()
        {
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton(typeof(ITestService), "name", (TestService1)null!));
            var ex2 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Singleton<ITestService>("name", (TestService1)null!));
            
            ex1!.ParamName.Should().Be("implementationInstance");
            ex2!.ParamName.Should().Be("implementationInstance");
        }

        [Test]
        public void ShouldThrowForNullBaseDescriptor()
        {
            // ReSharper disable once RedundantCast - test should be explicit
            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe((ServiceDescriptor)null!, "name"));

            ex1!.ParamName.Should().Be("descriptor");
        }

        [Test]
        public void ShouldThrowForNullServiceNameUsingBaseDescriptor()
        {
            var baseDescriptor = ServiceDescriptor.Singleton<ITestService, TestService1>();

            var ex1 = Assert.Throws<ArgumentNullException>(() => NamedServiceDescriptor.Describe(baseDescriptor, null!));

            ex1!.ParamName.Should().Be("serviceName");
        }

        [Test]
        public void ShouldCorrectlyWrapBaseServiceDescriptor()
        {
            var testInstance = new TestService1();

            var inst1 = NamedServiceDescriptor.Describe(ServiceDescriptor.Singleton<ITestService, TestService1>(), "name");
            var inst2 = NamedServiceDescriptor.Describe(ServiceDescriptor.Singleton<ITestService, TestService1>(_ => testInstance), "name");
            var inst3 = NamedServiceDescriptor.Describe(ServiceDescriptor.Singleton<ITestService>(_ => testInstance), "name");
            var inst4 = NamedServiceDescriptor.Describe(ServiceDescriptor.Singleton<ITestService>(testInstance), "name");

            inst1.Should().NotBeNull();
            inst1.ServiceName.Should().Be("name");
            inst1.ServiceType.Should().Be(typeof(ITestService));
            inst1.ImplementationType.Should().Be(typeof(TestService1));
            inst1.ImplementationInstance.Should().BeNull();
            inst1.ImplementationFactory.Should().BeNull();
            inst1.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst1.GetImplementationType().Should().Be(typeof(TestService1));
            inst2.Should().NotBeNull();
            inst2.ServiceName.Should().Be("name");
            inst2.ServiceType.Should().Be(typeof(ITestService));
            inst2.ImplementationType.Should().BeNull();
            inst2.ImplementationInstance.Should().BeNull();
            inst2.ImplementationFactory.Should().NotBeNull();
            inst2.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst2.GetImplementationType().Should().Be(typeof(TestService1));
            inst3.Should().NotBeNull();
            inst3.ServiceName.Should().Be("name");
            inst3.ServiceType.Should().Be(typeof(ITestService));
            inst3.ImplementationType.Should().BeNull();
            inst3.ImplementationInstance.Should().BeNull();
            inst3.ImplementationFactory.Should().NotBeNull();
            inst3.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst3.GetImplementationType().Should().Be(typeof(ITestService));
            inst4.Should().NotBeNull();
            inst4.ServiceName.Should().Be("name");
            inst4.ServiceType.Should().Be(typeof(ITestService));
            inst4.ImplementationType.Should().BeNull();
            inst4.ImplementationInstance.Should().Be(testInstance);
            inst4.ImplementationFactory.Should().BeNull();
            inst4.Lifetime.Should().Be(ServiceLifetime.Singleton);
            inst4.GetImplementationType().Should().Be(typeof(TestService1));
        }

        [Test, Ignore("Unable to properly mock ServiceDescriptor base class to inject the relevant properties.")]
        public void ShouldTrowForInvalidBaseDescriptor()
        {
            var baseDescriptor = NamedServiceDescriptorTestWrapper.CreateMockedServiceDescriptor();

            var ex1 = Assert.Throws<ArgumentException>(() => NamedServiceDescriptor.Describe(baseDescriptor, "name"));

            ex1!.ParamName.Should().Be("descriptor");
        }
    }
}
