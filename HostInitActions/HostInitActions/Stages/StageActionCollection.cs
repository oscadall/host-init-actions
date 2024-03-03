using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HostInitActions.Stages
{
    internal class StageActionCollection : IInitStageActionCollection
    {
        private readonly IServiceCollection _services;
        private readonly List<Func<IServiceProvider, IAsyncInitActionExecutor>> _actionExecutorFactories = new List<Func<IServiceProvider, IAsyncInitActionExecutor>>();

        public StageActionCollection(object stageKey, IServiceCollection services)
        {
            _services = services;
            StageKey = stageKey;
        }

        public object StageKey { get; }

        public IAsyncInitActionExecutor BuildStageExecutor(IServiceProvider serviceProvider)
        {
            var executors = _actionExecutorFactories
                .Select(factory => factory(serviceProvider))
                .ToArray();

            return new StageExecutor(StageKey, executors, serviceProvider.GetService<ILogger<StageExecutor>>());
        }

        public IInitStageActionCollection AddInitAction<TService>(Func<TService, CancellationToken, Task> initializationAction)
            where TService : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), initializationAction));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService>(Func<TService, Task> initializationAction)
            where TService : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), (s, ct) => initializationAction(s)));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    initializationAction));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    (s1, s2, ct) => initializationAction(s1, s2)));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3>(Func<TService1, TService2, TService3, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    initializationAction));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3>(Func<TService1, TService2, TService3, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    (s1, s2, s3, ct) => initializationAction(s1, s2, s3)));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3, TService4>(Func<TService1, TService2, TService3, TService4, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    initializationAction));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3, TService4>(Func<TService1, TService2, TService3, TService4, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    (s1, s2, s3, s4, ct) => initializationAction(s1, s2, s3, s4)));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(
            Func<TService1, TService2, TService3, TService4, TService5, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4, TService5>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    sp.GetRequiredService<TService5>(),
                    initializationAction));

            return this;
        }

        public IInitStageActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(
            Func<TService1, TService2, TService3, TService4, TService5, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull
        {
            _actionExecutorFactories.Add(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4, TService5>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    sp.GetRequiredService<TService5>(),
                    (s1, s2, s3, s4, s5, ct) => initializationAction(s1, s2, s3, s4, s5)));

            return this;
        }

        public IInitStageActionCollection AddInitActionExecutor<TInitActionExecutor>()
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _services.AddSingleton<TInitActionExecutor>();
            _actionExecutorFactories.Add(sp => sp.GetRequiredService<TInitActionExecutor>());

            return this;
        }

        public IInitStageActionCollection AddInitActionExecutor<TInitActionExecutor>(TInitActionExecutor initActionExecutor)
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _actionExecutorFactories.Add(sp => initActionExecutor);

            return this;
        }

        public IInitStageActionCollection AddInitActionExecutor<TInitActionExecutor>(Func<IServiceProvider, TInitActionExecutor> factoryFunc)
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _actionExecutorFactories.Add(factoryFunc);

            return this;
        }
    }
}
