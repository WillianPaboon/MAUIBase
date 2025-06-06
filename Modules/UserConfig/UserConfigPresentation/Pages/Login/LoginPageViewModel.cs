using CommonShared.Exceptions;
using CommunityToolkit.Mvvm.Input;
using Domain.Models.ValueObjects.Primitives;
using SharedPresentation.Classes.Extensions;
using SharedPresentation.Classes.Helpers;
using SharedPresentation.Pages;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using TinyMvvm;
using UserConfigDomain.Services;
using UserConfigDomain.UseCases;

namespace UserConfigPresentation.Pages.Login
{
    public class LoginPageViewModel: BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        private readonly ILoginUseCase _login;

        public LoginPageViewModel(ILoginUseCase login)
        {
            _login = login;
        }

        public ICommand LoginCommand => new Command(LogginCommandImplementation);

        private async void LogginCommandImplementation(object obj)
        {
            IsBusy = true;
            Result result = await _login.Execute(Email, Password).ConfigureAwait(true);

            if(result.IsSuccess)
            {
                NavigationService.SetRootPage("Home");
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
