using System;
using JetBrains.Annotations;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary> Internal implementation for <see cref="INamedService{TService,TKey}" />. </summary>
    /// <typeparam name="TService">The service type.</typeparam>
    /// <typeparam name="TKey">The service's name type.</typeparam>
    [UsedImplicitly]
    internal sealed class NamedService<TService, TKey> : INamedService<TService, TKey>
        where TService : class
    {
        #region Constructors

        /// <summary>
        /// Constructs a Named Service.
        /// </summary>
        /// <param name="service">The service implementation to expose.</param>
        [UsedImplicitly] // via Activator in NamedServiceDescriptor
        public NamedService(TService service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #endregion

        #region INamedService

        /// <inheritdoc />
        public object Service { get; }

        #endregion
    }
}
