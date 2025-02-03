using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;
using FitQuest.Exceptions.ExceptionsBase;

namespace FitQuest.Application.UseCases.User.Register
{
    public class RegisterUserUseCase
    {
        public ResponseRegisteredUserJson Execute(RequestRegisterUserJson request)
        {
            // 1°: Validar a Request (Nome não é vazio, Email é um e-mail mesmo, senha contém os requisitos)
            Validate(request);

            // 2°: Mapear a Request em uma entidade (Uma Classe para receber os dados)
            // 3°: Criptografar da senha do usuário
            // 4°: Salvar no Banco de Dados

            return new ResponseRegisteredUserJson
            {
                Name = request.Name
            };

        }
        private void Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            // Retorna true se o resultado da validação der correto (os dados foram passados corretamente)
            if (result.IsValid == false) 
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList(); // Mapea o objeto iterável "e" (é o nosso erro) achando a propriedade que contém a mensagem de erro

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
