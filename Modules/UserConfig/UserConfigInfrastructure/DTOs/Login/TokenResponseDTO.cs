using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserConfigInfrastructure.Models.Login
{
    /// <summary>  
    /// Represents the response containing token information.  
    /// </summary>  
    public class TokenResponseDTO
    {
        /// <summary>  
        /// Gets or sets the type of the token.  
        /// </summary>  
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        /// <summary>  
        /// Gets or sets the expiration time of the token.  
        /// </summary>  
        [JsonPropertyName("expires_in")]
        public string? Expires { get; set; }

        /// <summary>  
        /// Gets or sets the extended expiration time of the token.  
        /// </summary>  
        [JsonPropertyName("ext_expires_in")]
        public string? ExtExpires { get; set; }

        /// <summary>  
        /// Gets or sets the access token.  
        /// </summary>  
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
    }
}
