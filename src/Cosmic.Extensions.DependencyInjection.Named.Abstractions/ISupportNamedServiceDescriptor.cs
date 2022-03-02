#if NET5_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary>
    /// Specifies the contract for an <see cref="IServiceCollection"/> that supports <see cref="NamedServiceDescriptor"/>.
    /// </summary>
    [PublicAPI]
    public interface ISupportNamedServiceDescriptor : IServiceCollection
    {
        /// <summary>
        /// Adds a <see cref="NamedServiceDescriptor"/> to the services collection.
        /// </summary>
        /// <param name="descriptor">The named service descriptor.</param>
        void Add(NamedServiceDescriptor descriptor);

        /// <summary>
        /// Adds a <see cref="NamedServiceDescriptor"/> to the services collection.
        /// </summary>
        /// <param name="descriptor">The service descriptor.</param>
        /// <param name="serviceName">The name to use for the service.</param>
#if NET5_0_OR_GREATER
        [ExcludeFromCodeCoverage]
#endif
        void Add(ServiceDescriptor descriptor, string serviceName)
#if NET5_0_OR_GREATER
            => Add(NamedServiceDescriptor.Describe(descriptor, serviceName))
#endif
            ;
    }
}
