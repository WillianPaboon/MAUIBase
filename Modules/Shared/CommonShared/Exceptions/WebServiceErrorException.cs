using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonShared.Exceptions
{
    public class WebServiceErrorException: Exception
    {
        public WebServiceErrorException()
        {
        }

        public WebServiceErrorException(string message) : base(message)
        {
        }

        public WebServiceErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
