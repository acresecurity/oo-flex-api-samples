using System.Net;
using Common.Configuration;
using Common.DataObjects;
using DataEntry.Cli.Cardholder.Settings;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder
{
    internal class DeletePhotoCommand : DefaultCommand<DeletePhotoSettings>
    {
        public DeletePhotoCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of DefaultCommand<DeletePhotoSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeletePhotoSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.BadgingRemovePhoto];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a cardholder");
                return 0;
            }

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the cardholder photo?", false))
                return 1;

            var response = await AnsiConsole.Status().StartAsync("Deleting photo...", _ => client.DeleteJSendAsync($"{Settings.Api}/api/v2/photo/{settings.UniqueKey}"));
            if (response.IsSuccess())
            {
                AnsiConsole.MarkupLine("[green]Photo deleted successfully[/]");
                return 0;
            }

            DisplayError(response);
            return 1;
        }

        #endregion
    }
}
