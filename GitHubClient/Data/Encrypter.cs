using System;
using System.Security.Cryptography;
using System.Text;

namespace GitHubClient.Data
{
    public class Encrypter
    {
        public string EncryptString(string value)
        {
            byte[] valueAsByteArray = Encoding.UTF8.GetBytes(value);
            byte[] protectedByteArray = ProtectedData.Protect(valueAsByteArray, null);
            string protectedString = Convert.ToBase64String(protectedByteArray);
            return protectedString;
        }

        public string DecryptString(string protectedBase64String)
        {
            byte[] protectedByteArray = Convert.FromBase64String(protectedBase64String);
            byte[] unProtectedByteArray = ProtectedData.Unprotect(protectedByteArray, null);
            string unProtectedString = Encoding.UTF8.GetString(unProtectedByteArray, 0, unProtectedByteArray.Length);
            return unProtectedString;
        }
    }
}
