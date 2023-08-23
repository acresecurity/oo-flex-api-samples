using System.IO.Abstractions;
using System.Net;
using Flex.Cli.DataEntry.Cardholder.Settings;
using Flex.DataObjects;
using Flex.DataObjects.Cardholder;
using Flex.Services.Abstractions;
using Flex.Utils;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.DataEntry.Cardholder
{
    internal class UploadPhotoCommand : AsyncCommand<UploadPhotoSettings>
    {
        private readonly IFileSystem _fileSystem;

        public UploadPhotoCommand(IOptionsProvider options, ICacheStore cache, IFlexHttpClientFactory factory, IFileSystem fileSystem)
            : base(options, cache, factory) =>
            _fileSystem = fileSystem;

        #region Overrides of AsyncCommand<UploadPhotoSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, UploadPhotoSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.BadgingAddPhoto];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add a photo to a cardholder");
                return CommandLineInsufficientPermission;
            }

            if (!_fileSystem.File.Exists(settings.Filename))
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "File does not exist");
                return CommandLineGeneralError;
            }

            var format = await ImageFileFormat.GetImageFormat(settings.Filename);
            if (format == ImageFileFormat.UnknownFormat)
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Unknown or unsupported image format");
                return CommandLineGeneralError;
            }

            await using var stream = _fileSystem.File.OpenRead(settings.Filename);
            using var request = new HttpRequestMessage(HttpMethod.Post, "file");
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "file", _fileSystem.Path.GetFileName(settings.Filename) }
            };

            var response = await AnsiConsole.Status().StartAsync("Uploading photo...", _ => client.PostJSendAsync($"api/v2/cardholder/{settings.UniqueKey}/upload", content));

            if (!response.IsSuccess())
                return DisplayError(response);

            if (settings.OutputJson)
                DisplayJson(response.Data);
            else
            {
                var photo = response.Deserialize<Photo>();
                DisplayObject(photo);
            }

            return CommandLineSuccess;
        }

        #endregion
    }
}
