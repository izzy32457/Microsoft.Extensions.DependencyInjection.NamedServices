using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for retrieving services from an <see cref="IServiceProvider" />.
    /// </summary>
    [PublicAPI]
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets a service with the specified type and name.
        /// </summary>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceType">The type of the service required.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>An instance of <paramref name="serviceType"/> with the <paramref name="serviceName"/> specified if it exists, otherwise <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <paramref name="serviceType"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static object? GetNamedService(this IServiceProvider services, Type serviceType, string serviceName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (services is ISupportNamedServices namedServiceProvider)
            {
                return namedServiceProvider.GetNamedService(serviceType, serviceName);
            }

            var namedServiceWrapper = (INamedService?)services.GetService(
                NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName));
            return namedServiceWrapper?.Service;
        }

        /// <summary>
        /// Gets a service with the specified type and name.
        /// </summary>
        /// <typeparam name="TService">The type of the service required.</typeparam>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>An instance of <typeparamref name="TService"/> with the <paramref name="serviceName"/> specified if it exists, otherwise <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <typeparamref name="TService"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static TService? GetNamedService<TService>(this IServiceProvider services, string serviceName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServices namedServiceProvider)
            {
                return (TService?)namedServiceProvider.GetNamedService(serviceType, serviceName);
            }

            var namedServiceWrapper = (INamedService?)services.GetService(
                NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName));
            return (TService?)namedServiceWrapper?.Service;
        }

        /// <summary>
        /// Gets a required service with the specified type and name.
        /// </summary>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceType">The type of the service required.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>An instance of <paramref name="serviceType"/> with the <paramref name="serviceName"/> specified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <paramref name="serviceType"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">There is no service of type <paramref name="serviceType"/> with the name of <paramref name="serviceName"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static object GetRequiredNamedService(this IServiceProvider services, Type serviceType, string serviceName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return services switch
            {
                ISupportRequiredNamedServices namedServiceProvider => namedServiceProvider.GetRequiredNamedService(serviceType, serviceName),
                ISupportNamedServices namedServices => namedServices.GetNamedService(serviceType, serviceName)
                    ?? throw new InvalidOperationException($"No service of type '{serviceType}' with the name '{serviceName}' was found."),
                _ => ((INamedService)services.GetRequiredService(NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName))).Service,
            };
        }

        /// <summary>
        /// Gets a required service with the specified type and name.
        /// </summary>
        /// <typeparam name="TService">The type of the service required.</typeparam>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>An instance of <typeparamref name="TService"/> with the <paramref name="serviceName"/> specified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <typeparamref name="TService"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">There is no service of type <typeparamref name="TService"/> with the name of <paramref name="serviceName"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static TService GetRequiredNamedService<TService>(this IServiceProvider services, string serviceName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            return services switch
            {
                ISupportRequiredNamedServices namedServiceProvider => (TService)namedServiceProvider.GetRequiredNamedService(serviceType, serviceName),
                ISupportNamedServices namedServices => (TService?)namedServices.GetNamedService(serviceType, serviceName)
                    ?? throw new InvalidOperationException($"No service of type '{serviceType}' with the name '{serviceName}' was found."),
                _ => (TService)((INamedService)services.GetRequiredService(NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName))).Service,
            };
        }

        /// <summary>
        /// Gets a collection of registered services for the given <paramref name="serviceType"/> and <paramref name="serviceName"/>.
        /// </summary>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceType">The type of the service required.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>A collection of services with the <paramref name="serviceType"/> and <paramref name="serviceName">name</paramref> specified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <paramref name="serviceType"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static IEnumerable<object?> GetNamedServices(this IServiceProvider services, Type serviceType, string serviceName)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (services is ISupportNamedServices namedServiceProvider)
            {
                return namedServiceProvider.GetNamedServices(serviceType, serviceName);
            }

            var namedServiceWrapper = services.GetServices(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName))
                .Cast<INamedService>();
            return namedServiceWrapper.Select(x => x.Service);
        }

        /// <summary>
        /// Gets a collection of registered services for the given <typeparamref name="TService">type</typeparamref> and <paramref name="serviceName"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service required.</typeparam>
        /// <param name="services">A collection of services.</param>
        /// <param name="serviceName">The name of the service required.</param>
        /// <returns>A collection of services with the <typeparamref name="TService">type</typeparamref> and <paramref name="serviceName">name</paramref> specified.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/>, <typeparamref name="TService"/> or <paramref name="serviceName"/> are <see langword="null"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        public static IEnumerable<TService?> GetNamedServices<TService>(this IServiceProvider services, string serviceName)
            where TService : class
            => services.GetNamedServices(typeof(TService), serviceName).Cast<TService?>();
    }
}
