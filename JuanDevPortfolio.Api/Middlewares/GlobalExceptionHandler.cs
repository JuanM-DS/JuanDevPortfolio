using Core.Application.Wrappers;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
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
                Log.Error(exception.Message);
                response = AppError.Create("Hubo un error de aplicación, favor comunicarse con el administrador").BuildResponse<object>(HttpStatusCode.InternalServerError, message: "Hubo un error en el servidor");
            }

            httpContext.Response.StatusCode = (int)response.HttpStatusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken:cancellationToken);
            return true;
        }
    }
}
