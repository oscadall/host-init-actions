using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
            // It must be ensured that the singleton service is registered only once
            services.TryAddSingleton<InitExecutionService>();

            // Multiple invocations of "AddHostedService" with the same type will only perform one registration
            services.AddHostedService<InitHostedService>();

            return new InitServiceCollection(services);
        }
    }
}
