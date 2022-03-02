using System.Diagnostics.CodeAnalysis;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Unit.Fakes
{
    [ExcludeFromCodeCoverage]
    public class TestService1 : ITestService
    {
        public string Name => "TestService1";
    }
}
