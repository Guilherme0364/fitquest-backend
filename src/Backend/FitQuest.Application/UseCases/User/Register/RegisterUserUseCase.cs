using AutoMapper;
using FitQuest.Application.Services.AutoMapper;
using FitQuest.Application.Services.Criptography;
using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;
using FitQuest.Domain.Repositories;
using FitQuest.Domain.Repositories.User;
using FitQuest.Exceptions;
using FitQuest.Exceptions.ExceptionsBase;
using FitQuest.Infraestructure.DataAccess;

namespace FitQuest.Application.UseCases.User.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IUserWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;
        private readonly PasswordEncrypter _passwordEncrypter;
        private readonly IMapper _mapper;
        

        public RegisterUserUseCase(
            IUserReadOnlyRepository readOnlyRepository, 
            IUserWriteOnlyRepository writeOnlyRepository,
            PasswordEncrypter passwordEncrypter,
            IUnityOfWork unityOfWork,
            IMapper mapper)
        {
            _writeOnlyRepository = writeOnlyRepository;
            _readOnlyRepository = readOnlyRepository;
            _passwordEncrypter = passwordEncrypter;
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
        {            
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _passwordEncrypter.Encrypt(request.Password);

            await _writeOnlyRepository.Add(user);

            await _unityOfWork.Commit();

            return new ResponseRegisteredUserJson
            {
                Name = user.Name
            };
        }

        private async Task Validate(RequestRegisterUserJson request)
        {
            var validator = new RegisterUserValidator();

            var result = validator.Validate(request);

            var emailExist = await _readOnlyRepository.ExistisActiveUserWithEmail(request.Email);

            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_EXISTS));
            }

            // Se existe um erro o IsValid se torna falso
            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
