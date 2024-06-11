using System.Linq;
using HostInitActions.Tests.TestServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace HostInitActions.Tests
{
    internal class AddAsyncInitializationServiceTests
    {
        [Test]
        public void MultipleRegistrationsOfInitServices_OnlyOneRegistered()
        {
            var services = new ServiceCollection();

            services.AddAsyncServiceInitialization();
            services.AddAsyncServiceInitialization();
            services.AddAsyncServiceInitialization();

            var provider = services.BuildServiceProvider();

            Assert.AreEqual(1, provider.GetServices<IHostedService>().Count());
            Assert.AreEqual(1, provider.GetServices<InitExecutionService>().Count());
        }

        [Test]
        public void RegistrationsOfInitServicesAndKeyedServiceExists_NoException()
        {
            var services = new ServiceCollection();
            services.AddKeyedSingleton<StageTestService1>("test");

            Assert.DoesNotThrow(() => services.AddAsyncServiceInitialization());
        }

        [Test]
        public void RegistrationsOfInitServicesAndKeyedServiceExists_CorrectRegistration()
        {
            var services = new ServiceCollection();
            services.AddKeyedSingleton<StageTestService1>("test");

            services.AddAsyncServiceInitialization();

            var provider = services.BuildServiceProvider();

            Assert.NotNull(provider.GetService<IHostedService>());
            Assert.NotNull(provider.GetService<InitExecutionService>());
        }
    }
}
