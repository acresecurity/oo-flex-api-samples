using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class DeletePhotoCommand : AsyncCommand<DeletePhotoSettings>
    {
        public DeletePhotoCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory)
            : base(options, cache, factory)
        {
        }

        #region Overrides of AsyncCommand<DeletePhotoSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, DeletePhotoSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.BadgingRemovePhoto];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to delete a cardholder");
                return CommandLineInsufficientPermission; ;
            }

            if (!AnsiConsole.Confirm("Are you sure you wish to delete the cardholder photo?", false))
                return CommandLineCancelled;

            var response = await AnsiConsole.Status().StartAsync("Deleting photo...", _ => client.DeleteJSendAsync($"{Settings.Api}/api/v2/photo/{settings.UniqueKey}"));

            if (response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
                AnsiConsole.MarkupLine("[green]Photo deleted successfully[/]");

            return CommandLineSuccess;
        }

        #endregion
    }
}
