using AutoMapper;
using BaseMAUI.Settings;
using BaseMAUI.Settings.Mapper;
using BaseMAUI.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TinyMvvm;

namespace BaseMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            IMapper mapper = AutoMapperConfig.Initialize();
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseTinyMvvm()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG || DEBUGQA
    		builder.Logging.AddDebug();
#endif

            RegisterAssemblies.RegisterServices(builder);
            builder.Services.AddTransient<AppShell, AppShellViewModel>();
            builder.Services.AddTransient<MainPage, MainPageViewModel>();
            builder.Services.AddSingleton(mapper);

            return builder.Build();
        }

    }
}
