using System.Security.Cryptography;
using System.Text;

namespace MyHealth.Api.Utils
{
    public class Cryptography
    {
        public static string ComputeSha256Hash(string pRawData)
        {
            if (string.IsNullOrEmpty(pRawData))
                return null;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(pRawData));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool ValidPassword(string pInputPassword, string pPasswordHash)
        {
            var inputHashPassword = ComputeSha256Hash(pInputPassword);
            return inputHashPassword.Equals(pPasswordHash);
        }
    }
}
