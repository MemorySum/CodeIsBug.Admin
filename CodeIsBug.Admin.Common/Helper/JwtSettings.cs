using Newtonsoft.Json;

namespace CodeIsBug.Admin.Common.Helper
{
    public class JwtSettings
    {
        /// <summary>
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("audience")]
        public string Audience { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }
    }
}