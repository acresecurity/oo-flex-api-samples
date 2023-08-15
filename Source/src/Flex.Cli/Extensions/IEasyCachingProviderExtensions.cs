
// ReSharper disable once CheckNamespace
namespace EasyCaching.Core
{
    // ReSharper disable once InconsistentNaming
    internal static class IEasyCachingProviderExtensions
    {
        public static async Task<(bool, T)> TryGetValueAsync<T>(this IEasyCachingProvider self, string cacheKey)
        {
            var cache = await self.GetAsync<T>(cacheKey);
            return cache is { HasValue: true }
                ? (true, cache.Value)
                : (false, default);
        }

        public static bool TryGetValue<T>(this IEasyCachingProvider self, string cacheKey, out T value)
        {
            var cache = self.Get<T>(cacheKey);
            if (cache is { HasValue: true })
            {
                value = cache.Value;
                return true;
            }

            value = default;
            return false;
        }
    }
}
