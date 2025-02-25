using FitQuest.Domain.Repositories;
using FitQuest.Domain.Repositories.User;
using FitQuest.Infraestructure.DataAccess;
using FitQuest.Infraestruture.DataAccess;
using FitQuest.Infraestruture.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitQuest.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext_SqlServer(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            // Sinal de exclamação no final para indicar que sabemos que o valor não será nulo
            string connectionString = configuration.GetConnectionString("ConnectionSqlServer")!;


            services.AddDbContext<FitQuestDbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
