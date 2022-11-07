using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public class AsyncInitializationAction<TService> : IAsyncInitializationAction
    {
        private readonly TService _service;
        private readonly Func<TService, CancellationToken, Task> _action;

        public AsyncInitializationAction(TService service, Func<TService, CancellationToken, Task> action)
        {
            _service = service;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService, TService2> : IAsyncInitializationAction
    {
        private readonly TService _service;
        private readonly TService2 _service2;
        private readonly Func<TService, TService2, CancellationToken, Task> _action;

        public AsyncInitializationAction(TService service, TService2 service2, Func<TService, TService2, CancellationToken, Task> action)
        {
            _service = service;
            _service2 = service2;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service, _service2, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService, TService2, TService3> : IAsyncInitializationAction
    {
        private readonly TService _service;
        private readonly TService2 _service2;
        private readonly TService3 _service3;
        private readonly Func<TService, TService2, TService3, CancellationToken, Task> _action;

        public AsyncInitializationAction(TService service, TService2 service2, TService3 service3, Func<TService, TService2, TService3, CancellationToken, Task> action)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service, _service2, _service3, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService, TService2, TService3, TService4> : IAsyncInitializationAction
    {
        private readonly TService _service;
        private readonly TService2 _service2;
        private readonly TService3 _service3;
        private readonly TService4 _service4;
        private readonly Func<TService, TService2, TService3, TService4, CancellationToken, Task> _action;

        public AsyncInitializationAction(
            TService service, 
            TService2 service2, 
            TService3 service3, 
            TService4 service4, 
            Func<TService, TService2, TService3, TService4, CancellationToken, Task> action)
        {
            _service = service;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service, _service2, _service3, _service4, cancellationToken);
        }
    }
}
