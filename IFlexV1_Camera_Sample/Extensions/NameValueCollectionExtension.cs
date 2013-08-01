using System;
using System.Linq;
using System.Web;

namespace System.Collections.Specialized
{
    public static class NameValueCollectionExtension
    {
        public static string ToQueryString(this NameValueCollection self)
        {
            return string.Join("&",
                Array.ConvertAll(self.AllKeys,
                    key => string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(self[key]))));
        }
    }
}
