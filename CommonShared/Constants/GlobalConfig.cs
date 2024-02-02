using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonShared.Constants
{
    internal class GlobalConfig
    {

#if DEBUG || DEBUGQA || RELEASEQA
        public const string BaseUrl = "https://localhost:44300";
#else
        public const string BaseUrl = "https://api.yourdomain.com";
#endif
    }
}
