using System.Collections.ObjectModel;
using System.Reflection;

namespace BaseMAUI.Classes.Helpers
{
    /// <summary>
    /// Helper class for retrieving assemblies related to different application layers.
    /// </summary>
    public static class AssembliesHelper
    {
        /// <summary>
        /// Retrieves all domain-related assemblies.
        /// </summary>
        /// <returns>A read-only collection of domain-related assemblies.</returns>
        public static ReadOnlyCollection<Assembly> GetsAllDomainAssemblies()
        {
            var assemblies = new List<Assembly>
                {
                    typeof(Domain.Domain).Assembly,
                    typeof(UserConfigDomain.UserConfigDomain).Assembly,
                };

#if BuyMod1
            assemblies.Add(typeof(BuyDomain.BuyDomain).Assembly);
#endif
#if SellMod1
            assemblies.Add(typeof(SellDomain.SellDomain).Assembly);
#endif
            return assemblies.AsReadOnly();
        }

        /// <summary>
        /// Retrieves all infrastructure-related assemblies.
        /// </summary>
        /// <returns>A read-only collection of infrastructure-related assemblies.</returns>
        public static ReadOnlyCollection<Assembly> GetsAllInfraAssemblies()
        {
            var assemblies = new List<Assembly>
                {
                    typeof(Infrastructure.Infrastructure).Assembly,
                    typeof(UserConfigInfrastructure.UserConfigInfrastructure).Assembly,
                };

#if BuyMod1
            // Infrastructure assemblies for Buy module
#endif
#if SellMod1
            // Infrastructure assemblies for Sell module
#endif
            return assemblies.AsReadOnly();
        }

        /// <summary>
        /// Retrieves all presentation-related assemblies.
        /// </summary>
        /// <returns>A read-only collection of presentation-related assemblies.</returns>
        public static ReadOnlyCollection<Assembly> GetsAllPresentationAssemblies()
        {
            var assemblies = new List<Assembly>
                {
                    Assembly.GetExecutingAssembly(),
                    typeof(SharedPresentation.SharedPresentation).Assembly,
                    typeof(UserConfigPresentation.UserConfigPresentation).Assembly,
                };

#if BuyMod1
            // Add Buy module assemblies
#endif
#if SellMod1
            // Add Sell module assemblies
#endif
            return assemblies.AsReadOnly();
        }
    }
}
