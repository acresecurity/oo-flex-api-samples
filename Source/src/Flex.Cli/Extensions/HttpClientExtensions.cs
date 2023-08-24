using System.Collections;
using System.Net.Http.Headers;
using System.Text;
using Flex.Responses;
using Flurl;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Spectre.Console;

// ReSharper disable once CheckNamespace
namespace System.Net
{
    /// <summary>
    /// JSend response helper extensions.
    /// Flex API will always return a HTTP status code of 200, unless something goes horribly wrong.
    /// It will return a guaranteed message structure.
    ///   {
    ///     "status": "{ success | fail | error }",
    ///     "data": "{ a wrapper for any data to be returned }"
    ///   }
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    internal static class HttpClientExtensions
    {
        public static Task<JSendResponse> DeleteJSendAsync(this HttpClient httpClient, string requestUri, object content = null)
            => httpClient.SendJSendAsync<JSendResponse>(HttpMethod.Delete, requestUri, content);

        public static Task<T> DeleteAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJSendAsync<T>(HttpMethod.Delete, requestUri, content);

        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string requestUri)
            => JsonConvert.DeserializeObject<T>(await httpClient.GetStringAsync(requestUri));

        public static Task<JSendResponse> GetJsendAsync(this HttpClient httpClient, string requestUri)
            => httpClient.GetAsync<JSendResponse>(requestUri);

        public static Task<PagedJSendResponse> GetPagedJsendAsync(this HttpClient httpClient, string requestUri, int page = 1, int pageSize = 12)
        {
            var split = requestUri.Split('?');

            //NOTE: The QueryHelper converts Array values from the default Arg=V1&Arg=V2&Arg=V3 to Arg=V1,V2,V3
            // The former is more compact and cleaner. Important when passing Arrays or enum lists.
            //See: https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0#collections

            var address = split[0];
            var query = split.Length == 2
                ? QueryHelpers.ParseQuery(split[1]).ToDictionary(p => p.Key, p => p.Value.ToString())
                : new Dictionary<string, string>();

            query["page"] = page.ToString();
            query["pageSize"] = pageSize.ToString();

            return httpClient.GetAsync<PagedJSendResponse>(QueryHelpers.AddQueryString(address, query));
        }

        public static async Task<(PagedJSendResponse, T)> FetchPaged<T>(this HttpClient httpClient, string url)
        {
            const int pageSize = 500;

            if (typeof(T).GetElementType() == null)
                throw new ArgumentException($"{typeof(T)} is not an array type");

            var nextUrl = url.SetQueryParam("pageSize", pageSize);
            Array result = null;
            var index = 0;

            PagedJSendResponse response;
            do
            {
                response = await httpClient.GetAsync<PagedJSendResponse>(nextUrl);
                if (response.IsSuccess())
                {
                    nextUrl = response.NextPageUrl;
                    result ??= Array.CreateInstance(typeof(T).GetElementType(), response.TotalNumberOfRecords);

                    if (response.Deserialize<T>() is IEnumerable data)
                    {
                        foreach (var item in data)
                            result.SetValue(item, index++);
                    }
                }
                else
                {
                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("[red]{0}[/]", $"{response.Status} Message");
                    AnsiConsole.MarkupLine("[red]{0}[/]", Markup.Escape(string.IsNullOrEmpty(response.Message) ? response.Data.ToString() : response.Message));

                    return default;
                }
            } while (!string.IsNullOrEmpty(nextUrl));

            return (response, (T)(object)result);
        }

        public static Task<JSendResponse> PostJSendAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJSendAsync<JSendResponse>(HttpMethod.Post, requestUri, content);

        public static Task<T> PostAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJSendAsync<T>(HttpMethod.Post, requestUri, content);

        public static Task<JSendResponse> PutJSendAsync(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJSendAsync<JSendResponse>(HttpMethod.Put, requestUri, content);

        public static Task<T> PutAsync<T>(this HttpClient httpClient, string requestUri, object content)
            => httpClient.SendJSendAsync<T>(HttpMethod.Put, requestUri, content);

        public static Task SendAsync(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
            => httpClient.SendJSendAsync<IgnoreResponse>(method, requestUri, content);

        public static async Task<T> SendJSendAsync<T>(this HttpClient httpClient, HttpMethod method, string requestUri, object content)
        {
            var message = new HttpRequestMessage(method, requestUri)
            {
                Version = HttpVersion.Version11, 
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json"),
            };
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (content != null)
            {
                if (content is HttpContent httpContent)
                    message.Content = httpContent;
                else
                    message.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            }

            var httpResponseMessage = await httpClient.SendAsync(message);

            httpResponseMessage.EnsureSuccessStatusCode();

            return typeof(T) == typeof(IgnoreResponse)
                ? default
                : JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }

        private class IgnoreResponse
        {
        }
    }
}
