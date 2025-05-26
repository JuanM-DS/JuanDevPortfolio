using Core.Application.Wrappers;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace JuanDevPortfolio.Api.Middlewares
{
	public class ValidatorFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if (!context.ModelState.IsValid)
			{
				List<AppError> errors = context.ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.SelectMany(kvp => kvp.Value.Errors
					.Select(error => AppError.Create(error.ErrorMessage, kvp.Key)))
					.ToList();

				if (errors.Any())
				{
					context.Result = new BadRequestObjectResult(errors.BuildResponse<Empty>(HttpStatusCode.BadRequest, "Hubieron errores de validacion"));
					return;
				}
			}
			await next();
		}
	}
}
