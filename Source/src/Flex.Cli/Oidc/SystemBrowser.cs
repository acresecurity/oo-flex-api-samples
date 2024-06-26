using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using IdentityModel.OidcClient.Browser;

namespace Flex.Oidc
{
    /// <summary>
    /// Sample provided by IdentityModel.OidcClient.Samples <see cref="https://github.com/IdentityModel/IdentityModel.OidcClient.Samples/blob/main/NetCoreConsoleClient/src/NetCoreConsoleClient/SystemBrowser.cs"/>
    /// </summary>
    internal class SystemBrowser : IBrowser
    {
        public int Port { get; }

        private readonly string _path;

        public SystemBrowser(int? port = null, string path = null)
        {
            _path = path;

            Port = port ?? GetRandomUnusedPort();
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            using var listener = new LoopbackHttpListener(Port, _path);

            OpenBrowser(options.StartUrl);

            try
            {
                var result = await listener.WaitForCallbackAsync();
                return string.IsNullOrWhiteSpace(result)
                    ? new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = "Empty response." }
                    : new BrowserResult { Response = result, ResultType = BrowserResultType.Success };
            }
            catch (TaskCanceledException ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.Timeout, Error = ex.Message };
            }
            catch (Exception ex)
            {
                return new BrowserResult { ResultType = BrowserResultType.UnknownError, Error = ex.Message };
            }
        }

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    Process.Start("xdg-open", url);
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    Process.Start("open", url);
                else
                    throw;
            }
        }
    }
}
