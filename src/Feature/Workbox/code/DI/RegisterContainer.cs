namespace Feature.Workbox.DI
{
    using Feature.Workbox.Controllers;
    using Feature.Workbox.Interfaces;
    using Feature.Workbox.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;

    /// <summary>
    /// Registering the DI container
    /// </summary>
    /// <seealso cref="Sitecore.DependencyInjection.IServicesConfigurator"/>
    public class RegisterContainer : IServicesConfigurator
    {
        /// <summary>
        /// Configures the specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IWorkflowRepository, WorkflowRepository>();

            serviceCollection.AddTransient<WorkboxApiController>();
        }
    }
}