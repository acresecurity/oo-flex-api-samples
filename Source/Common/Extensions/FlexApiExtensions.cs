using System.Reflection;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Common
{
    public static class FlexApiExtensions
    {
        public static async Task<string?> HandleTokenViaResourceOwnerPassword(this HttpClient client, string authority, string clientId, string secret, string operatorId, string operatorPassword)
        {
            var accessToken = await client.GetRefreshTokenViaResourceOwnerPasswordAsync(authority, clientId, secret, operatorId, operatorPassword);
            if (!string.IsNullOrEmpty(accessToken))
                client.SetBearerToken(accessToken);

            return accessToken;
        }

        public static async Task<string?> HandleViaAuthCodeGrant(this HttpClient client, string authority, string clientId, string secret, string redirect_uri, string authCode)
        {
            var accessToken = await client.GetRefreshTokenViaAuthCodeGrantAsync(authority, clientId, secret, redirect_uri, authCode);
            if (!string.IsNullOrEmpty(accessToken))
                client.SetBearerToken(accessToken);

            return accessToken;
        }

        private static IConfigurationRoot? _config;

        private static IConfigurationRoot Config() =>
            _config ??= new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyConfigurationAttribute>().Configuration}.json", true, true)
                .Build();

        public static string MqttBaseUrlFromConfig()
        {
            var config = Config();
            return config["MqttHostSettings:Host"];
        }

        public static int MqttPortFromConfig()
        {
            var config = Config();
            return Convert.ToInt32(config["MqttHostSettings:Port"]);
        }

        public static string MqttClientUserNameFromConfig()
        {
            var config = Config();
            return config["MqttClientSettings:client_id"];
        }

        public static string MqttClientPasswordFromConfig()
        {
            var config = Config();
            return config["MqttClientSettings:client_secret"];
        }

        public static string MqttClientIdFromConfig()
        {
            var config = Config();
            return config["MqttClientSettings:Id"];
        }

        public static string GetBaseUrlFromConfig()
        {
            var config = Config();
            return config["FlexApi:base_url"];
        }

        public static async Task<HttpClient> GetHttpClientViaResourceOwnerPassword()
        {
            var config = Config();

            var client = new HttpClient();
            await client.HandleTokenViaResourceOwnerPassword(
                config["OAuth:authority"],
                config["OAuth:client_id"],
                config["OAuth:client_secret"],
                config["Operator:operator_id"],
                config["Operator:operator_password"]);

            return client;
        }

        public static async Task<(HttpClient, string?)> GetHttpClientViaAuthCodeGrant(string authCode)
        {
            var config = Config();

            var client = new HttpClient();

            var accessToken = await client.HandleViaAuthCodeGrant(
                config["OAuth:authority"],
                config["OAuth:client_id"],
                config["OAuth:client_secret"],
                config["OAuth:redirect_uri"],
                authCode);

            return (client, accessToken);
        }

        private static async Task<string?> GetRefreshTokenViaAuthCodeGrantAsync(this HttpClient client, string authority, string clientId, string clientSecret, string redirectUri, string authCode)
        {
            var disco = await client.GetDiscoveryDocumentAsync(authority);
            if (disco.IsError)
                throw new Exception(disco.Error);

            var response = await client.RequestAuthorizationCodeTokenAsync(
                new AuthorizationCodeTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = clientId,
                    Code = authCode,
                    ClientSecret = clientSecret,
                    GrantType = OidcConstants.GrantTypes.AuthorizationCode,
                    RedirectUri = redirectUri
                });

            if (response.IsError)
            {
                AnsiConsole.Write(response.ErrorDescription);
                return null;
            }

            return response.AccessToken;
        }

        private static async Task<string?> GetRefreshTokenViaResourceOwnerPasswordAsync(this HttpClient client, string authority, string clientId, string secret, string operatorId, string operatorPassword)
        {
            var disco = await client.GetDiscoveryDocumentAsync(authority);
            if (disco.IsError)
                throw new Exception(disco.Error);

            var response = await client.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = clientId,
                    ClientSecret = secret,
                    UserName = operatorId,
                    Password = operatorPassword
                });

            if (response.IsError)
            {
                AnsiConsole.Write(response.ErrorDescription);
                return null;
            }

            return response.AccessToken;
        }
    }
}