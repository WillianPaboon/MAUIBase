using SharedPresentation.Pages;

namespace SharedPresentation.Classes.Dependencies
{

    public class NavigationService
    {
        private static Dictionary<string, Type> Pages { get; set; } = new();
        private static Dictionary<string, Type> ViewModels { get; set; } = new();

        private readonly IServiceProvider _serviceProvider;
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        public Task NavigateToAsync(string route, object? parameter = null)
        {
            if (Application.Current?.MainPage is null)
                throw new InvalidOperationException("MainPage is not set.");

            if (Pages.TryGetValue(route, out Type? pageType))
            {
                var page = _serviceProvider.GetService(pageType);

                if (page is not Page)
                    throw new InvalidOperationException($"The resolved page for route '{route}' is not a valid Page.");

                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    navigationPage.Navigation.PushAsync(page as Page);
                }
                else
                {
                    Application.Current.MainPage = page as Page;
                }

                return Task.CompletedTask;
            }

            throw new ArgumentException($"Route '{route}' not registered.");
        }

        /// <inheritdoc/>
        public async Task<Page> GoBackAsync()
        {
            if (Application.Current?.MainPage?.Navigation != null)
            {
                return await Application.Current.MainPage.Navigation.PopAsync().ConfigureAwait(true);
            }

            throw new InvalidOperationException("Navigation stack is not available.");
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

        /// <summary>
        /// Gets the page type associated with a specified route.
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Type GetPageRoute(string route)
        {
            if (Pages.TryGetValue(route, out Type? pageType))
                return pageType;

            throw new ArgumentException($"Route '{route}' not registered.");
        }
    }

}
