using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using EncurtadorLink.Shared.Library.Services.Interfaces;

namespace EncurtadorLink.Shared.Library.Services
{
    public class Criptografador : ICriptografador
    {
        private readonly string _cryptoKey;

        public Criptografador(IConfiguration configuration)
        {
            _cryptoKey = configuration["CryptoKey"]!;
        }

        public CryptoStream EncryptStream(Stream responseStream)
        {
            Aes aes = GetEncryptionAlgorithm();
            ToBase64Transform base64Transform = new ToBase64Transform();
            CryptoStream base64EncodedStream = new CryptoStream(responseStream, base64Transform, CryptoStreamMode.Write);
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(base64EncodedStream, encryptor, CryptoStreamMode.Write);
            return cryptoStream;
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] encrypted;
            using (Aes aes = GetEncryptionAlgorithm())
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

        public Stream DecryptStream(Stream cipherStream)
        {
            Aes aes = GetEncryptionAlgorithm();
            FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
            CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
            return decryptedStream;
        }

        public string DecryptString(string cipherText)
        {
            Aes aes = GetEncryptionAlgorithm();
            byte[] buffer = Convert.FromBase64String(cipherText);
            MemoryStream memoryStream = new MemoryStream(buffer);
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }

        public bool TokenTemporarioValido(string tempToken)
        {
            var dataString = DecryptString(tempToken);
            var dataToken = Convert.ToDateTime(dataString);

            return dataToken >= BrasilDateTime().AddMinutes(-5);
        }

        private DateTime BrasilDateTime()
        {
            TimeZoneInfo brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone);
        }

        private Aes GetEncryptionAlgorithm()
        {
            Aes aes = Aes.Create();
            var secret_key = Encoding.UTF8.GetBytes(_cryptoKey);
            var initialization_vector = Encoding.UTF8.GetBytes(_cryptoKey);
            aes.Key = secret_key;
            aes.IV = initialization_vector;
            return aes;
        }
    }
}
