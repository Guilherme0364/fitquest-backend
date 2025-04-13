using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using FitQuest.Application.UseCases.Login.DoLogin;
using FitQuest.Communication.Requests;
using FitQuest.Domain.Entities;
using FluentAssertions;

namespace UseCases.Test.Login.DoLogin
{
    public class DoLoginUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            // Porque a função retorna dois valores
            (var user, var password) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(new RequestLoginJson
            {
                Email = user.Email,
                Password = user.Password
            });

            result.Should().NotBeNull();            
            result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);            
        }

        private static DoLoginUseCase CreateUseCase(FitQuest.Domain.Entities.User? user = null)
        {
            var passwordEncrypter = PasswordEncrypterBuilder.Build();
            var userReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();

            if (user is not null)
                userReadOnlyRepositoryBuilder.GetByEmailAndPassword(user);

            return new DoLoginUseCase(userReadOnlyRepositoryBuilder.Build(), passwordEncrypter);
        }
    }
}
