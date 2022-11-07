using System.Linq;
using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    public class InitActionsTests
    {
        [Test]
        public async Task OneService_OneInitAction()
        {
            // ARRANGE
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService>(
                            async (service, ct) => await service.Init(ct));
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var service = host.Services.GetRequiredService<ITestInitService>();
            Assert.True(service.Initialized);

            await host.StopAsync();
        }

        [Test]
        public async Task OneService_OneInitAction_NoneCancellationToken()
        {
            // ARRANGE
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddSingleton<ITestInitService, TestInitService>();

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService>(
                            async (service) => await service.Init(default));
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var service = host.Services.GetRequiredService<ITestInitService>();
            Assert.True(service.Initialized);

            await host.StopAsync();
        }

        [Test]
        public async Task FourInitActions_ExecutedInCorrectOrder()
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
                        .AddInitAction<ITestInitService>(
                            async (service, ct) => await service.Init(ct))
                        .AddInitAction<ITestInitService2>(
                            async (service, ct) => await service.Init(ct))
                        .AddInitAction<ITestInitService3>(
                            async (service, ct) => await service.Init(ct))
                        .AddInitAction<ITestInitService4>(
                            async (service, ct) => await service.Init(ct));

                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var service = host.Services.GetRequiredService<ITestInitService4>();
            Assert.True(service.Initialized);

            await host.StopAsync();
        }

        [Test]
        public async Task MultipleInitServiceRegistration_RegisterOnlyOnce()
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
                        .AddInitAction<ITestInitService>(
                            async (service, ct) => await service.Init(ct));

                    services
                        .AddAsyncServiceInitialization()
                        .AddInitAction<ITestInitService2>(
                            async (service, ct) => await service.Init(ct));
                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            var initServices = host
                .Services
                .GetServices<IHostedService>()
                .OfType<InitHostedService>();
            Assert.AreEqual(1, initServices.Count());

            var service = host.Services.GetRequiredService<ITestInitService2>();
            Assert.True(service.Initialized);

            await host.StopAsync();
        }
    }
}