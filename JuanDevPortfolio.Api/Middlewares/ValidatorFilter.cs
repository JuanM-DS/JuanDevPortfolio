using Core.Application.Wrappers;
using Core.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

public class ValidationMiddleware : IAsyncActionFilter
{
	private readonly IServiceProvider provider;

	public ValidationMiddleware(IServiceProvider provider)
	{
		this.provider = provider;
	}
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var errors = new List<AppError>();

		foreach (var item in context.ActionArguments)
		{
			var type = item.Value?.GetType();
			if (type is null)
				return;

			var validatorType = typeof(IValidator<>).MakeGenericType(type);
			var validator = provider.GetRequiredService(validatorType) as IValidator;
			if (validator is null)
				return;

			var validationContext = new ValidationContext<object>(item.Value!);
			var validResult = await validator.ValidateAsync(validationContext);
			if (!validResult.IsValid)
				errors.AddRange(validResult.Errors.Select(x => AppError.Create(x.ErrorMessage, x.PropertyName)));
		}

		if (errors.Any())
		{
			context.Result = new BadRequestObjectResult(errors.BuildResponse<Empty>(HttpStatusCode.BadRequest, "Hubieron errores de validación"));
			return;
		}

		await next();
	}
}
