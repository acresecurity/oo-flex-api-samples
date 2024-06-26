using EasyCaching.Core;
using Flex.Services.Abstractions;
using Flurl;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Spectre.Console;

namespace Flex.Services
{
    internal class FlexHttpClientFactory : IFlexHttpClientFactory
    {
        private readonly Configuration.Options _settings;
        private readonly IHttpClientFactory _factory;
        private readonly IEasyCachingProvider _cache;
        private readonly OidcClient _oidcClient;

        public FlexHttpClientFactory(IOptionsProvider options, OidcClient oidcClient, IHttpClientFactory factory, IEasyCachingProvider cache)
        {
            _settings = options.Options;
            _factory = factory;
            _cache = cache;
            _oidcClient = oidcClient;
        }

        public async Task<HttpClient> GetClient()
        {
            _cache.TryGetValue<Configuration.Token>(CacheKeyTokens, out var tokens);

            var authority = _settings.Api.AppendPathSegment("identity");
            if (tokens == null || tokens.Authority != authority)
            {
                tokens = new Configuration.Token { Authority = authority };
                await _cache.FlushAsync();
            }

            if (tokens.IsValidAndIsExpired)
            {
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Refreshing token...", _ => _oidcClient.RefreshTokenAsync(tokens.RefreshToken));

                if (response.IsError)
                {
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.Error.EscapeMarkup());
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.ErrorDescription.EscapeMarkup());
                    await _cache.FlushAsync();
                    return default;
                }

                tokens.ExpiresIn = response.ExpiresIn;
                tokens.AccessToken = response.AccessToken;
                tokens.ExpiresAt = response.AccessTokenExpiration.UtcDateTime;
                tokens.RefreshToken = response.RefreshToken;

                await _cache.SetAsync(CacheKeyTokens, tokens, TimeSpan.FromDays(365)).ConfigureAwait(false);
            }
            else if (!tokens.IsValidAndNotExpiring)
            {
                await _cache.FlushAsync();

                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Sign in with OIDC...", _ => _oidcClient.LoginAsync());

                if (response.IsError)
                {
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.Error.EscapeMarkup());
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.ErrorDescription.EscapeMarkup());
                    await _cache.FlushAsync();
                    return default;
                }

                tokens.ExpiresIn = response.TokenResponse.ExpiresIn;
                tokens.AccessToken = response.AccessToken;
                tokens.ExpiresAt = response.AccessTokenExpiration.UtcDateTime;
                tokens.RefreshToken = response.RefreshToken;

                await _cache.SetAsync(CacheKeyTokens, tokens, TimeSpan.FromDays(365)).ConfigureAwait(false);
            }
            
            var result = _factory.CreateClient();
            result.BaseAddress = new Uri(_settings.Api);
            result.SetBearerToken(tokens.AccessToken);
            return result;
        }
    }
}
