using Infrastructure.Repositories.WebService._base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.WebService
{
    public interface ILoginWebService
    {
        public void Login();
    }

    public class LoginWebService : ILoginWebService
    {
        public LoginWebService(string baseUrl, TimeSpan timeout = default)
        {
        }

        public void Login()
        {
            throw new NotImplementedException();
        }
    }

}
