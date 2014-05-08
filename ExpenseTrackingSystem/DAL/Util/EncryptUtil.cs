using System;
using System.Security.Cryptography;
using System.Text;

namespace DAL.Util
{
    public static class EncryptUtil
    {
        public static string EncodePassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] hash = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
