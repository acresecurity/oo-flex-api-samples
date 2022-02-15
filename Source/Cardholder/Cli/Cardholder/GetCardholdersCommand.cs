using System.Net;
using Common;
using Common.Configuration;
using Flurl;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;
using CardholderDto = Common.DataObjects.Cardholder;

namespace Cardholder.Cli.Cardholder
{
    internal class GetCardholdersCommand : DefaultCommand<GetCardholdersSettings>
    {
        public GetCardholdersCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<GetCardholderSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, GetCardholdersSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view cardholders");
                return 0;
            }

            var (pagedResponse, cardholders) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving cardholders ...", p =>
                    client.FetchPaged<CardholderDto[]>(
                        $"{_settings.Api}/api/v2/cardholders"
                            .SetQueryParam("where", settings.Where?.Replace('\'', '"'))
                            .SetQueryParam("orderBy", settings.OrderBy)));

            if (pagedResponse.IsSuccess())
            {
                DisplayTable(cardholders, nameof(CardholderDto.UniqueKey), nameof(CardholderDto.FirstName), nameof(CardholderDto.LastName), nameof(CardholderDto.Title), nameof(CardholderDto.Department));
                return 0;
            }

            DisplayError(pagedResponse);
            return 1;
        }

        #endregion
    }
}
