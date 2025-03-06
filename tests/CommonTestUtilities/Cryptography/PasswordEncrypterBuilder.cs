using FitQuest.Application.Services.Criptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncrypterBuilder
    {
        public static PasswordEncrypter Build()
        {
            return new PasswordEncrypter("abc123");
        }
    }
}
