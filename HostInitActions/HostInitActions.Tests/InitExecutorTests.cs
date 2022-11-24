using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class InitExecutorTests
    {
        [Test]
        public async Task AddInitExecutor_NoParameters()
        {
            // ARRANGE
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>()
                        .AddSingleton<ITestInitService2, TestInitService2>()
                        .AddSingleton<ITestInitService3, TestInitService3>()
                        .AddSingleton<ITestInitService4, TestInitService4>()
                        .AddSingleton<ITestInitService5, TestInitService5>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService>(
                            async (service1) =>
                            {
                                await service1.Init(default);
                            })
                        .AddInitActionExecutor<TestAsyncInitExecutor>()
                        .AddInitAction<ITestInitService5>(
                            async (service5) =>
                            {
                                await service5.Init(default);
                            });
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var service1 = host.Services.GetRequiredService<ITestInitService>();
            Assert.True(service1.Initialized);

            var service2 = host.Services.GetRequiredService<ITestInitService2>();
            Assert.True(service2.Initialized);

            var service3 = host.Services.GetRequiredService<ITestInitService3>();
            Assert.True(service3.Initialized);

            var service4 = host.Services.GetRequiredService<ITestInitService4>();
            Assert.True(service4.Initialized);

            var service5 = host.Services.GetRequiredService<ITestInitService5>();
            Assert.True(service5.Initialized);

            await host.StopAsync();
        }

        [Test]
        public async Task AddInitExecutor_FactoryFunc()
        {
            // ARRANGE
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>()
                        .AddSingleton<ITestInitService2, TestInitService2>()
                        .AddSingleton<ITestInitService3, TestInitService3>()
                        .AddSingleton<ITestInitService4, TestInitService4>()
                        .AddSingleton<ITestInitService5, TestInitService5>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService>(
                            async (service1) =>
                            {
                                await service1.Init(default);
                            })
                        .AddInitActionExecutor(sp => new TestAsyncInitExecutor(
                            sp.GetRequiredService<ITestInitService2>(),
                            sp.GetRequiredService<ITestInitService3>(),
                            sp.GetRequiredService<ITestInitService4>()))
                        .AddInitAction<ITestInitService5>(
                            async (service5) =>
                            {
                                await service5.Init(default);
                            });
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var service1 = host.Services.GetRequiredService<ITestInitService>();
            Assert.True(service1.Initialized);

            var service2 = host.Services.GetRequiredService<ITestInitService2>();
            Assert.True(service2.Initialized);

            var service3 = host.Services.GetRequiredService<ITestInitService3>();
            Assert.True(service3.Initialized);

            var service4 = host.Services.GetRequiredService<ITestInitService4>();
            Assert.True(service4.Initialized);

            var service5 = host.Services.GetRequiredService<ITestInitService5>();
            Assert.True(service5.Initialized);

            await host.StopAsync();
        }


        [Test]
        public async Task AddInitExecutor_Instance()
        {
            // ARRANGE
            var service1 = new TestInitService();
            var service2 = new TestInitService2(service1);
            var service3 = new TestInitService3(service2);
            var service4 = new TestInitService4(service3);
            var service5 = new TestInitService5(service4);
            var executor = new TestAsyncInitExecutor(service2, service3, service4);

            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService>(service1)
                        .AddSingleton<ITestInitService2>(service2)
                        .AddSingleton<ITestInitService3>(service3)
                        .AddSingleton<ITestInitService4>(service4)
                        .AddSingleton<ITestInitService5>(service5);

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService>(
                            async s1 =>
                            {
                                await s1.Init(default);
                            })
                        .AddInitActionExecutor(executor)
                        .AddInitAction<ITestInitService5>(
                            async s5 =>
                            {
                                await s5.Init(default);
                            });
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            Assert.True(service1.Initialized);
            Assert.True(service2.Initialized);
            Assert.True(service3.Initialized);
            Assert.True(service4.Initialized);
            Assert.True(service5.Initialized);

            await host.StopAsync();
        }
    }
}
