using System.Net;
using Flex.Cli.DataEntry.Credential.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Credential
{
    internal class AddCredentialCommand : DefaultCommand<AddCredentialSettings>
    {
        public AddCredentialCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<AddCredentialSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, AddCredentialSettings commandSettings, HttpClient client, UserInfo userInfo, DataObjects.Settings.Settings settings)
        {
            var right = userInfo[UserRights.ADDCARD];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add credentials");
                return CommandLineInsufficientPermission;
            }

            var response = await AnsiConsole.Status().StartAsync("Adding credential...", _ => client.PostJSendAsync($"api/v2/cardholder/{commandSettings.CardholderKey}/credential", commandSettings));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (commandSettings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var added = response.Deserialize<DataObjects.Cardholder.Credential>();
                AnsiConsole.MarkupLine($"[green]Credential added successfully[/] UniqueKey: [[{added.UniqueKey}]]");
                CompareAndDisplay(commandSettings, new DataObjects.Cardholder.Credential(), added, p => p.BorderColor(Color.Green), new[] { nameof(AddCredentialSettings.CardholderKey) });
            }

            return CommandLineSuccess;
        }

        #endregion

        private static bool DefaultToDeactivate(DataObjects.Settings.Settings settings, DataObjects.Cardholder.Cardholder cardholder)
        {
            var result = settings.NewCredential.DefaultToDeactivate ?? false;
            var tenantId = cardholder != null ? cardholder.TenantId ?? 0 : 0;
            if (settings.Segregation.UsesSegregation == true && settings.Tenants.TryGetValue(tenantId, out var tenant))
                result = tenant.DefaultToDeactivateNewCard ?? result;

            return !result;
        }

        private static int DefaultCardMode(DataObjects.Settings.Settings settings, DataObjects.Cardholder.Cardholder cardholder)
        {
            var result = settings.NewCredential.DefaultCardMode ?? 0;
            var tenantId = cardholder != null ? cardholder.TenantId ?? 0 : 0;
            if (settings.Segregation.UsesSegregation == true && settings.Tenants.TryGetValue(tenantId, out var tenant))
                result = tenant.DefaultCodeMode ?? result;

            return result;
        }

        private static int DefaultFacilityCode(DataObjects.Settings.Settings settings, DataObjects.Cardholder.Cardholder cardholder)
        {
            var result = settings.NewCredential.DefaultFacilityCode ?? 0;
            var tenantId = cardholder != null ? cardholder.TenantId ?? 0 : 0;
            if (settings.Segregation.UsesSegregation == true && settings.Tenants.TryGetValue(tenantId, out var tenant))
                result = tenant.DefaultFacilityCode ?? result;

            return result;
        }

        private static DateTime DefaultDeactivationDate(DataObjects.Settings.Settings settings, DataObjects.Cardholder.Cardholder cardholder)
        {
            var defaultActivation = settings.NewCredential.DefaultActivation ?? 90;
            var tenantId = cardholder != null ? cardholder.TenantId ?? 0 : 0;
            if (settings.Segregation.UsesSegregation == true && settings.Tenants.TryGetValue(tenantId, out var tenant))
                defaultActivation = tenant.DefaultActivation ?? defaultActivation;

            var result = defaultActivation;

            //Value 0=1 Day, 89=90 Days, 90=1 Year, 91=2 Years, etc.

            if (result < 0)
                result = 90;

            return result > 89
                ? DateTime.Now.AddYears(result - 89)
                : DateTime.Now.AddDays(result + 1);
        }
    }
}
