using System.IO.Abstractions;
using System.Net;
using Common;
using Common.Configuration;
using Common.DataObjects;
using DataEntry.Cli.Cardholder.Settings;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace DataEntry.Cli.Cardholder
{
    internal class UploadPhotoCommand : DefaultCommand<UploadPhotoSettings>
    {
        private readonly IFileSystem _fileSystem;

        public UploadPhotoCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient, IFileSystem fileSystem)
            : base(options, oidcClient)
        {
            _fileSystem = fileSystem;
        }

        #region Overrides of DefaultCommand<UploadPhotoSettings>

        protected override async Task<int> ExecuteAsync(CommandContext context, UploadPhotoSettings settings, HttpClient client, UserInfo userInfo)
        {
            var right = userInfo[UserRights.BadgingAddPhoto];
            if (!right.AsBool())
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Operator is not allowed to add a photo to a cardholder");
                return 1;
            }

            if (!_fileSystem.File.Exists(settings.Filename))
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "File does not exist");
                return 1;
            }

            var format = await ImageFileFormat.GetImageFormat(settings.Filename);
            if (format == ImageFileFormat.UnknownFormat)
            {
                AnsiConsole.MarkupLine("[red]{0}[/]", "Unknown or unsupported image format");
                return 1;
            }

            await using var stream = _fileSystem.File.OpenRead(settings.Filename);
            using var request = new HttpRequestMessage(HttpMethod.Post, "file");
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "file", _fileSystem.Path.GetFileName(settings.Filename) }
            };

            var response = await AnsiConsole.Status().StartAsync("Uploading photo...", _ => client.PostJSendAsync($"{Settings.Api}/api/v2/cardholder/{settings.UniqueKey}/upload", content));
            if (response.IsSuccess())
            {
                var photo = response.Deserialize<Photo>();
                DisplayObject(photo);

                return 0;
            }

            return DisplayError(response) ? 1 : 0;
        }

        #endregion
    }
}
