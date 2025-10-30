using System.Security.Cryptography;
using System.Text;

namespace PortiaNet.Helper.SecurityHelper
{
    public class EncryptionDecryptionHelper
    {

        private readonly string _key;

        public EncryptionDecryptionHelper(string key)
        {
            _key = key;
        }

        public string EncryptString(string text)
        {
            try
            {
                return Encrypt(text, _key);
            }
            catch (Exception ex)
            {
                throw new EncryptDecryptException("An error has been occurred when encrypting the value.", ex);
            }
        }

        public string DecryptString(string cipherText)
        {
            try
            {
                return Decrypt(cipherText, _key);
            }
            catch (Exception ex)
            {
                throw new EncryptDecryptException("An error has been occurred when decrypting the value.", ex);
            }
        }

        public static string Encrypt(string textToEncrypt, string key)
        {
            using var aes = Aes.Create();

            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var pwdBytes = Encoding.UTF8.GetBytes(key);
            var keyBytes = new byte[16];
            var len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            aes.Key = pwdBytes;
            aes.IV = keyBytes;
            var transform = aes.CreateEncryptor();
            var plainText = Encoding.UTF8.GetBytes(textToEncrypt);

            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }

        public static string Decrypt(string textToDecrypt, string key)
        {
            using var aes = Aes.Create();

            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var encryptedData = Convert.FromBase64String(textToDecrypt);
            var pwdBytes = Encoding.UTF8.GetBytes(key);
            var keyBytes = new byte[16];
            var len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            aes.Key = pwdBytes;
            aes.IV = keyBytes;
            var plainText = aes.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }
    }
}
