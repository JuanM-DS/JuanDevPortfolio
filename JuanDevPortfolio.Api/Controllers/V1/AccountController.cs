using Asp.Versioning;
using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class AccountController : BaseController
	{
		private readonly IAccountServices accountServices;

		public AccountController(IAccountServices AccountServices)
		{
			accountServices = AccountServices;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(SaveUserDTO Save)
		{
			var response = await accountServices.RegisterAsync(Save);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(SignInAsync))]
		public async Task<IActionResult> SignInAsync(SignInRequestDTO SignIn)
		{
			var response = await accountServices.SignInAsync(SignIn);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(SignOutAsync))]
		public async Task<IActionResult> SignOutAsync()
		{
			await accountServices.SignOutAsync();
			return Ok();
		}

		[HttpPost]
		[Route(nameof(ForgotPasswordAsync))]
		public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestDTO ForgotPassword)
		{
			var response = await accountServices.ForgotPasswordAsync(ForgotPassword);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(ResetPasswordAsync))]
		public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestDTO ResetPassword)
		{
			var response = await accountServices.ResetPassword(ResetPassword);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
