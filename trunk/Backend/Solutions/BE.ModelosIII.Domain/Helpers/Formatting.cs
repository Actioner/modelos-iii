using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE.ModelosIII.Domain.Helpers
{
    public static class Formatting
    {
        public static string ToBase64(this string source)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(source);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(this string source)
        {
            var base64EncodedBytes = Convert.FromBase64String(source);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
