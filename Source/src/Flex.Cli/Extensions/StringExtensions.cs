using System.Security.Cryptography;
using System.Text;

// ReSharper disable once CheckNamespace
namespace System
{
    internal static class StringExtensions
    {
        public static string ComputeHash(this string self)
        {
            if (string.IsNullOrEmpty(self))
                throw new ArgumentNullException(nameof(self));

            var data = MD5.HashData(Encoding.UTF8.GetBytes(self));
            var sb = new StringBuilder(64);
            foreach (var item in data)
                sb.Append(item.ToString("x2"));
            return sb.ToString();
        }
    }
}
