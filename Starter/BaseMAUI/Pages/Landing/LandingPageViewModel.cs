using SharedPresentation.Classes.Extensions;
using SharedPresentation.Pages;
using System.Windows.Input;

namespace BaseMAUI.Pages.Landing;

/// <summary>
/// Represents the view model for the landing page.
/// </summary>
public class LandingPageViewModel : BaseViewModel
{

    /// <summary>
    /// Initializes a new instance of the <see cref="LandingPageViewModel"/> ViewModel.
    /// </summary>
    public LandingPageViewModel()
    {
    }

    public override Task Initialize()
    {
        NavigationService.SetRootPage("LoginPage");
        return base.Initialize();
    }

    public override Task OnAppearing()
    {

        NavigationService.SetRootPage("LoginPage");
        return base.OnAppearing();

    }
}
