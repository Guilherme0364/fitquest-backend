using FitQuest.Application.Services.Criptography;
using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;
using FitQuest.Domain.Repositories.User;
using FitQuest.Exceptions.ExceptionsBase;

namespace FitQuest.Application.UseCases.Login.DoLogin
{
    public class DoLoginUseCase : IDoLoginUseCase
    {

        private readonly IUserReadOnlyRepository _repository;
        private readonly PasswordEncrypter _passwordEncrypter;

        public DoLoginUseCase(IUserReadOnlyRepository repository, PasswordEncrypter passwordEncrypter)
        {
            _repository = repository;
            _passwordEncrypter = passwordEncrypter;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var encryptedPassword = _passwordEncrypter.Encrypt(request.Password);

            var user = await _repository.GetByEmailAndPassword(request.Email, encryptedPassword) 
                ?? throw new InvalidLoginException(); // Se null, irá executar o código após as interrogações

            return new ResponseRegisteredUserJson
            {
                Name = user.Name
            };
        }
    }
}
