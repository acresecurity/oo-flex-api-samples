using System.IO.Abstractions;
using System.Runtime.Serialization;
using Flex.Services.Abstractions;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Flex.Cli.Setup.Models
{
    [DataContract]
    internal class SetupModel : ReactiveObject
    {
        private readonly ILogger _logger;
        private readonly IOptionsProvider _optionsProvider;
        private readonly IConfiguration _configuration;
        private readonly string _fileName;
        private readonly IHostEnvironment _environment;
        private readonly IFileSystem _fileSystem;
        private readonly ICacheStore _cache;

        public SetupModel(ILogger<SetupModel> logger, IOptionsProvider optionsProvider, IConfiguration configuration, IHostEnvironment environment, IFileSystem fileSystem, ICacheStore cache)
        {
            _logger = logger;
            _optionsProvider = optionsProvider;
            _configuration = configuration;
            _environment = environment;
            _fileSystem = fileSystem;
            _cache = cache;

            _fileName = environment.IsDevelopment()
                ? "appsettings.Development.json"
                : "appsettings.json";

            Options = optionsProvider.Options;
        }

        #region Observables

        [Reactive, DataMember]
        public Configuration.Options Options { get; set; }

        [Reactive, DataMember]
        public ValidationResult Validation { get; set; }

        #endregion

        public void FlushCache() => _cache.Provider.Flush();

        public void Validate() => Validation = _optionsProvider.Validate();

        public void Save()
        {
            Validation = _optionsProvider.Validate();
            if (!Validation.IsValid)
            {
                _logger.LogError("Validation failed. Settings not saved.");
                return;
            }
            
            _logger.LogInformation("Saving settings...");

            JObject json;
            var filename = _fileSystem.Path.Combine(_environment.ContentRootPath, _fileName);
            if (File.Exists(filename))
            {
                var raw = _fileSystem.File.ReadAllText(filename);
                json = JObject.Parse(raw);
            }
            else
                json = new JObject();

            var optionsJson = JObject.FromObject(new { FlexApi = Options });
            json.Merge(optionsJson);

            _fileSystem.File.WriteAllText(filename, json.ToString());

            if (_configuration is IConfigurationRoot root)
                root.Reload();
        }
    }
}
