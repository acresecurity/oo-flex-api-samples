using System.Net;
using EasyCaching.Core;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.DataObjects.Settings;
using Flex.Services.Abstractions;
using Spectre.Console;

namespace Flex.Services
{
    internal class CacheStore : ICacheStore
    {
        private readonly Configuration.Options _settings;
        private readonly IFlexHttpClientFactory _clientFactory;

        public CacheStore(IOptionsProvider options, IEasyCachingProvider cache, IFlexHttpClientFactory clientFactory)
        {
            _settings = options.Options;
            _clientFactory = clientFactory;
            
            Provider = cache;
        }

        public IEasyCachingProvider Provider { get; }

        public async Task<Settings> Settings()
        {
            if (Provider.TryGetValue<Settings>(CacheKeySettings, out var result))
                return result;

            var client = await _clientFactory.GetClient();
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving global DNA settings", _ => client.GetJsendAsync($"{_settings.Api}/api/v2/settings/global"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return default;
            }

            result = response.Deserialize<Settings>();
            await Provider.SetAsync(CacheKeySettings, result, DefaultCacheTime).ConfigureAwait(false);
            return result;
        }

        public async Task<UserInfo> UserInfo()
        {
            if (Provider.TryGetValue<UserInfo>(CacheKeyUserInfo, out var result))
                return result;

            var client = await _clientFactory.GetClient();

            //
            // Fetch operator user information which include the rights assigned to the operator.
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving operator/user information", _ => client.GetJsendAsync($"{_settings.Api}/account/user"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return default;
            }

            result = response.Deserialize<UserInfo>();
            await Provider.SetAsync(CacheKeyUserInfo, result, DefaultCacheTime).ConfigureAwait(false);

            return result;
        }

        public async Task<Dictionary<int, string>> EventDescriptions()
        {
            if (Provider.TryGetValue<Dictionary<int, string>>(CacheKeyEventDescriptions, out var result))
                return result;

            var client = await _clientFactory.GetClient();

            //
            // Retrieve the event descriptions just so that we can provide it on the table when displayed.
            //
            var (pagedResponse, descriptions) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving event descriptions", _ => client.FetchPaged<EventDescription[]>($"{_settings.Api}/api/v2/hardware/event/descriptions"));

            if (!pagedResponse.IsSuccess())
            {
                DisplayError(pagedResponse);
                return default;
            }

            result = descriptions.ToDictionary(p => p.UniqueId, p => p.Description);
            await Provider.SetAsync(CacheKeyEventDescriptions, result, DefaultCacheTime).ConfigureAwait(false);

            return result;
        }

        public bool TryGetValue<T>(string cacheKey, out T value)
        {
            return Provider.TryGetValue(cacheKey, out value);
        }
    }
}
