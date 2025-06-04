using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedPresentation.Classes.Helpers
{
    public static class ExceptionHelper
    {
        /// <summary>  
        /// Manage an error depending on its type.  
        /// </summary>  
        /// <param name="exception"> Exception occurred </param>  
        /// <param name="aditionalProperties"> Properties that show in Appcenter error report </param>  
        /// <returns></returns>  
        public async static Task Handle(Exception exception, Dictionary<string, string>? aditionalProperties = null)
        {
            string exceptionName = exception.GetType().Name;
            string exceptionMessage = exception.Message;

            Dictionary<string, string> properties = aditionalProperties ?? new();

            if (exception.InnerException != null)
            {
                properties.Add("innerEx", exception.InnerException.Message);
            }

            if (Application.Current?.MainPage == null)
                return;

            switch (exceptionName)
            {
                case "UnauthorizedAccessException":
                    await Application.Current.MainPage.DisplayAlert("Alert", "User name or password are invalid", "Okay").ConfigureAwait(true);
                    break;
                case "NoInternetException":
                case "TimeOutException":
                case "InternetProblemException":
                    await Application.Current.MainPage.DisplayAlert("Alert", exception.Message, "Okay").ConfigureAwait(true);
                    break;
                default:
                    await Application.Current.MainPage.DisplayAlert("Alert", "Default Alert Message.", "Okay").ConfigureAwait(true);
                    break;
            }
        }
    }
}
