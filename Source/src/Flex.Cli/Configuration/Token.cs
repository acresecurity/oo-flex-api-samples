using Newtonsoft.Json;

namespace Flex.Configuration
{
    internal class Token
    {
        public string Authority { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public DateTime ExpiresAt { get; set; }

        public string RefreshToken { get; set; }

        [JsonIgnore]
        public bool IsValidAndNotExpiring => !string.IsNullOrEmpty(AccessToken) && ExpiresAt > DateTime.UtcNow.AddSeconds(30);

        [JsonIgnore]
        public bool IsValidAndIsExpired => !string.IsNullOrEmpty(AccessToken) && DateTime.UtcNow > ExpiresAt;
    }
}