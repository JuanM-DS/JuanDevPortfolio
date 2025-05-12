using Core.Application.Wrappers;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace JuanDevPortfolio.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new AppResponse();

            if(exception is AppException)
            {
                var appException = (AppException)exception;

                response = appException.AppResponse;
            }
            else
            {
                response = AppError.Create(exception.Message).BuildResponse<object>(HttpStatusCode.InternalServerError, message: "Hubo un error en el servidor");
            }

            httpContext.Response.StatusCode = (int)response.HttpStatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken:cancellationToken);
            return true;
        }
    }
}
