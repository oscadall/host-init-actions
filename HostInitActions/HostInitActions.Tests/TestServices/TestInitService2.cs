using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests.TestServices
{
    internal class TestInitService2 : ITestInitService2
    {
        private readonly ITestInitService _testInitService;

        public TestInitService2(ITestInitService testInitService)
        {
            _testInitService = testInitService;
        }

        public bool Initialized { get; private set; }

        public Task Init(CancellationToken cancellationToken)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Service is initialized");
            }

            if (!_testInitService.Initialized)
            {
                throw new InvalidOperationException("Dependency not initialized");
            }

            Initialized = true;
            return Task.CompletedTask;
        }
    }
}
