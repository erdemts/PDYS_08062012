using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
    public static class CyrptoTool
    {
        public const string CyrptoKey = "XrtyPkj8";

        public static string CyrptoText(string decyrptoText)
        {
            return CyrptoText(decyrptoText, CyrptoKey);
        }


        public static string DeCryptoText(string cyrptoText)
        {
            return DeCryptoText(cyrptoText, CyrptoKey);
        }

        public static string CyrptoText(string decyrptoText, string Key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(decyrptoText);
            byte[] Salt = Encoding.ASCII.GetBytes(Key.Length.ToString());


            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Key, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            memoryStream.Dispose();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;

        }

        public static string DeCryptoText(string cyrptoText, string Key)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            byte[] EncryptedData = Convert.FromBase64String(cyrptoText);
            byte[] Salt = Encoding.ASCII.GetBytes(Key.Length.ToString());


            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Key, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

            memoryStream.Close();
            memoryStream.Dispose();
            cryptoStream.Close();

            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            return DecryptedData;
        }


    }
}
