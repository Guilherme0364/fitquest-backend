using System.Reflection;
using FitQuest.Domain.Repositories;
using FitQuest.Domain.Repositories.User;
using FitQuest.Infraestructure.DataAccess;
using FitQuest.Infraestructure.Extension;
using FitQuest.Infraestruture.DataAccess;
using FitQuest.Infraestruture.DataAccess.Repositories;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitQuest.Infraestructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddRepositories(services);

            if (configuration.IsUnityTestEnviroment())
                return;

            AddDbContext_SqlServer(services, configuration);
            
            AddFluentMigration(services, configuration);
        }

        private static void AddDbContext_SqlServer(IServiceCollection services, IConfiguration configuration)
        {
            // O retorno da função "ConnectionString" é a string de conexão
            string connectionString = configuration.ConnectionString();

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

        private static void AddFluentMigration(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddFluentMigratorCore().ConfigureRunner(options =>
            {
                options
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.Load("FitQuest.Infraestructure")).For.All();
            });
        }
    }
}
