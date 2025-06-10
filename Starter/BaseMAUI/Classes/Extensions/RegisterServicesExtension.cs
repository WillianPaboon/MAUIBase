using BaseMAUI.Classes.Helpers;
using System.Reflection;

namespace BaseMAUI.Classes.Extensions
{
    /// <summary>
    /// Provides extension methods for registering services in the MauiAppBuilder.
    /// </summary>
    internal static class RegisterServicesExtension
    {

        /// <summary>
        /// Registers services from various assemblies into the MauiAppBuilder.
        /// </summary>
        /// <param name="builder">The MauiAppBuilder to register services into.</param>
        /// <returns>The MauiAppBuilder with registered services.</returns>
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            List<Assembly> domainAssemblies = AssembliesHelper.GetsAllDomainAssemblies().ToList();
            domainAssemblies.AddRange(AssembliesHelper.GetsAllInfraAssemblies());

            foreach (Assembly assembly in domainAssemblies)
                RegisterServicesFromAssembly(builder, assembly);

            return builder;
        }

        private static MauiAppBuilder RegisterServicesFromAssembly(MauiAppBuilder mauiAppBuilder, Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            Dictionary<string, Type> allServices = types.Where(t => 
                                                               t.Name.EndsWith("Service",StringComparison.CurrentCulture) 
                                                               || t.Name.EndsWith("Repository", StringComparison.CurrentCulture) 
                                                               || t.Name.EndsWith("UseCase", StringComparison.CurrentCulture)).ToDictionary(t => t.Name, t => t);

            foreach (KeyValuePair<string, Type> implementation in allServices.Where(serv => serv.Value.IsClass))
            {
                string interfaceName = "I" + implementation.Key;
                if (allServices.TryGetValue(interfaceName, out Type? interfaceType))
                {
                    mauiAppBuilder.Services.AddSingleton(interfaceType, implementation.Value);
                }
            }

            return mauiAppBuilder;
        }
    }
}
