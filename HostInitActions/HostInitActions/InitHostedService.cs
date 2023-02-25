using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    internal class InitHostedService : IHostedService
    {
        private readonly InitExecutionService _initExecutionService;

        public InitHostedService(InitExecutionService initExecutionService)
        {
            _initExecutionService = initExecutionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _initExecutionService.ExecuteInitActionsAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
