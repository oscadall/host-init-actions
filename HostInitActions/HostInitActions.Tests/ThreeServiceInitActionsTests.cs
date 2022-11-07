using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class ThreeServiceInitActionsTests
    {
        [Test]
        public async Task TwoServiceInitialization_OneInitAction()
        {
            // ARRANGE
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>()
                        .AddSingleton<ITestInitService2, TestInitService2>()
                        .AddSingleton<ITestInitService3, TestInitService3>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2, ITestInitService3>(
                            async (service, service2, service3, ct) =>
                            {
                                await service.Init(ct);
                                await service2.Init(ct);
                                await service3.Init(ct);
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

            // CLEANUP
            await host.StopAsync();
            host.Dispose();
        }

        [Test]
        public async Task TwoServiceInitialization_OneInitAction_NoneCancellationToken()
        {
            // ARRANGE
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>()
                        .AddSingleton<ITestInitService2, TestInitService2>()
                        .AddSingleton<ITestInitService3, TestInitService3>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2, ITestInitService3>(
                            async (service, service2, service3) =>
                            {
                                await service.Init(default);
                                await service2.Init(default);
                                await service3.Init(default);
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

            // CLEANUP
            await host.StopAsync();
            host.Dispose();
        }
    }
}
