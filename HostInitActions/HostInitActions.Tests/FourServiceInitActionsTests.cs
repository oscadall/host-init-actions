using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class FourServiceInitActionsTests
    {
        [Test]
        public async Task TwoServiceInitialization_OneInitAction()
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
                        .AddSingleton<ITestInitService4, TestInitService4>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2, ITestInitService3, ITestInitService4>(
                            async (service1, service2, service3, service4, ct) =>
                            {
                                await service1.Init(ct);
                                await service2.Init(ct);
                                await service3.Init(ct);
                                await service4.Init(ct);
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

            await host.StopAsync();
        }

        [Test]
        public async Task TwoServiceInitialization_OneInitAction_NoneCancellationToken()
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
                        .AddSingleton<ITestInitService4, TestInitService4>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2, ITestInitService3, ITestInitService4>(
                            async (service1, service2, service3, service4) =>
                            {
                                await service1.Init(default);
                                await service2.Init(default);
                                await service3.Init(default);
                                await service4.Init(default);
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

            await host.StopAsync();
        }
    }
}
