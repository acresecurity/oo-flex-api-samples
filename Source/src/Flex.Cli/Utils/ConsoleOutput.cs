using System.Collections;
using System.Reflection;
using Flex.DataObjects;
using Flex.Responses;
using Newtonsoft.Json.Linq;
using Spectre.Console.Json;
using Spectre.Console;

namespace Flex.Utils
{
    internal static class ConsoleOutput
    {
        public static bool DisplayJson(string json)
        {
            var writer = new JsonText(json);
            AnsiConsole.Write(writer);

            return true;
        }

        public static bool DisplayJson(JToken token) => DisplayJson(token.ToString());

        /// <summary>
        /// Helper method to display and errors or messages returned from the server that wasn't successful
        /// </summary>
        /// <param name="response"></param>
        public static int DisplayError(JSendResponse response)
        {
            if (response.HasFailedValidation())
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine(ValidationErrorHeader);
                var table = new Table();
                table.BorderColor(Color.Red);
                table.AddColumn("Field");
                table.AddColumn("Message");

                foreach (var item in response.Deserialize<ValidationError[]>())
                    table.AddRow(item.Field ?? "<Missing Field>", item.Message.EscapeMarkup());

                table.Expand();
                AnsiConsole.Write(table);

                return CommandLineServerValidationError;
            }

            if (response.HasFailed() || response.HasError())
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[red]{0}[/]", $"{response.Status} Message");
                AnsiConsole.MarkupLine("    [red]{0}[/]", Markup.Escape(string.IsNullOrEmpty(response.Message) ? response.Data.ToString() : response.Message));

                return CommandLineRequestError;
            }

            return CommandLineSuccess;
        }

        public static void DisplayTable<T>(T data, params string[] fields) where T : class => DisplayTable(data, null, fields);

        public static void DisplayTable<T>(T data, Action<Table> configureTable, params string[] fields) where T : class
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

        public static void DisplayObject<T>(T data) where T : class
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
    }
}
