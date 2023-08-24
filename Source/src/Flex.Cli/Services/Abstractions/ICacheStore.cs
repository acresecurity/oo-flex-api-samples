using EasyCaching.Core;
using Flex.DataObjects;
using Flex.DataObjects.Settings;

namespace Flex.Services.Abstractions
{
    internal interface ICacheStore
    {
        IEasyCachingProvider Provider { get; }

        Task<Dictionary<int, string>> EventDescriptions();

        Task<Settings> Settings();

        Task<UserInfo> UserInfo();

        bool TryGetValue<T>(string cacheKey, out T value);
    }
}
