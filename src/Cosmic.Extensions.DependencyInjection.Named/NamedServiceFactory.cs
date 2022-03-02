using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// A factory class for creating Named service implementations.
    /// </summary>
    [PublicAPI]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "We are faking system service implementation here.")]
    public class NamedServiceFactory : ISupportNamedServices, ISupportRequiredNamedServices
    {
        private readonly IServiceProvider _services;

        /// <summary>
        /// Constructs a Named Service Factory.
        /// </summary>
        /// <param name="services">A service provider to enable Dependency Injection.</param>
        public NamedServiceFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services), "Service provider is required to manage named services.");
        }

        /// <summary>
        /// Gets a named service.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>An instance of the required named service if it exists, otherwise <see langword="null"/>.</returns>
        public object? GetNamedService(Type serviceType, string serviceName)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (_services is ISupportNamedServices namedServices)
            {
                return namedServices.GetNamedService(serviceType, serviceName);
            }

            var namedServiceWrapper = GetNamedServiceWrapper(_services, serviceType, serviceName);
            return namedServiceWrapper?.Service;
        }

        /// <summary>
        /// Gets a collection of named service.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A collection of instances of the required named service.</returns>
        public IEnumerable<object?> GetNamedServices(Type serviceType, string serviceName)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (_services is ISupportNamedServices namedServices)
            {
                return namedServices.GetNamedServices(serviceType, serviceName);
            }

            var namedServiceWrapper = GetNamedServiceWrappers(_services, serviceType, serviceName);
            return namedServiceWrapper.Select(x => x.Service);
        }

        /// <summary>
        /// Gets a named service.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>An instance of the required named service.</returns>
        /// <exception cref="NullReferenceException">Thrown if the service doesn't exist.</exception>
        public object GetRequiredNamedService(Type serviceType, string serviceName)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (_services is ISupportRequiredNamedServices namedServices)
            {
                return namedServices.GetRequiredNamedService(serviceType, serviceName);
            }

            var namedServiceWrapper = GetNamedServiceWrapper(_services, serviceType, serviceName);
            return namedServiceWrapper?.Service
                   ?? throw new NullReferenceException($"No services registered for {serviceType} with a name of '{serviceName}'.");
        }

        private static INamedService? GetNamedServiceWrapper(IServiceProvider services, Type serviceType, string serviceName)
            => services.GetService(NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName)) as INamedService;

        private static IEnumerable<INamedService> GetNamedServiceWrappers(IServiceProvider services, Type serviceType, string serviceName)
            => services.GetRequiredService(NamedServiceManager.CreateNamedServiceEnumerableInterface(serviceType, serviceName)) as IEnumerable<INamedService>
                ?? Enumerable.Empty<INamedService>();
    }
}
