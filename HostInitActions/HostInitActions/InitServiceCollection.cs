using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HostInitActions.Stages;
using Microsoft.Extensions.DependencyInjection;

namespace HostInitActions
{
    internal class InitServiceCollection : IInitActionCollection
    {
        private readonly IServiceCollection _services;
        private readonly InitContext _context;

        public InitServiceCollection(IServiceCollection services)
        {
            _services = services;

            var initContextType = typeof(InitContext);

            var context = _services.FirstOrDefault(x => x.ServiceType == initContextType)?.ImplementationInstance;
            if (context == null)
            {
                _context = new InitContext();
                _services.AddSingleton(_context);
            }
            else
            {
                _context = (InitContext)context;
            }
        }

        public IInitActionCollection AddInitAction<TService>(Func<TService, CancellationToken, Task> initializationAction)
            where TService : notnull
        {
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), initializationAction));

            return this;
        }

        public IInitActionCollection AddInitAction<TService>(Func<TService, Task> initializationAction)
            where TService : notnull
        {
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
                new AsyncInitAction<TService>(sp.GetRequiredService<TService>(), (s, ct) => initializationAction(s)));

            return this;
        }

        public IInitActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
        {
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
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
            _services.AddSingleton<IAsyncInitActionExecutor>(sp =>
                new AsyncInitAction<TService1, TService2, TService3, TService4, TService5>(
                    sp.GetRequiredService<TService1>(),
                    sp.GetRequiredService<TService2>(),
                    sp.GetRequiredService<TService3>(),
                    sp.GetRequiredService<TService4>(),
                    sp.GetRequiredService<TService5>(),
                    (s1, s2, s3, s4, s5, ct) => initializationAction(s1, s2, s3, s4, s5)));

            return this;
        }

        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>()
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _services.AddSingleton<IAsyncInitActionExecutor, TInitActionExecutor>();
            return this;
        }

        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>(TInitActionExecutor initExecutor)
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _services.AddSingleton<IAsyncInitActionExecutor>(initExecutor);
            return this;
        }

        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>(Func<IServiceProvider, TInitActionExecutor> factoryFunc)
            where TInitActionExecutor : class, IAsyncInitActionExecutor
        {
            _services.AddSingleton<IAsyncInitActionExecutor>(factoryFunc);
            return this;
        }

        public IInitStageActionCollection GetOrAddStage(object stageKey)
        {
            var stageCollection = _context.StageCollections.FirstOrDefault(s => s.StageKey.Equals(stageKey));

            if (stageCollection != null)
            {
                return stageCollection;
            }

            stageCollection = new StageActionCollection(stageKey, _services);
            _context.StageCollections.Add(stageCollection);
            _services.AddSingleton(sp => stageCollection.BuildStageExecutor(sp));

            return stageCollection;
        }
    }
}

