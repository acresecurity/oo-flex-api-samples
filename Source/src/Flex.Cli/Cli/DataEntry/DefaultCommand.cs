using Flex.DataObjects;
using Flex.DataObjects.Settings;
using Flex.Services.Abstractions;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry
{
    internal abstract class DefaultCommand<TSettings> : AsyncCommand<TSettings>
        where TSettings : DefaultCommandSettings
    {
        protected DefaultCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory clientFactory)
            : base(options, cache, clientFactory)
        {
        }

        protected abstract Task<int> ExecuteAsync(CommandContext context, TSettings commandSettings, HttpClient client, UserInfo userInfo, Settings settings);

        #region Overrides of AsyncCommand<TSettings>

        protected sealed override async Task<int> ExecuteAsync(CommandContext context, TSettings commandSettings, HttpClient client, UserInfo userInfo)
        {
            var settings = await Cache.Settings();
            if (settings == null)
                return CommandLineCacheError;

            return await ExecuteAsync(context, commandSettings, client, userInfo, settings);
        }

        #endregion
    }
}
