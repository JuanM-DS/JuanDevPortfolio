using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Enumerables;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Net;
using System.Text.RegularExpressions;

namespace Infrastructure.Authentication.Services
{
	public class AuthenticationServices : IAuthenticationServices
	{
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signingManager;

        public AuthenticationServices(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> SigningManager)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            signingManager = SigningManager;
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

			var result = await userManager.CreateAsync(appUser, saveUser.Password);
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

		public Task<AppResponse<Empty>> ForgotPasswordAsync(ForgotPasswordRequestDTO request)
		{
			throw new NotImplementedException();
		}


		public Task<AppResponse<Empty>> ResetPassword(ResetPasswordRequestDTO request)
		{
			throw new NotImplementedException();
		}

		public Task<AppResponse<Empty>> SignInAsync(LoginRequestDTO Login)
		{
			throw new NotImplementedException();
		}

		public Task<AppResponse<Empty>> SignOutAsync()
		{
			throw new NotImplementedException();
		}

		private bool IsEmailAccount(string account)
		{
			var result =  Regex.Match(account, "^[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,}$\r\n");
			return result.Success;
		}
	}
}
