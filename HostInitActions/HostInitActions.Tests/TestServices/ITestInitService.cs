using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions.Tests;

internal interface ITestInitService
{
    public bool Initialized { get; }

    public Task Init(CancellationToken cancellationToken);
}