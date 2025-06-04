using CommonShared.Exceptions;
using CommunityToolkit.Mvvm.Input;
using SharedPresentation.Classes.Helpers;
using SharedPresentation.Pages;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using TinyMvvm;
using UserConfigDomain.Services;

namespace UserConfigPresentation.Pages.Login
{
    public class LoginPageViewModel: BaseViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        private readonly ILoginService _loginService;

        public LoginPageViewModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ICommand LoginCommand => new Command(LogginCommandImplementation);

        private async void LogginCommandImplementation(object obj)
        {
            try
            {
                IsBusy = true;
                var token = await _loginService.LoginAsync(Email, Password).ConfigureAwait(true);
            }
            catch(WebServiceErrorException ex) when (ex.Message.Contains("access_denied", StringComparison.OrdinalIgnoreCase))
            {
                await ExceptionHelper.Handle(new UnauthorizedAccessException(JsonSerializer.Deserialize<Dictionary<string, string>>(ex.Message)?["error_description"]), 
                    new Dictionary<string, string>
                    {
                        { "Email", Email },
                        { "Password", Password }
                    }).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                await ExceptionHelper.Handle(ex, new Dictionary<string, string>
                {
                    { "Email", Email },
                    { "Password", Password }
                }).ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
