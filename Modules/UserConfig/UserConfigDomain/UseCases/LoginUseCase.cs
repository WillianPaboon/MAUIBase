using CommonShared.Exceptions;
using Domain.Models.ValueObjects;
using Domain.Models.ValueObjects.Primitives;
using System.Text.Json;
using UserConfigDomain.Services;

namespace UserConfigDomain.UseCases
{
    /// <summary>
    /// Representa un caso de uso para manejar el inicio de sesión.
    /// </summary>
    public interface ILoginUseCase
    {
        /// <summary>
        /// Ejecuta el caso de uso de inicio de sesión con el correo electrónico y la contraseña proporcionados.
        /// </summary>
        /// <param name="email">El correo electrónico del usuario.</param>
        /// <param name="password">La contraseña del usuario.</param>
        Task<Result> Execute(string email, string password);
    }

    internal class LoginUseCase : ILoginUseCase
    {
        private readonly ILoginService _loginService;

        public LoginUseCase(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<Result> Execute(string email, string password)
        {
            try
            {
                UserCredentials credentials = new(email, password);
                var token = await _loginService.LoginAsync(credentials).ConfigureAwait(true);

                //save token

                return Result.Success();
            }
            catch (WebServiceErrorException ex) when (ex.Message.Contains("access_denied", StringComparison.OrdinalIgnoreCase))
            {
                //Register in crashlytics
                string errorMessage = JsonSerializer.Deserialize<Dictionary<string, string>>(ex.Message)?["error_description"] ?? "wrong user information";
                return Result.Failure(errorMessage);
            }
            catch (Exception ex)
            {
                return Result.Failure(string.IsNullOrEmpty(ex.Message)? "Unknown error": ex.Message);
            }
        }
    }
}
