using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Flurl;
using Spectre.Console;
using Spectre.Console.Cli;
using CardholderDto = Flex.DataObjects.Cardholder.Cardholder;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class GetCardholdersCommand : AsyncCommand<GetCardholdersSettings>
    {
        public GetCardholdersCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<GetCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, GetCardholdersSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view cardholders");
                return CommandLineInsufficientPermission; ;
            }

            // Build where clause


            var (pagedResponse, cardholders) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving cardholders ...", _ =>
                    client.FetchPaged<CardholderDto[]>(
                        "api/v2/cardholders"
                            .SetQueryParam("where", settings.Where?.Replace('\'', '"'))
                            .SetQueryParam("orderBy", settings.OrderBy)));

            if (!pagedResponse.IsSuccess())
                return DisplayError(pagedResponse);

            if (settings.OutputJson)
                DisplayJson(pagedResponse.Data);
            else
                DisplayTable(cardholders,
                    nameof(CardholderDto.UniqueKey),
                    nameof(CardholderDto.FirstName),
                    nameof(CardholderDto.LastName),
                    nameof(CardholderDto.Title),
                    nameof(CardholderDto.Department),
                    nameof(CardholderDto.Updated));

            return CommandLineSuccess;
        }

        #endregion
    }
}
