using System.Diagnostics;
using System.Net;
using Flex.Cli.Hardware.Settings;
using Flex.DataObjects;
using Flex.DataObjects.Hardware;
using Flex.Services.Abstractions;
using Flurl;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Hardware
{
    internal class TreeCommand : AsyncCommand<TreeSettings>
    {
        private const int MaxRequests = 50;
        private int _requests = 1;

        public TreeCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<TreeSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, TreeSettings settings, HttpClient client, UserInfo userInfo)
        {
            var tree = new Tree("Hardware Tree");
            return await AnsiConsole.Live(tree)
                .StartAsync(async p =>
                {
                    try
                    {
                        var query = $"?flatten={settings.Flatten}";
                        if (settings.Filter.Any())
                            query = settings.Filter.Aggregate(query, (current, item) => current + $"&filter={item}");

                        var response = await client.GetJsendAsync($"api/v2/hardware/tree/{query}");
                        if (response.IsSuccess())
                        {
                            var result = response.Deserialize<HardwareTreeItem[]>();

                            foreach (var item in result)
                            {
                                var node = tree.AddNode(item.MarkupText);
                                if (settings.Flatten)
                                    p.Refresh();
                                else
                                {
                                    await ExpandTree(item, node, client, p);
                                    _requests++;
                                    if (_requests >= MaxRequests)
                                        break;
                                }
                            }
                        }
                        else if (response.IsRateLimited())
                            AnsiConsole.WriteLine(response.Message);

                        return DisplayError(response);
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);
                        return CommandLineUnhandledException;
                    }
                });
        }

        #endregion

        private async Task ExpandTree(HardwareTreeItem treeItem, TreeNode parentNode, HttpClient client, LiveDisplayContext context)
        {
            if (_requests >= MaxRequests)
                return;

            context.Refresh();

            // Depending on the server size (small, medium, large, extra-large) the hardware tree doesn't always return the child items.
            // When this happens we can request the child items by supplying the hardware type and unique key to the hardware tree request.
            if (treeItem?.Items != null && treeItem.IsCollection && !treeItem.Items.Any() && treeItem?.UniqueKey  != null && treeItem.UniqueKey != Guid.Empty)
            {
                var url = "api/v2/hardware/tree"
                    .SetQueryParam("type", treeItem.Type)
                    .SetQueryParam("uniqueKey", treeItem.UniqueKey);

                try
                {
                    _requests++;
                    var response = await client.GetJsendAsync(url);
                    if (!response.IsSuccess() && response.IsRateLimited())
                    {
                        await Task.Delay(TimeSpan.FromSeconds(response.RetryAfter()));
                        await ExpandTree(treeItem, parentNode, client, context);
                        return;
                    }

                    treeItem.Items = response.Deserialize<HardwareTreeItem[]>();
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex.Demystify(), ExceptionFormats.ShortenEverything);
                    return;
                }
            }

            if (treeItem?.Items != null)
            {
                foreach (var item in treeItem.Items)
                {
                    var node = parentNode.AddNode(item.MarkupText);
                    await ExpandTree(item, node, client, context);
                    if (_requests >= MaxRequests)
                        return;
                }
            }
        }
    }
}
