using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// A manager class for storing named service types and providing mapped <see cref="INamedService"/> implementations.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "We are faking system service implementation here.")]
    internal static class NamedServiceManager
    {
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Using latest language version with simplified new")]
        private static readonly ConcurrentDictionary<string, Type> AnonymousNamedServiceTypes = new(StringComparer.InvariantCultureIgnoreCase);

        private static readonly ModuleBuilder DynamicNamedServiceBuilder = AssemblyBuilder.DefineDynamicAssembly(
                new AssemblyName(Assembly.GetExecutingAssembly().GetName().Name + ".dynamic"), AssemblyBuilderAccess.Run)
            .DefineDynamicModule("MainModule");

        /// <summary>
        /// Creates a named service interface type definition.
        /// </summary>
        /// <param name="serviceType">The type of the underlying service.</param>
        /// <param name="name">The name of the underlying service.</param>
        /// <returns>A type descriptor for an <see cref="INamedService{TService,TKey}"/> with the specified <paramref name="serviceType"/> and <paramref name="name"/>.</returns>
        public static Type CreateNamedServiceInterface(Type serviceType, string name)
        {
            // Create essentially an anon type based on serviceType+name that can be used as the T param in INamedService<T>.
            // We'll use this anon type to be used as a type key to look up the service later via IServiceProvider.
            var namedType = CreateNamedServiceKeyType(name);
            return typeof(INamedService<,>).MakeGenericType(serviceType, namedType);
        }

        /// <summary>
        /// Creates a named service interface type definition.
        /// </summary>
        /// <typeparam name="TService">The type of the underlying service.</typeparam>
        /// <param name="name">The name of the underlying service.</param>
        /// <returns>A type descriptor for an <see cref="INamedService{TService,TKey}"/> with the specified <typeparamref name="TService"/> and <paramref name="name"/>.</returns>
        public static Type CreateNamedServiceInterface<TService>(string name)
            where TService : class
            => CreateNamedServiceInterface(typeof(TService), name);

        /// <summary>
        /// Creates an enumerable named service interface type definition.
        /// </summary>
        /// <param name="serviceType">The type of the underlying service.</param>
        /// <param name="name">The name of the underlying service.</param>
        /// <returns>A type descriptor for an <see cref="IEnumerable{T}"/> of <see cref="INamedService{TService,TKey}"/> with the specified <paramref name="serviceType"/> and <paramref name="name"/>.</returns>
        public static Type CreateNamedServiceEnumerableInterface(Type serviceType, string name)
            => typeof(IEnumerable<>).MakeGenericType(CreateNamedServiceInterface(serviceType, name));

        /// <summary>
        /// Creates a named service implementation type definition.
        /// </summary>
        /// <param name="serviceType">The type of the underlying service.</param>
        /// <param name="name">The name of the underlying service.</param>
        /// <returns>A type descriptor for an <see cref="INamedService{TService,TKey}"/> with the specified <paramref name="serviceType"/> and <paramref name="name"/>.</returns>
        public static Type CreateNamedServiceType(Type serviceType, string name)
        {
            // Create essentially an anon type based on serviceType+name that can be used as the T param in INamedService<T>.
            // We'll use this anon type to be used as a type key to look up the service later via IServiceProvider.
            var namedType = CreateNamedServiceKeyType(name);
            return typeof(NamedService<,>).MakeGenericType(serviceType, namedType);
        }

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="instance">A service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="instance"/>.</returns>
        public static INamedService CreateNamedServiceImplementation(this IServiceProvider services, Type serviceType, object instance, string name)
        {
            var namedServiceType = CreateNamedServiceType(serviceType, name);
            return (INamedService)ActivatorUtilities.CreateInstance(services, namedServiceType, instance);
        }

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service provider.</param>
        /// <param name="instance">A service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="instance"/>.</returns>
        public static INamedService CreateNamedServiceImplementation<TService>(this IServiceProvider services, TService instance, string name)
            where TService : class
            => CreateNamedServiceImplementation(services, typeof(TService), instance, name);

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="instance">A service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="instance"/>.</returns>
        public static INamedService CreateNamedServiceImplementation(Type serviceType, object instance, string name)
        {
            var namedServiceType = CreateNamedServiceType(serviceType, name);
            return Activator.CreateInstance(namedServiceType, instance) as INamedService
                ?? throw new NullReferenceException($"Unable to create a Named Service implementation for {serviceType} and {name}");
        }

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="factory">A factory function to create the service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="factory"/>.</returns>
        public static INamedService CreateNamedServiceImplementation(this IServiceProvider services, Type serviceType, Func<IServiceProvider, object> factory, string name)
        {
            var instance = factory(services);
            return CreateNamedServiceImplementation(serviceType, instance, name);
        }

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="factory">A factory function to create the service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="factory"/>.</returns>
        public static INamedService CreateNamedServiceImplementation<TService>(this IServiceProvider services, Func<IServiceProvider, TService> factory, string name)
            where TService : class
            => CreateNamedServiceImplementation(services, typeof(TService), factory, name);

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementation">The implementation type.</typeparam>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="factory">A factory function to create the service instance.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps the provided <paramref name="factory"/>.</returns>
        public static INamedService CreateNamedServiceImplementation<TService, TImplementation>(this IServiceProvider services, Func<IServiceProvider, TImplementation> factory, string name)
            where TService : class
            where TImplementation : class, TService
            => CreateNamedServiceImplementation(services, typeof(TService), factory, name);

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps an instance of <paramref name="implementationType"/>.</returns>
        public static INamedService CreateNamedServiceImplementation(
            this IServiceProvider services,
            Type serviceType,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType,
            string name)
        {
            var instance = ActivatorUtilities.CreateInstance(services, implementationType);
            return CreateNamedServiceImplementation(serviceType, instance, name);
        }

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps an instance of <typeparamref name="TService"/>.</returns>
#if NET5_0_OR_GREATER
        public static INamedService CreateNamedServiceImplementation<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TService>(this IServiceProvider services, string name)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static INamedService CreateNamedServiceImplementation<TService>(this IServiceProvider services, string name)
#endif
            where TService : class
            => CreateNamedServiceImplementation(services, typeof(TService), typeof(TService), name);

        /// <summary>
        /// Creates an <see cref="INamedService"/> wrapper implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementation">The implementation type.</typeparam>
        /// <param name="services">A service provider for dependency injection.</param>
        /// <param name="name">The service name.</param>
        /// <returns>An <see cref="INamedService"/> that wraps an instance of <typeparamref name="TImplementation"/>.</returns>
#if NET5_0_OR_GREATER
        public static INamedService CreateNamedServiceImplementation<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceProvider services, string name)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static INamedService CreateNamedServiceImplementation<TService, TImplementation>(this IServiceProvider services, string name)
