using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    internal class InitHostedService : IHostedService
    {
        private readonly IEnumerable<IAsyncInitExecutor> _initActions;

        public InitHostedService(IEnumerable<IAsyncInitExecutor> initActions)
        {
            _initActions = initActions;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var initAction in _initActions)
            {
                await initAction.ExecuteAsync(cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
