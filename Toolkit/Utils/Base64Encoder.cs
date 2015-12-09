using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orasi.Toolkit.Utils
{ /// <summary>
  /// Class to help quickly encode and decode text
  /// </summary>
    class Base64Encoder
    {
        /// <summary>
        /// Quick helper method to encode text</summary>
        /// <param name="plainText">Text that you want to be encoded</param>
        /// <returns>Encoded string that is not plain text</returns>
        public static string Encode(String plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Quick helper method to decode text</summary>
        /// <param name="encodedString">Text that you want to be decoded</param>
        /// <returns>Decoded string that is plain text</returns>
        public static string Decode(String encodedString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedString);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
