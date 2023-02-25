using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace HostInitActions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Performs all initialization actions, if any are registered. The intended use case is in applications or tests where no Host is used
        /// but only a ServiceCollection container to provide dependency injection. This extension method allows to explicitly call
        /// registered initialization actions that would be called automatically in the Host's environment.
        /// </summary>
        /// <param name="serviceProvider">Service provider with registered init actions.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task that's represents evaluation of all init actions.</returns>
        public static async Task ExecuteInitActionsAsync(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            var initService = serviceProvider.GetService<InitExecutionService>();

            if (initService != null)
            {
                await initService.ExecuteInitActionsAsync(cancellationToken);
            }
        }
    }
}
