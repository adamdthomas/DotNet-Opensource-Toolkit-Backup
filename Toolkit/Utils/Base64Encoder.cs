using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orasi.Toolkit.Utils
{
    class Base64Encoder
    {
        public static string encode(String plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string decode(String encodedString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
