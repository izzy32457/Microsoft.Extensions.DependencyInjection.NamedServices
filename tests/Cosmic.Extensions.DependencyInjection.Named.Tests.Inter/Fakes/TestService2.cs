﻿using System.Diagnostics.CodeAnalysis;

namespace Cosmic.Extensions.DependencyInjection.Named.Tests.Inter.Fakes
{
    [ExcludeFromCodeCoverage]
    public class TestService2 : ITestService
    {
        public string Name => "TestService2";
    }
}
