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

    public class AsyncInitializationAction<TService1, TService2> : IAsyncInitializationAction
    {
        private readonly TService1 _service1;
        private readonly TService2 _service2;
        private readonly Func<TService1, TService2, CancellationToken, Task> _action;

        public AsyncInitializationAction(TService1 service1, TService2 service2, Func<TService1, TService2, CancellationToken, Task> action)
        {
            _service1 = service1;
            _service2 = service2;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service1, _service2, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService1, TService2, TService3> : IAsyncInitializationAction
    {
        private readonly TService1 _service1;
        private readonly TService2 _service2;
        private readonly TService3 _service3;
        private readonly Func<TService1, TService2, TService3, CancellationToken, Task> _action;

        public AsyncInitializationAction(TService1 service1, TService2 service2, TService3 service3, Func<TService1, TService2, TService3, CancellationToken, Task> action)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service1, _service2, _service3, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService1, TService2, TService3, TService4> : IAsyncInitializationAction
    {
        private readonly TService1 _service1;
        private readonly TService2 _service2;
        private readonly TService3 _service3;
        private readonly TService4 _service4;
        private readonly Func<TService1, TService2, TService3, TService4, CancellationToken, Task> _action;

        public AsyncInitializationAction(
            TService1 service1, 
            TService2 service2, 
            TService3 service3, 
            TService4 service4, 
            Func<TService1, TService2, TService3, TService4, CancellationToken, Task> action)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service1, _service2, _service3, _service4, cancellationToken);
        }
    }

    public class AsyncInitializationAction<TService1, TService2, TService3, TService4, TService5> : IAsyncInitializationAction
    {
        private readonly TService1 _service1;
        private readonly TService2 _service2;
        private readonly TService3 _service3;
        private readonly TService4 _service4;
        private readonly TService5 _service5;
        private readonly Func<TService1, TService2, TService3, TService4, TService5, CancellationToken, Task> _action;

        public AsyncInitializationAction(
            TService1 service1,
            TService2 service2,
            TService3 service3,
            TService4 service4,
            TService5 service5,
            Func<TService1, TService2, TService3, TService4, TService5, CancellationToken, Task> action)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
            _service5 = service5;
            _action = action;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await _action(_service1, _service2, _service3, _service4, _service5, cancellationToken);
        }
    }
}
