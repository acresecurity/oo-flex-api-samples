using System.IO.Abstractions;
using EnumsNET;
using Flex.Services.Abstractions;
using Newtonsoft.Json.Linq;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli.Setup
{
    internal class InfoCommand : Command<DefaultCommandSettings>
    {
        private readonly IOptionsProvider _optionsProvider;
        private readonly IFileSystem _fileSystem;
        private readonly ICacheStore _cache;

        public InfoCommand(IOptionsProvider optionsProvider, IFileSystem fileSystem, ICacheStore cache)
        {
            _optionsProvider = optionsProvider;
            _fileSystem = fileSystem;
            _cache = cache;
        }

        #region Overrides of Command

        public override int Execute(CommandContext context, DefaultCommandSettings settings)
        {
            var options = _optionsProvider.Options;

            if (settings.FlushCache)
            {
                _cache.Provider.Flush();
                AnsiConsole.MarkupLine("[green]Cache flushed[/]");
            }

            if (settings.OutputJson)
            {
                DisplayJson(JToken.FromObject(options));
                return CommandLineSuccess;
            }

            var table = new Table()
                .Border(TableBorder.Minimal)
                .BorderColor(Color.Blue)
                .AddColumn("Name")
                .AddColumn("Value")
                .AddRow("Api", options.Api ?? "<Unset>")
                .AddRow("ClientId", options.ClientId ?? "<Unset>")
                .AddRow("ClientSecret", string.IsNullOrEmpty(options.ClientSecret) ? "<Unset>" : "[[REDACTED]]")
                .AddRow("MQTT Transport", options.Mqtt.Transport.AsString())
                .AddRow("MQTT Host", options.Mqtt.Host ?? "<Unset>")
                .AddRow("Cache Directory", _fileSystem.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "flex.cli", "cache"))
                ;

            AnsiConsole.Write(table);

            return CommandLineSuccess;
        }

        #endregion
    }
}
