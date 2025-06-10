
using BaseMAUI.Classes.Helpers;
using System.Collections.ObjectModel;
using System.Reflection;

namespace BaseMAUI.Classes.Extensions
{
    /// <summary>
    /// Provides extension methods for registering MediatR services in a Maui application.
    /// </summary>
    public static class RegisterMediatorExtension
    {
        /// <summary>
        /// Registers MediatR services with the specified <see cref="MauiAppBuilder"/>.
        /// </summary>
        /// <param name="builder">The <see cref="MauiAppBuilder"/> to configure.</param>
        /// <returns>The configured <see cref="MauiAppBuilder"/>.</returns>
        public static MauiAppBuilder RegisterMediator(this MauiAppBuilder builder)
        {
            builder.Services.AddMediatR(configuration);

            return builder;
        }

        /// <summary>
        /// Configures MediatR to register services from all domain assemblies.
        /// </summary>
        /// <param name="configuration">The MediatR service configuration.</param>
        private static void configuration(MediatRServiceConfiguration configuration)
        {
            ReadOnlyCollection<Assembly> assemblies = AssembliesHelper.GetsAllDomainAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                configuration.RegisterServicesFromAssembly(assembly);
            }
        }
    }
}
