using System;
using System.Collections.Generic;
using System.Linq;

namespace IFlexV1_Sample
{
    public static class StringExtension
    {
        /// <summary>
        /// An implementation of the Contains member of string that takes in a 
        /// string comparison. The traditional .NET string Contains member uses 
        /// StringComparison.Ordinal.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="value">The string value to search for.</param>
        /// <param name="comparison">The string comparison type.</param>
        /// <returns>Returns true when the substring is found.</returns>
        public static bool Contains(this string s, string value, StringComparison comparison)
        {
            return s.IndexOf(value, comparison) >= 0;
        }

        public static byte[] ToByteArray(this string self)
        {
            if (string.IsNullOrEmpty(self))
                return new byte[0];

            string[] values = self.Split(' ');
            byte[] result = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                int element = System.Convert.ToInt32(values[i], 16);
                if (element >= 0 && element <= 255)
                    result[i] = System.Convert.ToByte(element);
                else
                    result[i] = 0x0;
            }
            return result;
        }

        public static List<byte> ToByteList(this string self)
        {
            return new List<byte>(self.ToByteArray());
        }
    }
}
