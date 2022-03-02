using System;
using System.Diagnostics;
#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Describes a serviceType with its serviceType type, name, implementation, and lifetime.
    /// </summary>
    [PublicAPI]
    [DebuggerDisplay("Lifetime = {Lifetime}, ServiceType = {ServiceType}, ImplementationType = {ImplementationType}, ServiceName = {ServiceName}")]
    public class NamedServiceDescriptor : ServiceDescriptor
    {
        /// <summary>
        /// Initializes a new instance of <see cref="NamedServiceDescriptor"/> with the specified <paramref name="implementationType"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationType">The <see cref="Type"/> implementing the serviceType.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the serviceType.</param>
        public NamedServiceDescriptor(
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType,
            ServiceLifetime lifetime)
            : base(serviceType, implementationType, lifetime)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            ServiceName = serviceName;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NamedServiceDescriptor"/> with the specified <paramref name="instance"/>
        /// as a <see cref="ServiceLifetime.Singleton"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="instance">The instance implementing the serviceType.</param>
        public NamedServiceDescriptor(
            Type serviceType,
            string serviceName,
            object instance)
            : base(serviceType, instance)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            ServiceName = serviceName;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NamedServiceDescriptor"/> with the specified <paramref name="factory"/>.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="factory">A factory used for creating serviceType instances.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the serviceType.</param>
        public NamedServiceDescriptor(
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> factory,
            ServiceLifetime lifetime)
            : base(serviceType, factory, lifetime)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            ServiceName = serviceName;
        }

        /// <summary>
        /// Gets the name of the serviceType.
        /// </summary>
        public string ServiceName { get; }

        /// <inheritdoc />
        public override string ToString()
            => base.ToString() + $" {nameof(ServiceName)}: {ServiceName}";

        /// <summary>
        /// Gets the descriptors implementation type based on the detected implementation method.
        /// </summary>
        /// <remarks>Copied from MS.Ext.DI implementation.</remarks>
        /// <returns>The serviceType type of the defined implementation mechanism.</returns>
        internal Type GetImplementationType()
        {
            if (ImplementationType != null)
            {
                return ImplementationType;
            }

            if (ImplementationInstance != null)
            {
                return ImplementationInstance.GetType();
            }

            if (ImplementationFactory != null)
            {
                var typeArguments = ImplementationFactory.GetType().GenericTypeArguments;

                Debug.Assert(typeArguments.Length == 2, "ImplementationFactory has invalid type arguments.");

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType, ImplementationInstance or ImplementationFactory must be non null");
            return null!;
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
#if NET5_0_OR_GREATER
        public static NamedServiceDescriptor Transient<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(string serviceName)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static NamedServiceDescriptor Transient<TService, TImplementation>(string serviceName)
#endif
            where TService : class
            where TImplementation : class, TService
            => Describe<TService, TImplementation>(serviceName, ServiceLifetime.Transient);

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Transient(
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
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

            return Describe(serviceType, serviceName, implementationType, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Transient<TService, TImplementation>(
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Transient<TService>(string serviceName, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Transient(Type serviceType, string serviceName, Func<IServiceProvider, object> implementationFactory)
        {
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

            return Describe(serviceType, serviceName, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
#if NET5_0_OR_GREATER
        public static NamedServiceDescriptor Scoped<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(string serviceName)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static NamedServiceDescriptor Scoped<TService, TImplementation>(string serviceName)
#endif
            where TService : class
            where TImplementation : class, TService
            => Describe<TService, TImplementation>(serviceName, ServiceLifetime.Scoped);

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Scoped(
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
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

            return Describe(serviceType, serviceName, implementationType, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Scoped<TService, TImplementation>(
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Scoped<TService>(string serviceName, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Scoped(Type serviceType, string serviceName, Func<IServiceProvider, object> implementationFactory)
        {
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

            return Describe(serviceType, serviceName, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
#if NET5_0_OR_GREATER
        public static NamedServiceDescriptor Singleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(string serviceName)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static NamedServiceDescriptor Singleton<TService, TImplementation>(string serviceName)
#endif
            where TService : class
            where TImplementation : class, TService
            => Describe<TService, TImplementation>(serviceName, ServiceLifetime.Singleton);

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton(
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType)
        {
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

            return Describe(serviceType, serviceName, implementationType, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton<TService, TImplementation>(
            string serviceName,
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton<TService>(string serviceName, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return Describe(typeof(TService), serviceName, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton(
            Type serviceType,
            string serviceName,
            Func<IServiceProvider, object> implementationFactory)
        {
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

            return Describe(serviceType, serviceName, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationInstance"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationInstance">The instance of the implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton<TService>(string serviceName, TService implementationInstance)
            where TService : class
        {
            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            return Singleton(typeof(TService), serviceName, implementationInstance);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationInstance"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationInstance">The instance of the implementation.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Singleton(
            Type serviceType,
            string serviceName,
            object implementationInstance)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationInstance is null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return new NamedServiceDescriptor(serviceType, serviceName, implementationInstance);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="serviceName"/> and <paramref name="lifetime"/>.
        /// </summary>
        /// <typeparam name="TService">The type of the serviceType.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="lifetime">The lifetime of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
#if NET5_0_OR_GREATER
        public static NamedServiceDescriptor Describe<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(string serviceName, ServiceLifetime lifetime)
#elif NETSTANDARD2_0_OR_GREATER || NET461
        public static NamedServiceDescriptor Describe<TService, TImplementation>(string serviceName, ServiceLifetime lifetime)
#endif
            where TService : class
            where TImplementation : class, TService
            => Describe(typeof(TService), serviceName, typeof(TImplementation), lifetime);

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationType"/>,
        /// <paramref name="serviceName"/> and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <param name="lifetime">The lifetime of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Describe(
            Type serviceType,
            string serviceName,
#if NET5_0_OR_GREATER
            [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
#endif
            Type implementationType,
            ServiceLifetime lifetime)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return new NamedServiceDescriptor(serviceType, serviceName, implementationType, lifetime);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the specified
        /// <paramref name="serviceType"/>, <paramref name="implementationFactory"/>,
        /// and <paramref name="lifetime"/>.
        /// </summary>
        /// <param name="serviceType">The type of the serviceType.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <param name="implementationFactory">A factory to create new instances of the serviceType implementation.</param>
        /// <param name="lifetime">The lifetime of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Describe(Type serviceType, string serviceName, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (implementationFactory is null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return new NamedServiceDescriptor(serviceType, serviceName, implementationFactory, lifetime);
        }

        /// <summary>
        /// Creates an instance of <see cref="NamedServiceDescriptor"/> with the details of an
        /// existing <see cref="ServiceDescriptor"/> and specified <paramref name="serviceName"/>.
        /// </summary>
        /// <param name="descriptor">A serviceType descriptor.</param>
        /// <param name="serviceName">The name of the serviceType.</param>
        /// <returns>A new instance of <see cref="NamedServiceDescriptor"/>.</returns>
        public static NamedServiceDescriptor Describe(ServiceDescriptor descriptor, string serviceName)
        {
            if (descriptor is null)
            {
                throw new ArgumentNullException(nameof(descriptor));
            }

            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            if (descriptor.ImplementationType is not null)
            {
                return new NamedServiceDescriptor(
                    descriptor.ServiceType,
                    serviceName,
                    descriptor.ImplementationType,
                    descriptor.Lifetime);
            }

            if (descriptor.ImplementationInstance is not null)
            {
                return new NamedServiceDescriptor(
                    descriptor.ServiceType,
                    serviceName,
                    descriptor.ImplementationInstance);
            }

            if (descriptor.ImplementationFactory is not null)
            {
                return new NamedServiceDescriptor(
                    descriptor.ServiceType,
                    serviceName,
                    descriptor.ImplementationFactory,
                    descriptor.Lifetime);
            }

            throw new ArgumentException("Unable to determine serviceType descriptor implementation.", nameof(descriptor));
        }
    }
}
