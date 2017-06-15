using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DLL.EncryptAndDecrypt
{
    public class EncryptAndDecrypt
    {
        public string Encrypt(string str)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] pwd = System.Text.Encoding.Default.GetBytes(str);

            pwd = sha.ComputeHash(pwd);
          return System.Text.Encoding.Default.GetString(pwd);
        }
    }
}
