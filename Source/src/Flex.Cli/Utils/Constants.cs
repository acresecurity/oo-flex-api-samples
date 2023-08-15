
namespace Flex.Utils
{
    internal static class Constants
    {
        public static readonly string ValidationErrorHeader = "[red]Validation Failed[/]";

        #region Command Line Exit Codes

        public static readonly int CommandLineSuccess = 0;

        public static readonly int CommandLineGeneralError = 1;

        public static readonly int CommandLineCancelled = 2;

        public static readonly int CommandLineClientValidationError = 3;

        public static readonly int CommandLineServerValidationError = 4;

        public static readonly int CommandLineCacheError = 5;

        public static readonly int CommandLineRequestError = 400;

        public static readonly int CommandLineInsufficientPermission = 403;

        public static readonly int CommandLineUnhandledException = 500;

        #endregion

        #region Caching

        public static readonly TimeSpan DefaultCacheTime = TimeSpan.FromMinutes(5);

        public static readonly string CacheKeyTokens = "Tokens";

        public static readonly string CacheKeyUserInfo = "UserInfo";

        public static readonly string CacheKeyEventDescriptions = "Event Descriptions";

        public static readonly string CacheKeySettings = "Settings";

        #endregion
    }
}
