using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a service that performs initialization actions before the application starts.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        public static IInitActionCollection AddAsyncServiceInitialization(this IServiceCollection services)
        {
            var initServiceRegistered =
                services.Any(descriptor => descriptor.ImplementationType == typeof(InitHostedService));

            if (!initServiceRegistered)
            {
                services.AddHostedService<InitHostedService>();
            }

            return new InitServiceCollection(services);
        }
        
        private class InitServiceCollection : IInitActionCollection
        {
            private readonly IServiceCollection _services;

            public InitServiceCollection(IServiceCollection services)
            {
                _services = services;
            }

            public IInitActionCollection AddInitAction<TService>(Func<TService, CancellationToken, Task> initializationAction)
                where TService : notnull
            {
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService>(sp.GetRequiredService<TService>(), initializationAction));

                return this; 
            }

            public IInitActionCollection AddInitAction<TService>(Func<TService, Task> initializationAction)
                where TService : notnull
            {
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService>(sp.GetRequiredService<TService>(), (s, ct) => initializationAction(s)));

                return this;
            }

            public IInitActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, CancellationToken, Task> initializationAction)
                where TService1 : notnull
                where TService2 : notnull
            {
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2>(
                        sp.GetRequiredService<TService1>(), 
                        sp.GetRequiredService<TService2>(), 
                        initializationAction));

                return this;
            }

            public IInitActionCollection AddInitAction<TService1, TService2>(Func<TService1, TService2, Task> initializationAction)
                where TService1 : notnull
                where TService2 : notnull
            {
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3, TService4>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3, TService4>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3, TService4, TService5>(
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
                _services.AddSingleton<IAsyncInitializationAction>(sp =>
                    new AsyncInitializationAction<TService1, TService2, TService3, TService4, TService5>(
                        sp.GetRequiredService<TService1>(),
                        sp.GetRequiredService<TService2>(),
                        sp.GetRequiredService<TService3>(),
                        sp.GetRequiredService<TService4>(),
                        sp.GetRequiredService<TService5>(),
                        (s1, s2, s3, s4, s5, ct) => initializationAction(s1, s2, s3, s4, s5)));

                return this;
            }
        }
    }
}
