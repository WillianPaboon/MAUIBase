namespace CommonShared.Constants
{
    /// <summary>  
    /// Contains global configuration constants for the application.  
    /// </summary>  
    public static class GlobalConfig
    {
#if DEBUGQA || RELEASEQA
        /// <summary>  
        /// The base URL used for QA environments.  
        /// </summary>  
        public const string BaseUrl = "https://argosau.b2clogin.com/";
#else
           /// <summary>  
           /// The base URL used for production environments.
           /// </summary>  
           public const string BaseUrl = "https://api.yourdomain.com";  
#endif
    }
}
