using FitQuest.Application.Services.AutoMapper;
using FitQuest.Application.Services.Criptography;
using FitQuest.Application.UseCases.Login.DoLogin;
using FitQuest.Application.UseCases.User.Register;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitQuest.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEncrypter(services, configuration);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
            {
                options.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        }

        private static void AddPasswordEncrypter(IServiceCollection services, IConfiguration configuration)
        {
            // GetValue pois queremos o valor do tipo string não um objeto do tipo IConfiguration
            var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");

            // Sinal de exclamação no final para indicar que sabemos que o valor não será nulo
            services.AddScoped(option => new PasswordEncrypter(additionalKey!));
        }

    }
}
