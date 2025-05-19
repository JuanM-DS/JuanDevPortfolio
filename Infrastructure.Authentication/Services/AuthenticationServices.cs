using Core.Application.DTOs.Authentication;
using Core.Application.DTOs.Email;
using Core.Application.DTOs.Email.TempleteViewModels;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Enumerables;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Serilog;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Authentication.Services
{
	public class AuthenticationServices : IAuthenticationServices
	{
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signingManager;
		private readonly IEmailServices emailServices;
		private readonly IUriServices uriServices;

		public AuthenticationServices(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> SigningManager, IEmailServices EmailServices, IUriServices UriServices)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            signingManager = SigningManager;
			emailServices = EmailServices;
			uriServices = UriServices;
		}
		public async Task<AppResponse<UserDTO>> RegisterAsync(SaveUserDTO saveUser)
		{
			if(saveUser.Password != saveUser.ConfirmPassword)
				AppError.Create($"Las contraseñas no coinciden")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var userByName = userManager.FindByNameAsync(saveUser.UserName);
			var userByEmail= userManager.FindByEmailAsync(saveUser.Email);

			if ((await userByName) is not null)
				AppError.Create($"El userName: {saveUser.UserName} ya esta siendo utilizado")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			if ((await userByEmail) is not null)
				AppError.Create($"El email: {saveUser.Email} ya esta siendo utilizado")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var appUser = new AppUser() 
			{ 
				UserName = saveUser.UserName,
				Email = saveUser.Email,
				EmailConfirmed = true 
			};

			var result = await userManager.CreateAsync(appUser, saveUser.Role);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Information(item.Description);
				}
				AppError.Create($"Hubo un error al crear el usuario")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			result = await userManager.AddToRoleAsync(appUser, saveUser.Password);
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Information(item.Description);
				}
				AppError.Create($"Hubo un error al crear el usuario")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			var userDto = new UserDTO(appUser.Id, appUser.UserName, appUser.Email, saveUser.Role);
			return new(userDto, HttpStatusCode.Created);
		}

		public async Task<AppResponse<Empty>> ForgotPasswordAsync(ForgotPasswordRequestDTO request)
		{
			var user = IsEmailAccount(request.Account) switch
			{
				true => await userManager.FindByEmailAsync(request.Account),
				false => await userManager.FindByNameAsync(request.Account)
			};

			if (user is null)
				AppError.Create($"No se encontro ningun usuario con la cuenta: {request.Account}")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			var token = await userManager.GeneratePasswordResetTokenAsync(user!);
			var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

			var parameters = new Dictionary<string, string>()
			{
				{"Token", code}, 
				{"UserId", user!.Id.ToString()}
			};
			var finalUrl = uriServices.GetURL("RessetPassword", parameters);
			var viewModel = new ForgotPasswordViewModel(user.UserName!, finalUrl, DateTime.UtcNow.AddHours(1).ToString(), DateTime.UtcNow.Year);
			var emailRequest = new EmailRequestDTO(user!.Email!, "Cambiar Contraseña");
			var result = await emailServices.SendTemplateAsync(emailRequest, "ForgotPasswordEmail", viewModel);
			if(!result)
				AppError.Create($"Hubo un problema a la hora de enviar el correo")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			return new(HttpStatusCode.OK, "Correo para actualizar contraseña enviado correctamente");
		}


		public async Task<AppResponse<Empty>> ResetPassword(ResetPasswordRequestDTO request)
		{

			var user = await userManager.FindByIdAsync(request.UserId);
			if(user is null)
				AppError.Create($"Hubo un problema al verificar el usuario")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			if (request.Password != request.ConfirmPassword)
				AppError.Create($"Las contraseñas no coinciden")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

			var result = await userManager.ResetPasswordAsync(user!, token, request.Password);
			if(!result.Succeeded)
				foreach (var item in result.Errors)
				{
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Information(item.Description);
				}
				AppError.Create($"Hubo un error al cambiar la contraseña")
				.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
				.Throw();

			return new(HttpStatusCode.OK, "Se realizo el cambio de contraseña correctamente");
		}
		public Task<AppResponse<Empty>> SignInAsync(LoginRequestDTO Login)
		{
			throw new NotImplementedException();
		}

		public async Task SignOutAsync()
		{
			await signingManager.SignOutAsync();
		}

		private bool IsEmailAccount(string account)
		{
			var result =  Regex.Match(account, "^[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,}$\r\n");
			return result.Success;
		}

		Task<AppResponse<string>> IAuthenticationServices.SignInAsync(LoginRequestDTO Login)
		{
			throw new NotImplementedException();
		}

		public Task<AppResponse<string>> GenerateResetToken()
		{
			throw new NotImplementedException();
		}
	}
}
