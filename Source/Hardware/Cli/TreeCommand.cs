using System.Net;
using Common.DataObjects;
using Flurl;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Hardware.Cli
{
    internal class TreeCommand : Common.Cli.AsyncCommand<TreeSettings>
    {
        private const int MaxRequests = 50;
        private int _requests = 1;

        public TreeCommand(Microsoft.Extensions.Options.IOptions<Common.Configuration.Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<TreeSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, TreeSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            return await AnsiConsole.Status()
                .Spinner(Spinner.Known.Dots)
                .SpinnerStyle(Style.Parse("green bold"))
                .StartAsync("Building...", async p =>
                {
                    try
                    {
                        var query = $"?flatten={settings.Flatten}";
                        if (settings.Filter.Any())
                            query = settings.Filter.Aggregate(query, (current, item) => current + $"&filter={item}");

                        var response = await client.GetJsendAsync($"{Settings.Api}/api/v2/hardware/tree/{query}");
                        if (response.IsSuccess())
                        {
                            var result = response.Deserialize<HardwareTreeItem[]>();

                            var tree = new Tree("Hardware Tree");
                            foreach (var item in result)
                            {
                                var node = tree.AddNode(item.DisplayText);
                                if (!settings.Flatten)
                                {
                                    await ExpandTree(item, node, client, p);
                                    _requests++;
                                    if (_requests >= MaxRequests)
                                        break;
                                }
                            }

                            AnsiConsole.Write(tree);
                        }
                        else if (response.IsRateLimited())
                            AnsiConsole.WriteLine(response.Message);
                        else
                            DisplayError(response);

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                        return 1;
                    }
                });
        }

        #endregion

        private async Task ExpandTree(HardwareTreeItem treeItem, TreeNode parentNode, HttpClient client, StatusContext status)
        {
            status.Status($"Building... ({_requests}/{MaxRequests})");
            if (_requests >= MaxRequests)
                return;

            // Depending on the server size (small, medium, large, extra-large) the hardware tree doesn't always return the child items.
            // When this happens we can request the child items by supplying the hardware type and unique key to the hardware tree request.
            if ((treeItem?.Items == null || !treeItem.IsCollection || !treeItem.Items.Any()) && treeItem?.UniqueKey != null && treeItem.UniqueKey != Guid.Empty)
            {
                var url = $"{Settings.Api}/api/v2/hardware/tree"
                    .SetQueryParam("type", treeItem.Type)
                    .SetQueryParam("uniqueKey", treeItem.UniqueKey);

                try
                {
                    _requests++;
                    var response = await client.GetJsendAsync(url);
                    if (!response.IsSuccess() && response.IsRateLimited())
                    {
                        await Task.Delay(TimeSpan.FromSeconds(response.RetryAfter()));
                        await ExpandTree(treeItem, parentNode, client, status);
                        return;
                    }

                    treeItem.Items = response.Deserialize<HardwareTreeItem[]>();
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                    return;
                }
            }

            if (treeItem?.Items != null)
            {
                foreach (var item in treeItem.Items)
                {
                    var node = parentNode.AddNode(item.DisplayText);
                    await ExpandTree(item, node, client, status);
                    if (_requests >= MaxRequests)
                        return;
                }
            }
        }
    }
}