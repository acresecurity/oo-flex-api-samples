using Flex.Cli.DataEntry.AccessLevels.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.AccessLevels
{
    internal class AddAccessLevelCommand : AsyncCommand<AddAccessLevelSettings>
    {
        public AddAccessLevelCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AddAccessLevelSettings>
        
        protected override Task<int> ExecuteAsync(CommandContext context, AddAccessLevelSettings settings, HttpClient client, UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}