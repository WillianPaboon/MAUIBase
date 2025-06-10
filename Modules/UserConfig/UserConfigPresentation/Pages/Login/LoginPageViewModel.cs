using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.ValueObjects.Primitives;
using SharedPresentation.Classes.Extensions;
using SharedPresentation.Pages;
using System.Windows.Input;
using UserConfigDomain.UseCases;

namespace UserConfigPresentation.Pages.Login
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        private readonly ILoginUseCase _login;

        [ObservableProperty]
        private string email = "argosonesupport@argos.com.co";

        [ObservableProperty]
        private string password = "Cambiar.200";


        public LoginPageViewModel(ILoginUseCase login)
        {
            _login = login;
        }


        [RelayCommand]
        private async void Login(object obj)
        {
            IsBusy = true;
            Result result = await _login.Execute(Email, Password).ConfigureAwait(true);

            if(result.IsSuccess)
            {
                NavigationService.SetRootPage("MainHomePage");
            }
            else
            {
                if (Application.Current?.MainPage == null)
                    return;

                await Application.Current.MainPage.DisplayAlert("Alert", result.Message, "Okay").ConfigureAwait(true);

            }

            IsBusy = false;

        }
    }
}
