using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using FitQuest.Application.UseCases.User.Register;
using FitQuest.Exceptions;
using FitQuest.Exceptions.ExceptionsBase;
using FluentAssertions;

namespace UseCases.Test.User.Register
{
    public class RegisterUserUseCaseTest
    {
        [Fact]
        public async Task Success()
        {            
            var request = RequestRegisterUserJsonBuilder.Build();

            // Pega o retorno da função privada como o UseCase já com todas as intancias necessárias
            var useCase = CreateUseCase();

            var result = await useCase.Execute(request);

            result.Should().NotBeNull();
            result.Name.Should().Be(request.Name);
        }

        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var useCase = CreateUseCase(request.Email);

            // Aqui é guardado a função dentro da variável
            Func<Task> act = async () => await useCase.Execute(request);
            
            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(error => error.ErrorMessages.Count == 1 && error.ErrorMessages.Contains(ResourceMessagesException.EMAIL_ALREADY_EXISTS));

        }

        [Fact]
        public async Task Error_Name_Empty()
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase();

            // Aqui é guardado a função dentro da variável
            Func<Task> act = async () => await useCase.Execute(request);

            (await act.Should().ThrowAsync<ErrorOnValidationException>())
                .Where(error => error.ErrorMessages.Count == 1 && error.ErrorMessages.Contains(ResourceMessagesException.NAME_EMPTY));

        }

        // Função que irá devolver a instanciação de todos os mocks necessários para funções da classe RegisterUserUseCase
        private RegisterUserUseCase CreateUseCase(string? email = null) // Parâmetro opcional
        {
            var mapper = MapperBuilder.Build();
            var passwordEncrypter = PasswordEncrypterBuilder.Build();
            var unityOfWork = UnityOfWorkBuilder.Build();
            var writeOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
            var readOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();


            if(string.IsNullOrEmpty(email) == false)
            {
                readOnlyRepositoryBuilder.ExistActiveUserWithEmail(email);
            }

            return new RegisterUserUseCase(readOnlyRepositoryBuilder.Build(), writeOnlyRepository, passwordEncrypter, unityOfWork, mapper);
        }
    }
}
