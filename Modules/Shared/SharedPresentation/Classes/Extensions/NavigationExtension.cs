using SharedPresentation.Pages;

namespace SharedPresentation.Classes.Extensions
{
    public static class NavigationExtension
    {
        private static Dictionary<string, Type> Pages { get; set; } = new();
        private static Dictionary<string, Type> ViewModels { get; set; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigation"></param>
        /// <param name="route"></param>
        /// <param name="parameter"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static async void PushPageAsync(this INavigation navigation, string route, object? parameter = null)
        {
            if (Application.Current is null || Application.Current?.MainPage is null)
                throw new InvalidOperationException("MainPage is not set.");

            if (navigation.NavigationStack.Count == 0)
                throw new InvalidOperationException("NavigationPage is not set.");

            if (Pages.TryGetValue(route, out Type? pageType))
            {
                object? page = Application.Current?.Handler?.MauiContext?.Services.GetService(pageType);

                if (page is null || page is not Page)
                    throw new InvalidOperationException($"The resolved page for route '{route}' is not a valid Page.");

                await navigation.PushAsync(page as Page).ConfigureAwait(true);
                return;
            }

            throw new ArgumentException($"Route '{route}' not registered.");
        }

        public static void SetRootPage(this INavigation navigation, string route, object? parameter = null)
        {
            if (Application.Current is null || Application.Current?.MainPage is null)
                throw new InvalidOperationException("MainPage is not set.");

            if (Pages.TryGetValue(route, out Type? pageType))
            {
                object? page = Application.Current.Handler.MauiContext?.Services.GetService(pageType);

                if (page is null || page is not Page)
                    throw new InvalidOperationException($"The resolved page for route '{route}' is not a valid Page.");

                Application.Current.MainPage = page as Page;
                return;
            }

            throw new ArgumentException($"Route '{route}' not registered.");
        }

        /// <summary>
        /// Registers a page route with its corresponding page type.
        /// </summary>
        /// <param name="route"></param>
        /// <param name="pageType"></param>
        /// <param name="viewModelType"></param>
        public static void RegisterPagesRoute(string route, Type pageType, Type? viewModelType)
        {
            if (viewModelType != null && typeof(BasePage<>).MakeGenericType(viewModelType).IsAssignableFrom(pageType))
                ViewModels.TryAdd(route, viewModelType);

            Pages.TryAdd(route, pageType);
        }
    }
}
