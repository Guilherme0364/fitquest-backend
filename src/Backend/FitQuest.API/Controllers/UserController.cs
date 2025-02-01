using FitQuest.Application.UseCases.User.Register;
using FitQuest.Communication.Requests;
using FitQuest.Communication.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitQuest.API.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        public IActionResult Register(RequestRegisterUserJson request) 
        {
            var UseCase = new RegisterUserUseCase();

            var result = UseCase.Execute(request);

            return Created(string.Empty ,result); // Created recebe 0 ou 2 argumentos
        }
    }
}
