using System.Globalization;
using System.Runtime.CompilerServices;

namespace FitQuest.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Pega todas as culturas suportadas pelo .NET
            var suportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures); 

            // Recuperar da requisição a cultura que o aplicativo solicitou
            var requestCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            // Trocar a cultura
            var cultureInfo = new CultureInfo("en");

            // Se o header não for e nulo ou vazio e o mesmo for uma cultura suportada pelo .NET (verificado na comparação)
            if (string.IsNullOrEmpty(requestCulture) == false
                && suportedLanguages.Any(c => c.Name.Equals(requestCulture)))
            {
                 cultureInfo = new CultureInfo(requestCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            // Permite continuar o fluxo de execução da API após interceptação da Middleware
            await _next(context);
        }
    }
}
