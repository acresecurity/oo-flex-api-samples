using System.Collections;
using System.Net;
using System.Reflection;
using Common;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli
{
    internal abstract class DefaultCommand<TSettings> : Common.Cli.AsyncCommand<TSettings>
        where TSettings : CommandSettings
    {
        protected DefaultCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        #region Overrides of AsyncCommand<TSettings>

        public override async Task<int> ExecuteAsync(CommandContext context, TSettings settings)
        {
            var client = await GetClient();
            if (client == null)
                return 1;

            //
            // Fetch operator user information which include the rights assigned to the operator.
            //
            var response = await AnsiConsole
                .Status()
                .StartAsync("Retrieving operator/user information", p => client.GetJsendAsync($"{_settings.Api}/account/user"));

            if (!response.IsSuccess())
            {
                DisplayError(response);
                return 1;
            }

            var userInfo = response.Deserialize<UserInfo>();
            return await ExecuteAsync(context, settings, client, userInfo);
        }

        #endregion

        protected abstract Task<int> ExecuteAsync(CommandContext context, TSettings settings, HttpClient client, UserInfo userInfo);

        protected void DisplayTable<T>(T data, params string[] fields) where T : class => DisplayTable(data, null, fields);

        protected void DisplayTable<T>(T data, Action<Table> configureTable, params string[] fields) where T : class
        {
            if (!fields.Any())
                return;

            var table = new Table();
            table.BorderColor(Color.Blue);
            table.AddColumns(fields);

            configureTable?.Invoke(table);

            if (data is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    var properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => fields.Contains(p.Name) && p.GetValue(item) != null);
                    table.AddRow(fields.Select(field => $"{properties.FirstOrDefault(p => p.Name == field)?.GetValue(item) ?? string.Empty}".EscapeMarkup()).ToArray());
                }
            }
            else
            {
                var properties = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => fields.Contains(p.Name) && p.GetValue(data) != null);
                table.AddRow(fields.Select(field => $"{properties.FirstOrDefault(p => p.Name == field)?.GetValue(data) ?? string.Empty}".EscapeMarkup()).ToArray());
            }

            table.Expand();
            AnsiConsole.Write(table);
        }

        protected void DisplayObject<T>(T data) where T : class
        {
            if (data is IEnumerable eumerable)
            {
                foreach (var item in eumerable)
                {
                    var table = new Table();
                    table.BorderColor(Color.Blue);
                    table.AddColumn("Property");
                    table.AddColumn("Value");

                    var properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetValue(item) != null);
                    foreach (var property in properties)
                        table.AddRow(property.Name, $"{property.GetValue(item)}".EscapeMarkup());

                    table.Expand();
                    AnsiConsole.Write(table);
                }
            }
            else
            {
                var table = new Table();
                table.BorderColor(Color.Blue);
                table.AddColumn("Property");
                table.AddColumn("Value");

                var properties = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetValue(data) != null);
                foreach (var item in properties)
                    table.AddRow(item.Name, $"{item.GetValue(data)}".EscapeMarkup());

                table.Expand();
                AnsiConsole.Write(table);
            }
        }

        protected void CompareAndDisplay<TOriginal, TUpdated>(TSettings settings, TOriginal original, TUpdated updated, string[] ignoreProperties = default, string[] includeProperties = default)
            where TOriginal : class
            where TUpdated : class
        {
            CompareAndDisplay<TOriginal, TUpdated>(settings, original, updated, null, ignoreProperties, includeProperties);
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
            var ignore = ignoreProperties ?? Array.Empty<string>();

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
