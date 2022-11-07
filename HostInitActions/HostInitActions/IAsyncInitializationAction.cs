using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    internal interface IAsyncInitializationAction
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
