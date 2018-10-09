using Newtonsoft.Json;
using System;

namespace oAuth2JwtProfile
{
    public class AuthResult
    {
        [JsonProperty(PropertyName = "access_token")]
        public String AccessToken { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public String TokenType { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public String ExpiresIn { get; set; }
    }
}
