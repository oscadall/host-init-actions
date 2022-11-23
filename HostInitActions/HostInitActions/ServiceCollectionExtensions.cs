using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostInitActions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a service that performs initialization actions before the application starts.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Collection for registering initialization actions.</returns>
        public static IInitActionCollection AddAsyncServiceInitialization(this IServiceCollection services)
        {
            var initServiceRegistered =
                services.Any(descriptor => descriptor.ImplementationType == typeof(InitHostedService));

            if (!initServiceRegistered)
            {
                services.AddHostedService<InitHostedService>();
            }

            return new InitServiceCollection(services);
        }
    }
}
