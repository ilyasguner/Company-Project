using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    internal class SecurityProcess//C# kriptolama methotları
    {
        static SecurityProcess securityProcess;

        public string Encrypt(string eKey,string value)//kriptolama methodumuz
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashMD5Provier = new MD5CryptoServiceProvider();

            byte[] bytHash = hashMD5Provier.ComputeHash(Encoding.UTF8.GetBytes(eKey));
            tripleDESCryptoServiceProvider.Key = bytHash;
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            byte[] data = Encoding.UTF8.GetBytes(value);

            return Convert.ToBase64String(tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(data, 0, data.Length));
        }

        public string Decrypt(string dKey,string value)//şifreyi çözme methodumuz
        {
            TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();

            MD5CryptoServiceProvider hashMD5Provier = new MD5CryptoServiceProvider();

            byte[] bytHash = hashMD5Provier.ComputeHash(Encoding.UTF8.GetBytes(dKey));
            tripleDESCryptoServiceProvider.Key = bytHash;
            tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
            byte[] data = Convert.FromBase64String(value);

            return Encoding.UTF8.GetString(tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(data, 0, data.Length));
        }

        public static SecurityProcess GetInstance()
        {
            if (securityProcess==null)
            {
                securityProcess = new SecurityProcess();
            }
            return securityProcess;
        }
    }
}
