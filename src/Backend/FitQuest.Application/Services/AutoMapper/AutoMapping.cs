using System.Text;
using AutoMapper;
using FitQuest.Communication.Requests;

namespace FitQuest.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
        }

       /* Funcionamento do método `RequestToDomain`:
        1. O método `RequestToDomain` é chamado dentro do construtor da classe `AutoMapping`, o que garante que as configurações definidas sejam aplicadas assim que a classe for instanciada.
        2. O método configura um mapeamento entre a classe `RequestRegisterUserJson` (que representa uma requisição recebida pela API) e a classe `Domain.Entities.User` (que pertence ao domínio da aplicação).
            - `CreateMap<RequestRegisterUserJson, Domain.Entities.User>()`: Define que um objeto do tipo `RequestRegisterUserJson` pode ser mapeado automaticamente para um objeto do tipo `Domain.Entities.User`.
            - `.ForMember(dest => dest.Password, opt => opt.Ignore())`: Especifica que a propriedade `Password` do objeto de destino(`Domain.Entities.User`) não será preenchida durante o processo de mapeamento.Isso pode ser útil para evitar o mapeamento de valores sensíveis ou indesejados.
        3. O método é utilizado para garantir que os dados de entrada (requisição) sejam transformados de maneira controlada em uma entidade de domínio, que será usada nas regras de negócio da aplicação.
       */

        private void RequestToDomain()
        {            
            CreateMap<RequestRegisterUserJson, Domain.Entities.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }

        private void DomainToResponse()
        {

        }
    }
}
