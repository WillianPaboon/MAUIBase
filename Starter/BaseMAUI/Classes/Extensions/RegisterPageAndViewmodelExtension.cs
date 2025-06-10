
using BaseMAUI.Classes.Helpers;
using SharedPresentation.Classes.Extensions;
using System.Collections.ObjectModel;
using System.Reflection;
using UserConfigPresentation.Pages.Login;

namespace BaseMAUI.Classes.Extensions
{
    /// <summary>
    /// Extension methods for registering pages and view models with conventions.
    /// </summary>
    internal static class RegisterPageAndViewmodelExtension
    {

        /// <summary>
        /// Registers pages and view models in the specified assembly with the MauiAppBuilder using naming conventions.
        /// </summary>
        /// <param name="mauiAppBuilder">The MauiAppBuilder to register services with.</param>
        /// <returns>The MauiAppBuilder with the registered services.</returns>
        public static MauiAppBuilder RegisterPageAndViewmodelWithConvention(this MauiAppBuilder mauiAppBuilder)
        {
            ReadOnlyCollection<Assembly> assemblies = AssembliesHelper.GetsAllPresentationAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                RegisterPagesByAssembly(mauiAppBuilder, assembly);
            }

            return mauiAppBuilder;
        }

        private static void RegisterPagesByAssembly(MauiAppBuilder mauiAppBuilder, Assembly assembly)
        {
            var pageTypes = assembly.GetTypes()
                .Where(type => type.Name.EndsWith("Page", StringComparison.CurrentCulture) && !type.IsAbstract)
                .ToList();

            foreach (Type pageType in pageTypes)
            {
                mauiAppBuilder.Services.AddTransient(pageType);

                string viewModelTypeName = pageType.Name + "ViewModel";
                Type? viewModelType = assembly.GetTypes().FirstOrDefault(type => type.Name == viewModelTypeName);

                if (viewModelType != null)
                    mauiAppBuilder.Services.AddTransient(viewModelType);

                NavigationExtension.RegisterPagesRoute(pageType.Name, pageType, viewModelType);
            }
        }


    }
}
