using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.DataObjects.Cardholder;
using Flex.Services.Abstractions;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class ViewCardholderCommand : AsyncCommand<ViewCardholderSettings>
    {
        public ViewCardholderCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<ViewCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, ViewCardholderSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view cardholders");
                return CommandLineInsufficientPermission;
            }

            var json = JToken.Parse("{}");

            var response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}");
            if (response.IsSuccess())
            {
                var cardholder = response.Deserialize<DataObjects.Cardholder.Cardholder>();
                if (settings.OutputJson)
                    json["cardholder"] = response.Data;
                else
                    DisplayObject(cardholder);
            }

            if (settings.Credentials)
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/credentials");
                if (response.IsSuccess())
                {
                    var credentials = response.Deserialize<DataObjects.Cardholder.Credential[]>();
                    if (settings.OutputJson)
                        json["credentials"] = response.Data;
                    else
                        DisplayObject(credentials);
                }
            }

            if (settings.Photos)
            {
                response = await client.GetJsendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/photos");
                if (response.IsSuccess())
                {
                    var photos = response.Deserialize<Photo[]>();
                    if (settings.OutputJson)
                        json["photos"] = response.Data;
                    else
                        DisplayObject(photos);
                }
            }

            if (settings.OutputJson && json.Any())
                DisplayJson(json);

            return DisplayError(response);
        }

        #endregion
    }
}