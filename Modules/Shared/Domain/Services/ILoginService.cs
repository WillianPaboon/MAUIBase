using Infrastructure.Repositories.WebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ILoginService
    {
        void Login();
    }

    public class LoginService : ILoginService
    {
        private ILoginWebService _loginService;

        public LoginService(ILoginWebService loginService)
        {
            _loginService = loginService;
        }

        public void Login()
        {
            _loginService.Login();
        }
    }
}
