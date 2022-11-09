using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class TwoServiceInitActionsTests
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
                        .AddSingleton<ITestInitService2, TestInitService2>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2>(
                            async (service, service2, ct) =>
                            {
                                await service.Init(ct);
                                await service2.Init(ct);
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

            // CLEANUP
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
                        .AddSingleton<ITestInitService2, TestInitService2>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService, ITestInitService2>(
                            async (service, service2) =>
                            {
                                await service.Init(default);
                                await service2.Init(default);
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

            // CLEANUP
            await host.StopAsync();
        }
    }
}
