using System.Collections;
using System.Reflection;
using Common.Configuration;
using Common.DataObjects;
using Common.Responses;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Newtonsoft.Json;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Common.Cli
{
    /// <summary>
    /// Base class for executing the command line options.
    /// Provides simple helper for displaying response errors and managing tokens.
    /// Token methods is rather simplistic and shouldn't be used in production.
    /// </summary>
    /// <typeparam name="TSettings"></typeparam>
    public abstract class AsyncCommand<TSettings> : Spectre.Console.Cli.AsyncCommand<TSettings>
        where TSettings : CommandSettings
    {
        protected readonly Options Settings;

        private readonly OidcClient _oidcClient;

        protected AsyncCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
        {
            Settings = options.Value;
            _oidcClient = oidcClient;
        }

        protected async Task<HttpClient> GetClient()
        {
            Token tokens;
            if (File.Exists("tokens.json"))
            {
                var raw = await File.ReadAllTextAsync("tokens.json");
                tokens = JsonConvert.DeserializeObject<Token>(raw);
            }
            else
                tokens = new Token();

            if (tokens == null)
                return null;

            if (tokens.IsValidAndIsExpired)
            {
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Refreshing token...", _ => _oidcClient.RefreshTokenAsync(tokens.RefreshToken));

                if (response.IsError)
                {
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.Error.EscapeMarkup());
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.ErrorDescription.EscapeMarkup());
                    return null;
                }

                tokens.ExpiresIn = response.ExpiresIn;
                tokens.AccessToken = response.AccessToken;
                tokens.ExpiresAt = response.AccessTokenExpiration.UtcDateTime;
                tokens.RefreshToken = response.RefreshToken;

                await File.WriteAllTextAsync("tokens.json", JsonConvert.SerializeObject(tokens));
            }
            else if (!tokens.IsValidAndNotExpiring)
            {
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Sign in with OIDC...", _ => _oidcClient.LoginAsync());

                if (response.IsError)
                {
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.Error.EscapeMarkup());
                    AnsiConsole.MarkupLine("[red]{0}[/]", response.ErrorDescription.EscapeMarkup());
                    return null;
                }

                tokens.ExpiresIn = response.TokenResponse.ExpiresIn;
                tokens.AccessToken = response.AccessToken;
                tokens.ExpiresAt = response.AccessTokenExpiration.UtcDateTime;
                tokens.RefreshToken = response.RefreshToken;

                await File.WriteAllTextAsync("tokens.json", JsonConvert.SerializeObject(tokens));
            }

            var result = new HttpClient();
            result.SetBearerToken(tokens.AccessToken);
            return result;
        }

        /// <summary>
        /// Helper method to display and errors or messages returned from the server that wasn't successful
        /// </summary>
        /// <param name="response"></param>
        protected static void DisplayError(JSendResponse response)
        {
            if (response.HasFailedValidation())
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[red]Validation Errors[/]");
                var table = new Table();
                table.BorderColor(Color.Red);
                table.AddColumn("Field");
                table.AddColumn("Message");

                foreach (var item in response.Deserialize<ValidationError[]>())
                    table.AddRow(item.Field, item.Message.EscapeMarkup());

                table.Expand();
                AnsiConsole.Write(table);
            }
            else if (response.HasFailed() || response.HasError())
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[red]{0}[/]", $"{response.Status} Message");
                AnsiConsole.MarkupLine("    [red]{0}[/]", Markup.Escape(string.IsNullOrEmpty(response.Message) ? response.Data.ToString() : response.Message));
            }
        }

        protected void DisplayTable<T>(T data, params string[] fields) where T : class => DisplayTable(data, null, fields);

        protected void DisplayTable<T>(T data, Action<Table> configureTable, params string[] fields) where T : class
        {
            if (!fields.Any())
                return;

            var table = new Table();
            table.BorderColor(Color.Blue);
            foreach (var item in fields)
                table.AddColumn($"[yellow]{item}[/]");

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
            if (data is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
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
