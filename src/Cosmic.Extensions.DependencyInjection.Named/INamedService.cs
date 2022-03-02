using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Cosmic.Extensions.DependencyInjection
{
    /// <summary> Used internally by the DI framework to store named service instances. </summary>
    /// <typeparam name="TService">The type of service.</typeparam>
    /// <typeparam name="TKey">The typed name of the service.</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Different versions of same interface are better to keep together.")]
    internal interface INamedService<[UsedImplicitly] out TService, [UsedImplicitly] in TKey> : INamedService
    {
    }

    /// <summary> Used internally by the DI framework to store named service instances. </summary>
    internal interface INamedService
    {
        /// <summary> Gets the service instance. </summary>
        [UsedImplicitly]
        object Service { get; }
    }
}
