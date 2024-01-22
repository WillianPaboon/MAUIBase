using AutoMapper;
using BaseMAUI.Settings.Mapper;
using BaseMAUI.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TinyMvvm;

namespace BaseMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            IMapper mapper = AutoMapperConfig.Initialize();
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseTinyMvvm()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddSingleton(mapper);

            return builder.Build();
        }
    }
}
