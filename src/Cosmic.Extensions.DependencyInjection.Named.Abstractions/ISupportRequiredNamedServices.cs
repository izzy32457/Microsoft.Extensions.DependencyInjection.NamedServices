using System;
using JetBrains.Annotations;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Specifies the contract for an <see cref="IServiceProvider"/> that supports named services.
    /// </summary>
    [PublicAPI]
    public interface ISupportRequiredNamedServices
    {
        /// <summary>
        /// Retrieves a service by name.
        /// </summary>
        /// <param name="serviceType">The type of the service to return.</param>
        /// <param name="serviceName">The name of the service to return.</param>
        /// <returns>An instance of the specified service.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no services were found for the given <paramref name="serviceType"/> and <paramref name="serviceName"/>.</exception>
        [Pure]
        [MustUseReturnValue]
        object GetRequiredNamedService(Type serviceType, string serviceName);
    }
}
