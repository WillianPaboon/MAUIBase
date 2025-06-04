using BaseMAUI.Pages;
using BaseMAUI.Pages.Landing;
using SharedPresentation.Classes.Dependencies;
using SharedPresentation.Pages;
using TinyMvvm;

namespace BaseMAUI
{
    /// <summary>
    /// Represents the main application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider to resolve dependencies.</param>
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            MainPage = serviceProvider.GetRequiredService<LandingPage>();
        }
    }
}