#endif
            where TService : class
            where TImplementation : class, TService
            => CreateNamedServiceImplementation(services, typeof(TService), typeof(TImplementation), name);

        /// <summary>
        /// Emits and returns a dynamic interface type that is unique per <paramref name="name" /> combination.
        /// </summary>
        /// <param name="name">The service name.</param>
        /// <returns>A <see cref="Type"/> that represents the <paramref name="name"/>.</returns>
        /// <exception cref="NullReferenceException">Thrown if the named type cannot be created.</exception>
#if !NET5_0_OR_GREATER && !NET461
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "CreateTypeInfo should never return null.")]
#endif
        internal static Type CreateNamedServiceKeyType(string name)
        {
            return AnonymousNamedServiceTypes.GetOrAdd(
                name, _ =>
                {
                    var dynType = DynamicNamedServiceBuilder.DefineType(
                        name,
                        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Interface | TypeAttributes.Abstract | TypeAttributes.AutoClass | TypeAttributes.AnsiClass);
#if NET5_0_OR_GREATER || NET461
                    return dynType.CreateType()
#elif NETSTANDARD2_0_OR_GREATER
                    return dynType.CreateTypeInfo()!.AsType()
#endif
                           ?? throw new NullReferenceException($"Unable to create a Type definition for {name}");
                });
        }
    }
}
