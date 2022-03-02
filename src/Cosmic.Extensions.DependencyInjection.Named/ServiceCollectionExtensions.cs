using System;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection" />.
    /// </summary>
    [PublicAPI]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationType, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationType, serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddTransient<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddTransient<TService, TImplementation>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var implementationType = typeof(TImplementation);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, typeof(TService), serviceName, implementationType, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface<TService>(serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService, TImplementation>(serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient(
            this IServiceCollection services,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type serviceType,
            string serviceName)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, serviceType, serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddTransient<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddTransient<TService>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService>(serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TService>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddTransient<TService, TImplementation>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Transient);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddTransient(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationType, serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddScoped<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddScoped<TService, TImplementation>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            var implementationType = typeof(TImplementation);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService, TImplementation>(serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped(
            this IServiceCollection services,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type serviceType,
            string serviceName)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, serviceType, serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddScoped<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddScoped<TService>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService>(serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TService>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddScoped<TService, TImplementation>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Scoped);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddScoped(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService, TImplementation>(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationType, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationType, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddSingleton<TService, TImplementation>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            var implementationType = typeof(TImplementation);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationType, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService, TImplementation>(serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type serviceType,
            string serviceName)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, serviceType, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
#if NET5_0_OR_GREATER
        public static IServiceCollection AddSingleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static IServiceCollection AddSingleton<TService>(
#endif
            this IServiceCollection services,
            string serviceName)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, serviceType, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService>(serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TService, TImplementation>(
            this IServiceCollection services,
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationFactory, ServiceLifetime.Singleton);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation<TService, TImplementation>(implementationFactory, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton(
            this IServiceCollection services,
            Type serviceType,
            object implementationInstance,
            string serviceName)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationInstance);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(serviceType, implementationInstance, serviceName));
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// instance specified in <paramref name="implementationInstance"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the service to register.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddSingleton<TService>(
            this IServiceCollection services,
            TService implementationInstance,
            string serviceName)
            where TService : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            var serviceType = typeof(TService);
            if (services is ISupportNamedServiceDescriptor namedServices)
            {
                return Add(namedServices, serviceType, serviceName, implementationInstance);
            }

            return services
                .AddNamedServiceSupportServices()
                .AddSingleton(
                    NamedServiceManager.CreateNamedServiceInterface(serviceType, serviceName),
                    sp => sp.CreateNamedServiceImplementation(implementationInstance, serviceName));
        }

        private static ISupportNamedServiceDescriptor Add(
            ISupportNamedServiceDescriptor collection,
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType,
            ServiceLifetime lifetime)
        {
            var descriptor = new NamedServiceDescriptor(serviceType, serviceName, implementationType, lifetime);
            collection.Add(descriptor);
            return collection;
        }

        private static ISupportNamedServiceDescriptor Add(
            ISupportNamedServiceDescriptor collection,
            Type serviceType,
            string serviceName,
            object instance)
        {
            var descriptor = new NamedServiceDescriptor(serviceType, serviceName, instance);
            collection.Add(descriptor);
            return collection;
        }

        private static ISupportNamedServiceDescriptor Add(
            ISupportNamedServiceDescriptor collection,
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> implementationFactory,
            ServiceLifetime lifetime)
        {
            var descriptor = new NamedServiceDescriptor(serviceType, serviceName, implementationFactory, lifetime);
            collection.Add(descriptor);
            return collection;
        }

        private static IServiceCollection AddNamedServiceSupportServices(this IServiceCollection services)
        {
            services
                .TryAddSingleton<NamedServiceFactory>()
                ;
            return services;
        }
    }
}
