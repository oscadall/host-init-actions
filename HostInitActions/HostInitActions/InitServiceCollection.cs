using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HostInitActions
{
    internal class InitServiceCollection : IInitActionCollection
    {
        private readonly IServiceCollection _services;

        public InitServiceCollection(IServiceCollection services)
        {
            _services = services;
        }

        public IInitActionCollection AddInitAction<TService>(Func<TService, CancellationToken, Task> initializationAction)
            where TService : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService>(Func<TService, Task> initializationAction)
            where TService : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), (s, ct) => initializationAction(s)));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    (s1, s2, ct) => initializationAction(s1, s2)));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3>(Func<TService1, TService2, TService3, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3>(Func<TService1, TService2, TService3, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    (s1, s2, s3, ct) => initializationAction(s1, s2, s3)));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(Func<TService1, TService2, TService3, TService4, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(Func<TService1, TService2, TService3, TService4, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    (s1, s2, s3, s4, ct) => initializationAction(s1, s2, s3, s4)));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(Func<TService1, TService2, TService3, TService4, TService5, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4, TService5>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    sp.GetRequiredService<TService5>(),
                    initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(Func<TService1, TService2, TService3, TService4, TService5, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull
        {
            _services.AddSingleton<IAsyncInitExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4, TService5>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    sp.GetRequiredService<TService5>(),
                    (s1, s2, s3, s4, s5, ct) => initializationAction(s1, s2, s3, s4, s5)));

            return this;
        }

        public IInitActionCollection AddInitExecutor<TInitExecutor>()
            where TInitExecutor : class, IAsyncInitExecutor
        {
            _services.AddSingleton<IAsyncInitExecutor, TInitExecutor>();
            return this;
        }

        public IInitActionCollection AddInitExecutor<TInitExecutor>(TInitExecutor initExecutor)
            where TInitExecutor : class, IAsyncInitExecutor
        {
            _services.AddSingleton<IAsyncInitExecutor>(initExecutor);
            return this;
        }

        public IInitActionCollection AddInitExecutor<TInitActionClass>(Func<IServiceProvider, TInitActionClass> factoryFunc)
            where TInitActionClass : class, IAsyncInitExecutor
        {
            _services.AddSingleton<IAsyncInitExecutor>(factoryFunc);
            return this;
        }
    }
}

