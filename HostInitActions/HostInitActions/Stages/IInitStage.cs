using System.Threading.Tasks;
using System.Threading;
using System;

namespace HostInitActions.Stages
{
    public interface IInitStage
    {
        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService">The service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService>(
            Func<TService, CancellationToken, Task> initializationAction)
            where TService : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService">The service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService>(
            Func<TService, Task> initializationAction)
            where TService : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2>(
            Func<TService1, TService2, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2>(
            Func<TService1, TService2, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3>(
            Func<TService1, TService2, TService3, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3>(
            Func<TService1, TService2, TService3, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService4">Fourth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(
            Func<TService1, TService2, TService3, TService4, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService4">Fourth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(
            Func<TService1, TService2, TService3, TService4, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService4">Fourth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService5">Fifth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(
            Func<TService1, TService2, TService3, TService4, TService5, CancellationToken, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull;

        /// <summary>
        /// Registers a new initialization action.
        /// </summary>
        /// <typeparam name="TService1">First service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService2">Second service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService3">Third service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService4">Fourth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <typeparam name="TService5">Fifth service type that will be obtained from the IoC container and passed as a parameter to the initialization service.</typeparam>
        /// <param name="initializationAction">Initialization action.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4, TService5>(
            Func<TService1, TService2, TService3, TService4, TService5, Task> initializationAction)
            where TService1 : notnull
            where TService2 : notnull
            where TService3 : notnull
            where TService4 : notnull
            where TService5 : notnull;

        /// <summary>
        /// Registers a class that performs the initialization action.
        /// </summary>
        /// <typeparam name="TInitActionExecutor">A class type implementing the <see cref="IAsyncInitActionExecutor"/> interface.</typeparam>
        /// <returns>Collection for registering initialization actions.</returns>
        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>()
            where TInitActionExecutor : class, IAsyncInitActionExecutor;

        /// <summary> 
        /// Registers a class that performs the initialization action.
        /// </summary>
        /// <typeparam name="TInitActionExecutor">A class type implementing the <see cref="IAsyncInitActionExecutor"/> interface.</typeparam>
        /// <param name="initActionExecutor">Executor instance.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>(
            TInitActionExecutor initActionExecutor)
            where TInitActionExecutor : class, IAsyncInitActionExecutor;

        /// <summary> 
        /// Registers a class that performs the initialization action.
        /// </summary>
        /// <typeparam name="TInitActionExecutor">A class type implementing the <see cref="IAsyncInitActionExecutor"/> interface.</typeparam>
        /// <param name="factoryFunc">Factory method to create instance of current <typeparamref name="TInitActionExecutor"/>.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        public IInitActionCollection AddInitActionExecutor<TInitActionExecutor>(
            Func<IServiceProvider, TInitActionExecutor> factoryFunc)
            where TInitActionExecutor : class, IAsyncInitActionExecutor;
    }
}
