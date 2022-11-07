using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests.TestServices
{
    internal class TestInitService3 : ITestInitService3
    {
        private readonly ITestInitService2 _testInitService;

        public TestInitService3(ITestInitService2 testInitService)
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
