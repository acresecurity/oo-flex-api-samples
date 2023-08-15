using System.Security.Cryptography;
using System.Text;

// ReSharper disable once CheckNamespace
namespace MQTTnet.Client
{
    internal static class MqttClientOptionsBuilderExtensions
    {
        public static MqttClientOptionsBuilder WithCodeCredentials(this MqttClientOptionsBuilder self, string username, string password)
        {
            static string CreateHash(string value)
            {
                using var sha256 = SHA256.Create();
                var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));
                return Convert.ToBase64String(challengeBytes);
            }

            return self.WithCredentials(username, CreateHash(password));
        }
    }
}