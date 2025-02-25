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
        public async Task<IActionResult> Register(
            [FromServices]IRegisterUserUseCase useCase, // Recebendo o UseCase por Injeção de Dependência (dos serviços de injeção)
            [FromBody]RequestRegisterUserJson request) // Recebendo a Request do Body da requisição
        {            
            var result = await useCase.Execute(request);

            return Created(string.Empty, result); // Created recebe 0 ou 2 argumentos          
        }
    }
}
