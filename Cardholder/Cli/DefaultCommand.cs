using System.Reflection;
using Common.Configuration;
using IdentityModel.OidcClient;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Cardholder.Cli
{
    public abstract class DefaultCommand<TSettings> : Common.Cli.AsyncCommand<TSettings>
        where TSettings : CommandSettings
    {
        protected DefaultCommand(Microsoft.Extensions.Options.IOptions<Options> options, OidcClient oidcClient)
            : base(options, oidcClient)
        {
        }

        protected void DisplayObject<T>(T data) where T : class
        {
            var table = new Table();
            table.BorderColor(Color.Blue);
            table.AddColumn("Property");
            table.AddColumn("Value");

            var properties = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.GetValue(data) != null);
            foreach (var item in properties)
                table.AddRow(item.Name, $"{item.GetValue(data)}");

            table.Expand();
            AnsiConsole.Write(table);
        }

        protected void CompareAndDisplay<TOriginal, TUpdated>(TSettings settings, TOriginal original, TUpdated updated, string[]? ignoreProperties = default, string[]? includeProperties = default)
            where TOriginal : class
            where TUpdated : class
        {
            var table = new Table();
            //table.BorderColor(Color.Red);
            table.AddColumn("[yellow]Property[/]");
            table.AddColumn("[yellow]Parameter[/]");
            table.AddColumn("[yellow]Original Value[/]");
            table.AddColumn("[yellow]Updated Value[/]");

            var include = includeProperties ?? Array.Empty<string>();
            var ignore = ignoreProperties ?? Array.Empty<string>();

            // Get a list of properties that were supplied via the command line.
            var supplied = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => include.Contains(p.Name) || (p.GetValue(settings) != null && !ignore.Contains(p.Name)));

            var originalProperties = original.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => supplied.Any(x => x.Name == p.Name)).ToDictionary(p => p.Name, p => p);
            var updatedProperties = updated.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => supplied.Any(x => x.Name == p.Name)).ToDictionary(p => p.Name, p => p);

            foreach (var item in supplied)
            {
                table.AddRow(item.Name,
                    $"{item.GetValue(settings)}",
                    $"{originalProperties[item.Name].GetValue(original)}",
                    $"{updatedProperties[item.Name].GetValue(updated)}");
            }

            table.Expand();
            AnsiConsole.Write(table);
        }
    }
}
