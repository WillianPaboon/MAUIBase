using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseMAUI.Classes.Extensions
{
    public static class RegisterRepositoryAndServiceByEntityExtension
    {
        public static MauiAppBuilder RegisterRepositoryAndServiceByEntity(this MauiAppBuilder mauiAppBuilder, Assembly servicesAssembly, Assembly repositoriesAssembly)
        {
            var serviceTypes = servicesAssembly.GetTypes();
            var repositoryTypes = repositoriesAssembly.GetTypes();

            foreach (var serviceType in serviceTypes)
            {
                if (IsService(serviceType))
                {
                    RegisterService(mauiAppBuilder.Services, serviceType);
                }
            }

            foreach (var repositoryType in repositoryTypes)
            {
                if (IsRepository(repositoryType))
                {
                    RegisterRepository(mauiAppBuilder.Services, repositoryType);
                }
            }

            return mauiAppBuilder;
        }

        private static bool IsService(Type type)
        {
            return type.Name.EndsWith("Service") && !type.IsAbstract && !type.IsInterface;
        }

        private static bool IsRepository(Type type)
        {
            return type.Name.EndsWith("Repository") && !type.IsAbstract && !type.IsInterface;
        }

        private static void RegisterService(IServiceCollection services, Type serviceType)
        {
            services.AddSingleton(serviceType);
        }

        private static void RegisterRepository(IServiceCollection services, Type repositoryType)
        {
            var entityType = repositoryType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>))?.GetGenericArguments().FirstOrDefault();
            if (entityType != null)
            {
                services.AddSingleton(typeof(IBaseRepository<>).MakeGenericType(entityType), repositoryType);
            }
        }
    }
}
