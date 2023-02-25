using System.Threading;
using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class ExplicitInitExecutionTests
    {
        [Test]
        public async Task ExecuteInitActionsAsync_RegisteredMultipleActions_Success()
        {
            // ARRANGE
            var services = new ServiceCollection()
                .AddSingleton<ITestInitService, TestInitService>()
                .AddSingleton<ITestInitService2, TestInitService2>()
                .AddSingleton<ITestInitService3, TestInitService3>()
                .AddSingleton<ITestInitService4, TestInitService4>();

            services
                .AddAsyncServiceInitialization()
                .AddInitAction<ITestInitService>(
                    async (service, ct) => await service.Init(ct))
                .AddInitAction<ITestInitService2>(
                    async (service, ct) => await service.Init(ct))
                .AddInitAction<ITestInitService3>(
                    async (service, ct) => await service.Init(ct))
                .AddInitAction<ITestInitService4>(
                    async (service, ct) => await service.Init(ct));

            var serviceProvider = services.BuildServiceProvider();

            // ACT
            await serviceProvider.ExecuteInitActionsAsync();

            // ASSERT
            var service = serviceProvider.GetRequiredService<ITestInitService4>();
            Assert.True(service.Initialized);
        }

        [Test]
        public async Task ExecuteInitActionsAsync_RegisteredNoActions_PassWithNoException()
        {
            // ARRANGE
            var services = new ServiceCollection();
            var serviceProvider = services.BuildServiceProvider();

            // ACT + ASSERT
            await serviceProvider.ExecuteInitActionsAsync();
        }

        [Test]
        public async Task ExecuteInitActionsAsync_RegisteredMultipleActions_Success2()
        {
            // ARRANGE
            var called = 0;

            var services = new ServiceCollection()
                .AddSingleton<ITestInitService, TestInitService>()
                .AddSingleton<ITestInitService2, TestInitService2>();

            services
                .AddAsyncServiceInitialization()
                .AddInitAction<ITestInitService>(
                    async (service, ct) =>
                    {
                        await service.Init(ct);
                        Interlocked.Increment(ref called);
                    });

            services
                .AddAsyncServiceInitialization()
                .AddInitAction<ITestInitService2>(
                    async (service, ct) =>
                    {
                        await service.Init(ct);
                        Interlocked.Increment(ref called);
                    });

            var serviceProvider = services.BuildServiceProvider();

            // ACT
            await serviceProvider.ExecuteInitActionsAsync();

            // ASSERT
            Assert.AreEqual(2, called);
        }
    }
}
