using System.Text;

namespace Tasko.Cryptography.Extensions
{
    public static class AesConfiguration
    {
        public static byte[] Key { get { return Encoding.UTF8.GetBytes("da983189246a4520a94764a751fa466a"); } }
        public static byte[] IV { get { return new byte[16]; } }
    }
    public static class AesExtension
    {
        public static string Encrypt(this string plainText, byte[] key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0) throw new ArgumentNullException(nameof(key));
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException(nameof(IV));

            byte[] encrypted;
            using (var aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new System.Security.Cryptography.CryptoStream(msEncrypt, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public static string Decrypt(this string cipherText, byte[] key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0) throw new ArgumentNullException(nameof(key));
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException(nameof(IV));
            string plaintext = null;
            using (var aesAlg = System.Security.Cryptography.Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = IV;

                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var csDecrypt = new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

    }
}
