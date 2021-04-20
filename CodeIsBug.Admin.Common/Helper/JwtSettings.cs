using Newtonsoft.Json;
namespace CodeIsBug.Admin.Common.Helper
{
    public class JwtSettings
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }
    }
}
