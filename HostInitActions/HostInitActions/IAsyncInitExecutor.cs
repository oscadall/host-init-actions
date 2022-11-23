using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public interface IAsyncInitExecutor
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
