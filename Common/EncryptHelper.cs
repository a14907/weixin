using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class EncryptHelper
    {
        public static string Sha1Encrypt(string text)
        {
            byte[] cleanBytes = Encoding.Default.GetBytes(text);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(cleanBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}
