using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests
{
    internal class TestInitService : ITestInitService
    {
        public bool Initialized { get; private set; }

        public Task Init(CancellationToken cancellationToken)
        {
            if (Initialized)
            {
                throw new InvalidOperationException("Service is initialized");
            }

            Initialized = true;
            return Task.CompletedTask;
        }
    }
}
