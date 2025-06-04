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
        /// <summary>
        /// Registra servicios y repositorios en el contenedor de dependencias de MAUI a partir de un ensamblado especificado.
        /// </summary>
        /// <param name="mauiAppBuilder">El constructor de la aplicación MAUI.</param>
        /// <param name="assembly">El ensamblado del cual se obtendrán los tipos para registrar.</param>
        /// <returns>El constructor de la aplicación MAUI con los servicios registrados.</returns>
        /// <remarks>
        /// Este método busca en el ensamblado especificado todos los tipos cuyos nombres terminan en "Service" o "Repository".
        /// Luego, intenta encontrar una interfaz correspondiente cuyo nombre sea el mismo que el tipo, pero con un prefijo "I".
        /// Si encuentra una interfaz correspondiente, registra el tipo como una implementación singleton de esa interfaz en el contenedor de dependencias de MAUI.
        /// </remarks>
        public static MauiAppBuilder RegisterServicesFromAssembly(this MauiAppBuilder mauiAppBuilder, Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            Dictionary<string,Type> allServices = types.Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository")).ToDictionary(t => t.Name, t => t);

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
