using FitQuest.Communication.Requests;
using FluentValidation;
using FitQuest.Exceptions;

namespace FitQuest.Application.UseCases.User.Register
{
    public  class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson> // Essa classe será uma validadora da Classe tipada na notação
    {
        public RegisterUserValidator()
        {
            // Cria uma regra para o usuário onde o nome dele não pode ser vazio
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY); 

            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);

            // Verifica se a senha possui mais de 6 caracteres
            RuleFor(user => user.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_INVALID);

            // Se o email do usuário não for nulo ou vazio, como verificado na função da "classe" string
            When(user => string.IsNullOrEmpty(user.Email) == false, () =>
            {
                // Verifica se o email é um email de fato
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
            });
        }
    }
}
