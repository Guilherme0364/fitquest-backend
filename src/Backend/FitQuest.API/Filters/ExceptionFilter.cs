using System;
using System.Net;
using FitQuest.Communication.Response;
using FitQuest.Exceptions;
using FitQuest.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitQuest.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {            
            if(context.Exception is FitQuestException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowException(context);
            }
        }

        private static void HandleProjectException(ExceptionContext context)
        {            
            if(context.Exception is ErrorOnValidationException exception)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; // Cast de Enum para Int
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exception!.ErrorMessages));
            }
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Cast de Enum para Int
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR)); // Objeto para erros internos
        }

    }
}
