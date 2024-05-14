using System;
using System.Security.Cryptography;
using System.Text;

namespace test_base
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string originalText = "SUDHARSUN";

            // Encrypt the text
            string encryptedText = Encrypt(originalText);

            // Encode the encrypted text
            string encodedText = Encode(encryptedText);

            // Decode the encoded text
            string decodedText = Decode(encodedText);

            // Decrypt the decoded text
            string decryptedText = Decrypt(decodedText);

            // Print the decrypted text
            Response.Write(decryptedText);
        }

        public static string Encrypt(string text)
        {
            using (Aes encerp = Aes.Create())
            {
                encerp.Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
                encerp.IV = Encoding.UTF8.GetBytes("abcdef0123456789");
                ICryptoTransform encryptor = encerp.CreateEncryptor(encerp.Key, encerp.IV);

                byte[] encryptedBytes;

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }
                        encryptedBytes = msEncrypt.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string Decrypt(string encryptedText)
        {
            using (Aes decryp = Aes.Create())
            {
                decryp.Key = Encoding.UTF8.GetBytes("0123456789abcdef0123456789abcdef");
                decryp.IV = Encoding.UTF8.GetBytes("abcdef0123456789");
                ICryptoTransform decryptor = decryp.CreateDecryptor(decryp.Key, decryp.IV);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                string decryptedText = null;

                using (var msDecrypt = new System.IO.MemoryStream(encryptedBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            decryptedText = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return decryptedText;
            }
        }

        public static string Encode(string text)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(textBytes);
        }

        public static string Decode(string encodedText)
        {
            byte[] encodedBytes = Convert.FromBase64String(encodedText);
            return Encoding.UTF8.GetString(encodedBytes);
        }
    }
}


