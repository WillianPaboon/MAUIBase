namespace BaseMAUI.Classes.Extensions
{
    /// <summary>
    /// Provides extension methods for registering services in the MauiAppBuilder.
    /// </summary>
    internal static class RegisterAssemblies
    {
        /// <summary>
        /// Registers services from various assemblies into the MauiAppBuilder.
        /// </summary>
        /// <param name="builder">The MauiAppBuilder to register services into.</param>
        /// <returns>The MauiAppBuilder with registered services.</returns>
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.RegisterServicesFromAssembly(typeof(UserConfigDomain.Services.ILoginService).Assembly);
            builder.RegisterServicesFromAssembly(typeof(UserConfigInfrastructure.Repositories.WebService.ILoginWebService).Assembly);

#if BuyMod1
            builder.RegisterServicesFromAssembly(typeof(BuyDomain.Services.IBuyService).Assembly);
#endif
#if SellMod1
            builder.RegisterServicesFromAssembly(typeof(SellDomain.Services.ISellService).Assembly);
#endif

            return builder;
        }
    }
}
