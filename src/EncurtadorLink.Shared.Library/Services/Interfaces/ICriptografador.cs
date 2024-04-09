using System.Security.Cryptography;

namespace EncurtadorLink.Shared.Library.Services.Interfaces
{
    public interface ICriptografador
    {
        CryptoStream EncryptStream(Stream responseStream);
        byte[] Encrypt(string plainText);
        Stream DecryptStream(Stream cipherStream);
        string DecryptString(string cipherText);
        bool TokenTemporarioValido(string tempToken);
    }
}