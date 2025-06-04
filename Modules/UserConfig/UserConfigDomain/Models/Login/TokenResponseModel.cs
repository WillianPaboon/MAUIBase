
namespace UserConfigDomain.Models.Login
{
    /// <summary>
    /// Represents the response model for a token.
    /// </summary>
    public class TokenResponseModel
    {
        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or sets the expiration time of the token.
        /// </summary>
        public string? Expires { get; set; }

        /// <summary>
        /// Gets or sets the extended expiration time of the token.
        /// </summary>
        public string? ExtExpires { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string? AccessToken { get; set; }
    }
}
