using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public interface IInitActionCollection
    {
        IInitActionCollection AddInitAction<TService>(
            Func<TService, CancellationToken, Task> initializationAction);
        IInitActionCollection AddInitAction<TService>(
            Func<TService, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2>(
            Func<TService1, TService2, CancellationToken, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2>(
            Func<TService1, TService2, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2, TService3>(
            Func<TService1, TService2, TService3, CancellationToken, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2, TService3>(
            Func<TService1, TService2, TService3, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(
            Func<TService1, TService2, TService3, TService4, CancellationToken, Task> initializationAction);

        IInitActionCollection AddInitAction<TService1, TService2, TService3, TService4>(
            Func<TService1, TService2, TService3, TService4, Task> initializationAction);
    }
}
