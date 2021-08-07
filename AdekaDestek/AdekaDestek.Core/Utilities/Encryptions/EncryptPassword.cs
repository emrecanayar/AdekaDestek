using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdekaDestek.Core.Utilities.Encryptions
{
    //ASP.NET Identity için Custom Password algoritması -- Not = Eski sistemde bulunan kullanıcı şifreleriyle uyumlu olması için oluşturulmuştur.
    public static class EncryptPassword
    {
        public static string GetMD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
