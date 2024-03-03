using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HostInitActions.Stages
{
    internal class StageExecutor : IAsyncInitActionExecutor
    {
        private readonly object _stageKey;
        private readonly IEnumerable<IAsyncInitActionExecutor> _actionExecutors;
        private readonly ILogger<StageExecutor>? _logger;

        public StageExecutor(object stageKey, IEnumerable<IAsyncInitActionExecutor> actionExecutors, ILogger<StageExecutor>? logger)
        {
            _stageKey = stageKey;
            _actionExecutors = actionExecutors;
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var executionTasks = _actionExecutors.Select(async executor =>
            {
                try
                {
                    await executor.ExecuteAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    if (_logger != null)
                    {
                        _logger?.LogError(e, $"Initialization error. Stage KEY: {_stageKey}");
                    }
                    else
                    {
                        Console.WriteLine($"Initialization error. Stage KEY: {_stageKey}. Exception: {e}");
                    }

                    throw;
                }
            });

            await Task.WhenAll(executionTasks);
        }
    }
}
