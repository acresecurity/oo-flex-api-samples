using Common.Configuration;
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
        protected readonly Options _settings;
        protected readonly OidcClient _oidcClient;

        public AsyncCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
        {
            _settings = options.Value;
            _oidcClient = oidcClient;
        }

        protected async Task<HttpClient?> GetClient()
        {
            Token tokens = null;
            if (File.Exists("tokens.json"))
            {
                var raw = await File.ReadAllTextAsync("tokens.json");
                tokens = JsonConvert.DeserializeObject<Token>(raw);
            }
            else
                tokens = new Token();

            if (tokens.IsValidAndIsExpired)
            {
                var response = await AnsiConsole
                    .Status()
                    .StartAsync("Refreshing token...", p => _oidcClient.RefreshTokenAsync(tokens.RefreshToken));

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
                    .StartAsync("Sign in with OIDC...", p => _oidcClient.LoginAsync());

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
    }
}
