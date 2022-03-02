using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Cosmic.Extensions.DependencyInjection.Named.Abstractions.Tests.Unit.Fakes
{
    [ExcludeFromCodeCoverage]
    public static class NamedServiceDescriptorTestWrapper
    {
        // NOTE: unable to mock this as the properties are not virtual and cannot be proxied
        public static NamedServiceDescriptor CreateMockedNamedServiceDescriptor(
            string name,
            Type? serviceType = null,
            Type? implementationType = null,
            Func<IServiceProvider, object>? implementationFactory = null,
            object? implementationInstance = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var mock = Substitute.ForPartsOf<NamedServiceDescriptor>(typeof(ITestService), "testService", typeof(TestService1), ServiceLifetime.Singleton);
            mock.ServiceName.Returns(name);
            mock.ImplementationInstance.Returns(implementationInstance);
            mock.ImplementationFactory.Returns(implementationFactory);
            mock.ImplementationType.Returns(implementationType);
            mock.ServiceType.Returns(serviceType);
            mock.Lifetime.Returns(serviceLifetime);
            return mock;
        }

        // NOTE: unable to mock this as the properties are not virtual and cannot be proxied
        public static ServiceDescriptor CreateMockedServiceDescriptor(
            Type? serviceType = null,
            Type? implementationType = null,
            Func<IServiceProvider, object>? implementationFactory = null,
            object? implementationInstance = null,
            ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            var mock = Substitute.ForPartsOf<ServiceDescriptor>(typeof(ITestService), "testService", typeof(TestService1), ServiceLifetime.Singleton);
            mock.ImplementationInstance.Returns(implementationInstance);
            mock.ImplementationFactory.Returns(implementationFactory);
            mock.ImplementationType.Returns(implementationType);
            mock.ServiceType.Returns(serviceType);
            mock.Lifetime.Returns(serviceLifetime);
            return mock;
        }
    }
}
