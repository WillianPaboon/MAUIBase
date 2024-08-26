using BaseMAUI.Classes.Extensions;

namespace BaseMAUI.Settings
{
    public static class RegisterAssemblies
    {
        public static void RegisterServices(MauiAppBuilder builder)
        {
            builder.RegisterServicesFromAssembly(typeof(Domain.Services.ILoginService).Assembly);
            builder.RegisterServicesFromAssembly(typeof(Infrastructure.Repositories.WebService.ILoginWebService).Assembly);
        }
    }
}
