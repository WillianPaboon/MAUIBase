using SharedPresentation.Classes.Interfaces;
using TinyMvvm;

namespace SharedPresentation.Pages
{
    /// <summary>
    /// Base ViewModel class providing common functionality for all ViewModels.
    /// </summary>
    public abstract class BaseViewModel : TinyViewModel, INavigablePage
    {
        ///<inheritdoc/>
        public object? Parameters { get; set; }

        /// <summary>
        /// Gets the object responsible for handling stack-based navigation
        /// </summary>
        public Microsoft.Maui.Controls.INavigation NavigationService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider to use for dependency injection.</param>
        protected BaseViewModel(IServiceProvider serviceProvider)
        {
            // Initialize Global dependencies
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        protected BaseViewModel()
        {
            var indicator = new ActivityIndicator();
            indicator.BindingContext = this;
            indicator.Color = Colors.Red;
            indicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(IsBusy));
            Application.Current?.MainPage?.InsertLogicalChild(1,indicator);

        }

    }
}
