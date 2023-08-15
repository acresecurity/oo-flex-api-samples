﻿using System.Reflection;
using Flex.DataObjects;
using Flex.Services.Abstractions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Flex.Cli
{
    /// <summary>
    /// Base class for executing the command line options.
    /// Provides simple helper for displaying response errors and managing tokens.
    /// Token methods is rather simplistic and shouldn't be used in production.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    internal abstract class AsyncCommand<TSettings> : Spectre.Console.Cli.AsyncCommand<TSettings>
        where TSettings : DefaultCommandSettings
    {
        protected readonly Configuration.Options Settings;
        protected readonly ICacheStore Cache;

        private readonly IFlexHttpClientFactory _clientFactory;

        protected AsyncCommand(Microsoft.Extensions.Options.IOptions<Configuration.Options> options, ICacheStore cache, IFlexHttpClientFactory clientFactory)
        {
            Settings = options.Value;
            Cache = cache;
            _clientFactory = clientFactory;
        }

        protected abstract Task<int> ExecuteAsync(CommandContext context, TSettings settings, HttpClient client, UserInfo userInfo);

        #region Overrides of AsyncCommand<TSettings>

        public sealed override async Task<int> ExecuteAsync(CommandContext context, TSettings settings)
        {
            var client = await _clientFactory.GetClient();
            if (client == null)
                return CommandLineGeneralError;

            var userInfo = await Cache.UserInfo();
            if (userInfo == null)
                return CommandLineCacheError;

            return await ExecuteAsync(context, settings, client, userInfo);
        }

        #endregion

        protected void CompareAndDisplay<TOriginal, TUpdated>(TSettings settings, TOriginal original, TUpdated updated, string[] ignoreProperties = default, string[] includeProperties = default)
            where TOriginal : class
            where TUpdated : class
        {
            CompareAndDisplay(settings, original, updated, null, ignoreProperties, includeProperties);
        }

        protected void CompareAndDisplay<TOriginal, TUpdated>(TSettings settings, TOriginal original, TUpdated updated, Action<Table> configureTable, string[] ignoreProperties = default, string[] includeProperties = default)
            where TOriginal : class
            where TUpdated : class
        {
            var table = new Table();
            //table.BorderColor(Color.Red);
            table.AddColumn("[yellow]Property[/]");
            table.AddColumn("[yellow]Parameter[/]");
            table.AddColumn("[yellow]Original Value[/]");
            table.AddColumn("[yellow]Updated Value[/]");

            configureTable?.Invoke(table);

            var include = includeProperties ?? Array.Empty<string>();
            var ignore = (ignoreProperties ?? Array.Empty<string>()).Concat(new[] { nameof(DefaultCommandSettings.OutputJson), nameof(DefaultCommandSettings.IncludeBanner) }).ToArray();

            // Get a list of properties that were supplied via the command line.
            var supplied = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => include.Contains(p.Name) || (p.GetValue(settings) != null && !ignore.Contains(p.Name)));

            var originalProperties = original.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => supplied.Any(x => x.Name == p.Name)).ToDictionary(p => p.Name, p => p);
            var updatedProperties = updated.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => supplied.Any(x => x.Name == p.Name)).ToDictionary(p => p.Name, p => p);

            foreach (var item in supplied)
            {
                table.AddRow(item.Name,
                    $"{item.GetValue(settings)}".EscapeMarkup(),
                    $"{originalProperties[item.Name].GetValue(original)}".EscapeMarkup(),
                    $"{updatedProperties[item.Name].GetValue(updated)}".EscapeMarkup());
            }

            table.Expand();
            AnsiConsole.Write(table);
        }
    }
}