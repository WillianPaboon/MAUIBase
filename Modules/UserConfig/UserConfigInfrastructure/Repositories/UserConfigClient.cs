using Infrastructure.Repositories.WebService._base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserConfigInfrastructure.Repositories
{
    /// <summary>  
    /// Represents a client for interacting with the User Configuration API.  
    /// </summary>  
    public class UserConfigClient : ApiClient
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="UserConfigClient"/> class.  
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public UserConfigClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory?.CreateClient("loginb2c") ?? throw new ArgumentException("http client initialize error"))
        {
        }
    }
}
