using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace HostInitActions
{
    internal class InitExecutionService
    {
        private readonly IEnumerable<IAsyncInitActionExecutor> _initActions;

        public InitExecutionService(IEnumerable<IAsyncInitActionExecutor> initActions)
        {
            _initActions = initActions;
        }

        public async Task ExecuteInitActionsAsync(CancellationToken cancellationToken)
        {
            foreach (var initAction in _initActions)
            {
                await initAction.ExecuteAsync(cancellationToken);
            }
        }
    }
}
