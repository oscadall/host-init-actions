using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests;

internal interface ITestInitService
{
    public bool Initialized { get; }

    public Task Init(CancellationToken cancellationToken);
}

internal interface ITestInitService2 : ITestInitService {}
internal interface ITestInitService3 : ITestInitService {}
internal interface ITestInitService4 : ITestInitService {}
internal interface ITestInitService5 : ITestInitService {}