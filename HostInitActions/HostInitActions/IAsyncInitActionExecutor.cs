using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public interface IAsyncInitActionExecutor
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
