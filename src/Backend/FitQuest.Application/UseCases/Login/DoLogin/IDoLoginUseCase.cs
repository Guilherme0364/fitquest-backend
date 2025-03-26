using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;

namespace FitQuest.Application.UseCases.Login.DoLogin
{
    public interface IDoLoginUseCase
    {
        public Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
    }
}
