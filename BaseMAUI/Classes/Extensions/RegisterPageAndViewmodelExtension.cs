using System.Reflection;

namespace BaseMAUI.Classes.Extensions
{
    public static class RegisterPageAndViewmodelExtension
    {
        public static MauiAppBuilder RegisterPageAndViewmodelWithConvention(this MauiAppBuilder mauiAppBuilder, Assembly assembly)
        {
            var viewModelTypes = assembly.GetTypes()
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList();

            foreach (var viewModelType in viewModelTypes)
            {
                Type pageType = assembly.GetTypes()
                    .FirstOrDefault(type => type.Name == viewModelType.Name.Replace("ViewModel", ""));

                if (pageType != null)
                {
                    // Registra la página y el ViewModel
                    mauiAppBuilder.Services.AddTransient(pageType);
                    mauiAppBuilder.Services.AddTransient(viewModelType);

                }
            }

            return mauiAppBuilder;
        }
    }
}
