using System.Diagnostics.CodeAnalysis;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Inter.Fakes
{
    [ExcludeFromCodeCoverage]
    public class InvalidTestService : ITestService
    {
        // hiding constructor from IServiceCollection usage
        private InvalidTestService() { }

        public string Name => "InvalidTestService";
    }
}
