using BaseMAUI.Classes.Extensions;
using CommonShared.Constants;
using CommunityToolkit.Maui;
using Infrastructure.Repositories.WebService._base;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TinyMvvm;
using UserConfigInfrastructure.Repositories;

namespace BaseMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseTinyMvvm()
                .UseMauiCommunityToolkit()
                .RegisterPageAndViewmodelWithConvention()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUGPROD || DEBUGQA
            builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient("loginb2c", client =>
            {
                client.BaseAddress = new Uri(GlobalConfig.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            }).ConfigurePrimaryHttpMessageHandler(() => handler);

            return builder.Build();
        }

        static HttpClientHandler handler => new()
        {
            UseProxy = true,
            Proxy = WebRequest.DefaultWebProxy,
            ServerCertificateCustomValidationCallback = validate
        };

        /// <summary>
        /// Set logic to Validate the server certificate for HTTPS requests.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool validate(HttpRequestMessage message, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
