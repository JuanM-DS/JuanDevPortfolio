using Asp.Versioning;
using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

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
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "Receives the necessary credentials to register a user in the application"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.OK, "The registration was successful")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "The client sent invalid information for user registration")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred during register")]
		[SwaggerResponse((int)HttpStatusCode.UnsupportedMediaType, "Invalid content type")]
		public async Task<IActionResult> RegisterAsync([FromForm]SaveUserDTO Save)
		{
			var response = await accountServices.RegisterAsync(Save);
			return StatusCode((int)response.HttpStatusCode, response);
		}


		[HttpPost]
		[Route(nameof(SignInAsync))]
		[SwaggerOperation(
			Summary = "Sign in a user",
			Description = "Receives the credentials and returns a token if authentication is successful"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Sign in successful")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid sign in credentials")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred during sign in")]
		public async Task<IActionResult> SignInAsync(SignInRequestDTO SignIn)
		{
			var response = await accountServices.SignInAsync(SignIn);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[Authorize]
		[HttpPost]
		[Route(nameof(SignOutAsync))]
		[SwaggerOperation(
			Summary = "Sign out a user",
			Description = "Terminates the user's session or token"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "User signed out successfully")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while signing out")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "You do not have permission to access this endpoint")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while registering the user")]
		public async Task<IActionResult> SignOutAsync()
		{
			await accountServices.SignOutAsync();
			return Ok();
		}

		[HttpPost]
		[Route(nameof(ForgotPasswordAsync))]
		[SwaggerOperation(
			Summary = "Forgot password",
			Description = "Sends a reset password link to the user email"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Reset password link sent successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid email address")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while processing the request")]
		public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequestDTO ForgotPassword)
		{
			var response = await accountServices.ForgotPasswordAsync(ForgotPassword);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(ResetPasswordAsync))]
		[SwaggerOperation(
			Summary = "Reset password",
			Description = "Receives the reset token and new password to update the user credentials"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Password reset successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid reset token or password")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while resetting the password")]
		public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequestDTO ResetPassword)
		{
			var response = await accountServices.ResetPassword(ResetPassword);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
