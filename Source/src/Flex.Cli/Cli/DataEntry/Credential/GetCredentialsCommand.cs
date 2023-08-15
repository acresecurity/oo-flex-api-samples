using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.Configuration;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Flurl;
using Spectre.Console;
using Spectre.Console.Cli;
using CredentialDto = Flex.DataObjects.Cardholder.Credential;

namespace Flex.Cli.DataEntry.Credential
{
    internal class GetCredentialsCommand : AsyncCommand<GetCredentialsSettings>
    {
        public GetCredentialsCommand(Microsoft.Extensions.Options.IOptions<Options> options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<GetCredentialsSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, GetCredentialsSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view credentials");
                return CommandLineInsufficientPermission;
            }

            var (pagedResponse, credentials) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving credentials ...", _ =>
                    client.FetchPaged<CredentialDto[]>(
                        $"{Settings.Api}/api/v2/credentials"
                            .SetQueryParam("where", settings.Where?.Replace('\'', '"'))
                            .SetQueryParam("orderBy", settings.OrderBy)));

            if (!pagedResponse.IsSuccess())
                return DisplayError(pagedResponse);

            if (settings.OutputJson)
                DisplayJson(pagedResponse.Data);
            else
                DisplayTable(credentials,
                    nameof(CredentialDto.UniqueKey),
                    nameof(CredentialDto.CardNumber),
                    nameof(CredentialDto.FacilityCode),
                    nameof(CredentialDto.HotStamp),
                    nameof(CredentialDto.Mode),
                    nameof(CredentialDto.CardType));

            return CommandLineSuccess;
        }

        #endregion
    }
}