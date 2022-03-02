using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// A set of extension over <see cref="NamedServiceFactory"/> that provide generic typed versions of it's methods.
    /// </summary>
    [PublicAPI]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2201:Do not raise reserved exception types", Justification = "We are faking system service implementation here.")]
    public static class NamedServiceFactoryExtensions
    {
        /// <summary>
        /// Gets a named service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">A named services factory.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>An instance of the required named service if it exists, otherwise <see langword="null"/>.</returns>
        public static TService? GetNamedService<TService>(this NamedServiceFactory factory, string serviceName)
            where TService : class
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory), "Named Service Factory is required for method forwarding.");
            }

            return factory.GetNamedService(typeof(TService), serviceName) as TService;
        }

        /// <summary>
        /// Gets a collection of named service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">A named services factory.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>A collection of instances of the required named service.</returns>
        public static IEnumerable<TService> GetNamedServices<TService>(this NamedServiceFactory factory, string serviceName)
            where TService : class
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory), "Named Service Factory is required for method forwarding.");
            }

            return factory.GetNamedServices(typeof(TService), serviceName).Where(x => x is TService).Cast<TService>();
        }

        /// <summary>
        /// Gets a named service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">A named services factory.</param>
        /// <param name="serviceName">The name of the service.</param>
        /// <returns>An instance of the required named service.</returns>
        /// <exception cref="NullReferenceException">Thrown if the service doesn't exist.</exception>
        public static TService GetRequiredNamedService<TService>(this NamedServiceFactory factory, string serviceName)
            where TService : class
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory), "Named Service Factory is required for method forwarding.");
            }

            return factory.GetRequiredNamedService(typeof(TService), serviceName) as TService
                ?? throw new NullReferenceException($"No services registered for {typeof(TService)} with a name of '{serviceName}'.");
        }
    }
}
