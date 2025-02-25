using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;

namespace FitQuest.Application.UseCases.User.Register
{
    public interface IRegisterUserUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
    }
}
