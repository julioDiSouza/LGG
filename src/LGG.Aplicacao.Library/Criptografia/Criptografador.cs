using System.Text;
using System.Security.Cryptography;

namespace LGG.Aplicacao.Library.Criptografia
{
    public static class Criptografador
    {
        public static CryptoStream EncryptStream(Stream responseStream, string cryptoKey)
        {
            Aes aes = GetEncryptionAlgorithm(cryptoKey);
            ToBase64Transform base64Transform = new ToBase64Transform();
            CryptoStream base64EncodedStream = new CryptoStream(responseStream, base64Transform, CryptoStreamMode.Write);
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(base64EncodedStream, encryptor, CryptoStreamMode.Write);
            return cryptoStream;
        }

        public static Stream DecryptStream(Stream cipherStream, string cryptoKey)
        {
            Aes aes = GetEncryptionAlgorithm(cryptoKey);
            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
            return decryptedStream;
        }

        public static byte[] Encrypt(string plainText, string cryptoKey)
        {
            byte[] encrypted;
            using (Aes aes = GetEncryptionAlgorithm(cryptoKey))
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string EncryptString(string plainText, string cryptoKey)
        {
            string encrypted;
            using (Aes aes = GetEncryptionAlgorithm(cryptoKey))
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        var bytes = ms.ToArray();
                        encrypted = Convert.ToBase64String(bytes);
                    }
                }
            }
            return encrypted;
        }

        public static string DecryptString(string cipherText, string cryptoKey)
        {
            Aes aes = GetEncryptionAlgorithm(cryptoKey);
            byte[] buffer = Convert.FromBase64String(cipherText);
            MemoryStream memoryStream = new MemoryStream(buffer);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        #region ..:: Privado ::..

        private static Aes GetEncryptionAlgorithm(string cryptoKey)
        {
            Aes aes = Aes.Create();
            var secret_key = Encoding.UTF8.GetBytes(cryptoKey);
            var initialization_vector = Encoding.UTF8.GetBytes(cryptoKey);
            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }

        #endregion ..:: Fim - Privado ::..

    }
}
