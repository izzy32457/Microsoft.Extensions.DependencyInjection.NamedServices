using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Specifies the contract for an <see cref="IServiceProvider"/> that supports named services.
    /// </summary>
    [PublicAPI]
    public interface ISupportNamedServices
    {
        /// <summary>
        /// Retrieves a service by name.
        /// </summary>
        /// <param name="serviceType">The type of the service to return.</param>
        /// <param name="serviceName">The name of the service to return.</param>
        /// <returns>An instance of the specified service, or <see langword="null"/> if none exists.</returns>
        [Pure]
        [MustUseReturnValue]
        object? GetNamedService(Type serviceType, string serviceName);

        /// <summary>
        /// Retrieves a collection of services by name.
        /// </summary>
        /// <param name="serviceType">The type of the service to return.</param>
        /// <param name="serviceName">The name of the service to return.</param>
        /// <returns>A collection of the services with the specified type and name.</returns>
        [Pure]
        [MustUseReturnValue]
        IEnumerable<object?> GetNamedServices(Type serviceType, string serviceName);
    }
}
