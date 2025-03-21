using Microsoft.Extensions.Configuration;

namespace FitQuest.Infraestructure.Extension
{
    // Irá evitar duplicidade no código de recuperação da Connection String
    public static class ConfigurationExtension
    {
        public static bool IsUnityTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("InMemomryTest");
        }

        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("ConnectionSqlServer")!;            
        }
    }
}
