using System.Linq;
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
    }
}
