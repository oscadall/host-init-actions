using System.Linq;
using System.Threading.Tasks;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    public class StageSingleServiceInitActionTests
    {
        private const string TestStageName1 = "TestStage1";
        private const string TestStageName2 = "TestStage2";
        private const string TestStageName3 = "TestStage3";
        private const string TestStageName4 = "TestStage4";

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
                        .GetOrAddStage(TestStageName1)
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
                        .GetOrAddStage(TestStageName1)
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

                    var initActionCollection = services.AddAsyncServiceInitialization();

                    initActionCollection
                        .GetOrAddStage(TestStageName1)
                        .AddInitAction<ITestInitService>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName2)
                        .AddInitAction<ITestInitService2>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName3)
                        .AddInitAction<ITestInitService3>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName4)
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
        public async Task FourInitActions_RegisteredIntoPredefinedStages_ExecutedInCorrectOrder()
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

                    var initActionCollection = services.AddAsyncServiceInitialization();

                    initActionCollection.GetOrAddStage(TestStageName1);
                    initActionCollection.GetOrAddStage(TestStageName2);
                    initActionCollection.GetOrAddStage(TestStageName3);
                    initActionCollection.GetOrAddStage(TestStageName4);

                    initActionCollection
                        .GetOrAddStage(TestStageName4)
                        .AddInitAction<ITestInitService4>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName2)
                        .AddInitAction<ITestInitService2>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName3)
                        .AddInitAction<ITestInitService3>(
                            async (service, ct) => await service.Init(ct));

                    initActionCollection
                        .GetOrAddStage(TestStageName1)
                        .AddInitAction<ITestInitService>(
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
        public async Task TwoInitActions_RegisteredIntoPredefinedStages_ExecutedInCorrectOrder()
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

                    var initActionCollection = services.AddAsyncServiceInitialization();

                    initActionCollection.GetOrAddStage(TestStageName1);
                    initActionCollection.GetOrAddStage(TestStageName2);

                    initActionCollection
                        .GetOrAddStage(TestStageName2)
                        .AddInitAction<ITestInitService3, ITestInitService4>(
                            async (service3, service4, ct) =>
                            {
                                await service3.Init(ct);
                                await service4.Init(ct);
                            });

                    initActionCollection
                        .GetOrAddStage(TestStageName1)
                        .AddInitAction<ITestInitService, ITestInitService2>(
                            async (service1, service2, ct) =>
                            {
                                await service1.Init(ct);
                                await service2.Init(ct);
                            });
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
        public async Task RegisterInitAction_AllTypes()
        {
            bool success = false;

            // ARRANGE
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services
                        .AddScoped<StageTestService1>()
                        .AddScoped<StageTestService2>()
                        .AddScoped<StageTestService3>()
                        .AddScoped<StageTestService4>()
                        .AddScoped<StageTestService5>();

                    var initActionCollection = services.AddAsyncServiceInitialization();

                    initActionCollection
                        .GetOrAddStage(TestStageName1)
                        .AddInitAction<StageTestService1>(
                            (s, ct) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2>(
                            (s, s2, ct) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3>(
                            (s, s2, s3, ct) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3, StageTestService4>(
                            (s, s2, s3, s4, ct) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3, StageTestService4,
                            StageTestService5>(
                            (s, s2, s3, s4, s5, ct) => Task.CompletedTask)
                        .AddInitAction<StageTestService1>(
                            (s) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2>(
                            (s, s2) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3>(
                            (s, s2, s3) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3, StageTestService4>(
                            (s, s2, s3, s4) => Task.CompletedTask)
                        .AddInitAction<StageTestService1, StageTestService2, StageTestService3, StageTestService4,
                            StageTestService5>(
                            (s, s2, s3, s4, s5) => Task.CompletedTask);

                    initActionCollection
                        .AddInitAction<StageTestService1>(s =>
                        {
                            success = true;
                            return Task.CompletedTask;
                        });


                })
                .Build();

            // ACT
            await host.StartAsync();

            // ASSERT
            Assert.True(success);

            await host.StopAsync();
        }
    }
}