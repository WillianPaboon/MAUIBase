using Microsoft.Extensions.DependencyInjection;
using SharedPresentation.Classes.Interfaces;
using TinyMvvm;

namespace SharedPresentation.Pages
{
    /// <summary>
    /// BasePage class that inherits from TinyView.
    /// </summary>
    /// <typeparam name="T"> ViewModel</typeparam>
    public abstract class BasePage<T> : TinyView, INavigablePage
    {
        ///<inheritdoc/>
        public object? Parameters { get; set; }

        private IServiceProvider? _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasePage{T}"/> class.
        /// </summary>
        protected BasePage()
        {
        }

        ///<inheritdoc/>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _serviceProvider = Application.Current?.Handler?.MauiContext?.Services;

            if (_serviceProvider == null)
                throw new InvalidOperationException("ServiceProvider is not available.");
            
            
            BaseViewModel? viewModel = _serviceProvider.GetService(typeof(T)) as BaseViewModel;
            if(viewModel != null)
            {
                viewModel.NavigationService = Navigation;
                viewModel.Parameters = Parameters;
                BindingContext = viewModel;
            }
        }
    }
}
