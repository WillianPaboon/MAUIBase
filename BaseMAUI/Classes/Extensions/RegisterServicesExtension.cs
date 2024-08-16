using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseMAUI.Classes.Extensions
{
    public static class RegisterServicesExtension
    {
        public static MauiAppBuilder RegisterServicesFromAssembly(this MauiAppBuilder mauiAppBuilder, Assembly assembly)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                if (type.IsClass)
                {
                    var interfaceName = "I" + type.Name;

                    var interfaceType = types.FirstOrDefault(t =>
                        t.IsInterface && t.Name == interfaceName);

                    if (interfaceType != null)
                    {
                        // Verifica si el nombre de la clase termina en "Service" o "Repository"
                        if (type.Name.EndsWith("Service") || type.Name.EndsWith("Repository"))
                        {
                            mauiAppBuilder.Services.AddSingleton(interfaceType, type);
                        }
                    }
                }
            }

            return mauiAppBuilder;
        }
    }
}
