using System.Net;
using DataEntry.Cli.Credential.Settings;
using Common.Configuration;
using Common.DataObjects;
using Flurl;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;
using CredentialDto = Common.DataObjects.Credential;

namespace DataEntry.Cli.Credential
{
    internal class GetCredentialsCommand : DefaultCommand<GetCredentialsSettings>
    {
        public GetCredentialsCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<GetCredentialsSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, GetCredentialsSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.ViewPersonnel];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to view credentials");
                return 0;
            }

            var (pagedResponse, credentials) = await AnsiConsole
                .Status()
                .StartAsync("Retrieving credentials ...", _ =>
                    client.FetchPaged<CredentialDto[]>(
                        $"{Settings.Api}/api/v2/credentials"
                            .SetQueryParam("where", settings.Where?.Replace('\'', '"'))
                            .SetQueryParam("orderBy", settings.OrderBy)));

            if (pagedResponse.IsSuccess())
            {
                DisplayTable(credentials,
                    nameof(CredentialDto.UniqueKey),
                    nameof(CredentialDto.CardNumber),
                    nameof(CredentialDto.FacilityCode),
                    nameof(CredentialDto.HotStamp),
                    nameof(CredentialDto.Mode),
                    nameof(CredentialDto.CardType));

                return 0;
            }

            DisplayError(pagedResponse);
            return 1;
        }

        #endregion
    }
}
