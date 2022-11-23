using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests.TestServices
{
    internal class TestAsyncInitExecutor : IAsyncInitExecutor
    {
        private readonly ITestInitService2 _testInitService2;
        private readonly ITestInitService3 _testInitService3;
        private readonly ITestInitService4 _testInitService4;

        public TestAsyncInitExecutor(
            ITestInitService2 testInitService2,
            ITestInitService3 testInitService3,
            ITestInitService4 testInitService4)
        {
            _testInitService2 = testInitService2;
            _testInitService3 = testInitService3;
            _testInitService4 = testInitService4;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _testInitService2.Init(cancellationToken);
            await _testInitService3.Init(cancellationToken);
            await _testInitService4.Init(cancellationToken);
        }
    }
}
