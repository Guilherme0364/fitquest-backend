using System.Security.Cryptography;
using System.Text;

namespace FitQuest.Application.Services.Criptography
{
    public class PasswordEncrypter
    {
        private readonly string _additionalKey;

        public PasswordEncrypter(string additionalKey) 
        {
            _additionalKey = additionalKey;
        }

        public string Encrypt(string password)
        {
            // Interpolação de uma chave adicional com a senha original para dar mais segurança            

            var newPassword = $"{password}{_additionalKey}";

            // Criptografa a senha à nivel de bytes
            var bytes = Encoding.UTF8.GetBytes(newPassword);
            var hashBytes = SHA512.HashData(bytes);
            
            return ByteToString(hashBytes);
        }

        private static string ByteToString(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach(byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex); // Vai colocando os caracteres em sequência
            }
            return sb.ToString();
        }
    }
}
