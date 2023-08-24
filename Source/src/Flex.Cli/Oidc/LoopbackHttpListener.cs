using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Flex.Oidc
{
    internal class LoopbackHttpListener : IDisposable
    {
        private readonly TimeSpan _defaultTimeout = TimeSpan.FromMinutes(5);
        private readonly IWebHost _host;
        private readonly TaskCompletionSource<string> _source = new();

        public string Url { get; }

        public LoopbackHttpListener(int port, string path = null)
        {
            path ??= string.Empty;
            if (path.StartsWith("/"))
                path = path[1..];

            Url = $"http://127.0.0.1:{port}/{path}";

            _host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(Url)
                .Configure(Configure)
                .Build();

            _host.Start();
        }

        public void Dispose() => Task.Run(async () =>
            {
                await Task.Delay(500);
                _host.Dispose();
            });

        private void Configure(IApplicationBuilder app) =>
            app?.Run(async context =>
            {
                if (context.Request.Method == "GET")
                {
                    await SetResultAsync(context.Request.QueryString.Value, context);
                }
                else if (context.Request.Method == "POST")
                {
                    if (context.Request.ContentType != null && !context.Request.ContentType.Equals("application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase))
                        context.Response.StatusCode = 415;
                    else
                    {
                        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8);
                        var body = await reader.ReadToEndAsync();
                        await SetResultAsync(body, context);
                    }
                }
                else
                    context.Response.StatusCode = 405;
            });

        private async Task SetResultAsync(string value, HttpContext context)
        {
            try
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(SuccessResponse);
                await context.Response.Body.FlushAsync();

                _source.TrySetResult(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync(ErrorResponse);
                await context.Response.Body.FlushAsync();
            }
        }

        public Task<string> WaitForCallbackAsync()
        {
            Task.Run(async () =>
            {
                await Task.Delay(_defaultTimeout);
                _source.TrySetCanceled();
            });

            return _source.Task;
        }

        #region Static HTML Responses

        // https://codepen.io/warrendunlop/pen/YmVKzm

        private const string SuccessResponse = """
            <html>
             <head>
               <link href='https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap' rel='stylesheet'>
             </head>
             <style>
               body {
                 text-align: center;
                 padding: 40px 0;
                 background: #EBF0F5;
               }
               h1 {
                 color: #88B04B;
                 font-family: 'Nunito Sans', 'Helvetica Neue', sans-serif;
                 font-weight: 900;
                 font-size: 40px;
                 margin-bottom: 10px;
               }
               p {
                 color: #404F5E;
                 font-family: 'Nunito Sans', 'Helvetica Neue', sans-serif;
                 font-size:20px;
                 margin: 0;
               }
               i {
                 color: #9ABC66;
                 font-size: 100px;
                 line-height: 200px;
                 margin-left:-15px;
               }
               .card {
                 background: white;
                 padding: 60px;
                 border-radius: 4px;
                 box-shadow: 0 2px 3px #C8D0D8;
                 display: inline-block;
                 margin: 0 auto;
               }
             </style>
             <body>
               <div class='card'>
                 <div style='border-radius:200px; height:200px; width:200px; background: #F8FAF5; margin:0 auto;'>
                   <i class='checkmark'>&#x2713;</i>
                 </div>
                 <h1>Success</h1>
                 <p>You have successfully logged on.<br/>You can now return to the application.</p>
               </div>
             </body>
            </html>
            """;

        private const string ErrorResponse = """
             <html>
               <head>
                 <link href='https://fonts.googleapis.com/css?family=Nunito+Sans:400,400i,700,900&display=swap' rel='stylesheet'>
               </head>
               <style>
                 body {
                   text-align: center;
                   padding: 40px 0;
                   background: #EBF0F5;
                 }
                 h1 {
                   color: #b04b53;
                   font-family: 'Nunito Sans', 'Helvetica Neue', sans-serif;
                   font-weight: 900;
                   font-size: 40px;
                   margin-bottom: 10px;
                 }
                 p {
                   color: #404F5E;
                   font-family: 'Nunito Sans', 'Helvetica Neue', sans-serif;
                   font-size:20px;
                   margin: 0;
                 }
                 i {
                   color: #bc6666;
                   font-size: 100px;
                   line-height: 200px;
                   margin-left:-15px;
                 }
                 .card {
                   background: white;
                   padding: 60px;
                   border-radius: 4px;
                   box-shadow: 0 2px 3px #C8D0D8;
                   display: inline-block;
                   margin: 0 auto;
                 }
               </style>
               <body>
                 <div class='card'>
                   <div style='border-radius:200px; height:200px; width:200px; background: #F8FAF5; margin:0 auto;'>
                     <i class='exception'>!</i>
                   </div>
                   <h1>Error</h1> 
                   <p>Invalid request.</p>
                 </div>
               </body>
             </html>
             """;

        #endregion
    }
}
