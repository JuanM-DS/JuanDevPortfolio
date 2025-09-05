using Core.Application.DTOs.Authentication;
using Core.Application.DTOs.Email;
using Core.Application.DTOs.Email.TempleteViewModels;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Shared;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Enumerables;
using Core.Domain.Settings;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Serilog;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Authentication.Services
{
	public class AccountServices : IAccountServices
	{
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<AppRole> roleManager;
        private readonly SignInManager<AppUser> signingManager;
		private readonly IEmailServices emailServices;
		private readonly IUriServices uriServices;
		private readonly IHttpContextProvider httpContextProvider;
		private readonly IImageRepository imageRepository;
		private readonly ITokenServices tokenServices;

		public AccountServices(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> SigningManager, IEmailServices EmailServices, IUriServices UriServices, IHttpContextProvider HttpContextProvider, IImageRepository imageRepository, ITokenServices TokenServices)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            signingManager = SigningManager;
			emailServices = EmailServices;
			uriServices = UriServices;
			httpContextProvider = HttpContextProvider;
			this.imageRepository = imageRepository;
			tokenServices = TokenServices;
		}
		
		public async Task<AppResponse<UserDTO>> RegisterAsync(SaveUserDTO saveUser)
		{
			if(saveUser.Password != saveUser.ConfirmPassword)
				AppError.Create($"Las contraseñas no coinciden")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var userByEmail= userManager.FindByEmailAsync(saveUser.Email);
			if ((await userByEmail) is not null)
				AppError.Create($"El email: {saveUser.Email} ya esta siendo utilizado")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			string? imageUrl = null;
			if (saveUser.ImageFile is not null)
			{
				imageUrl = await imageRepository
					.SaveImageAsync(saveUser.ImageFile, "User", saveUser.Email);
			}
			imageUrl ??= imageRepository.GetDefaultImageUrl("User");

			var appUser = new AppUser(saveUser.FirstName, imageUrl) 
			{
				Email = saveUser.Email,
				EmailConfirmed = true,
				UserName = saveUser.Email
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

			result = await userManager.AddToRoleAsync(appUser, RoleType.Basic.ToString());
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
			var roles = appUser.Roles.Select(x =>x.Role.ToString()).ToList();
			var userDto = new UserDTO(appUser.Id, appUser.Email, roles, appUser.ProfileImageUrl, appUser.FirstName);
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
				AppError.Create($"No se encontró ningún usuario con la cuenta: {request.Account}")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			var token = await userManager.GeneratePasswordResetTokenAsync(user!);
			var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

			var parameters = new Dictionary<string, string>()
			{
				{"Token", code}, 
				{"UserId", user!.Id.ToString()}
			};
			var finalUrl = uriServices.GetURL("Account/RessetPassword", parameters);
			var viewModel = new ForgotPasswordViewModel(user.FirstName!, finalUrl, DateTime.UtcNow.AddHours(1).ToString(), DateTime.UtcNow.Year);
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
			if (!result.Succeeded)
			{
				foreach (var item in result.Errors)
				{
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true).Information(item.Description);
				}
				AppError.Create($"Hubo un error al cambiar la contraseña")
				.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
				.Throw();
			}
			await tokenServices.DeleteRefreshTokensByUserAsync(user!.Id);
			return new(HttpStatusCode.OK, "Se realizo el cambio de contraseña correctamente");
		}

		public async Task SignOutAsync()
		{
			var currentIp = httpContextProvider.GetUserIpAddress();
			await tokenServices.DeleteRefreshTokensByIpAsync(currentIp);
			await signingManager.SignOutAsync();
		}

		public async Task<AppResponse<SingInResponse>> SignInAsync(SignInRequestDTO Login)
		{
			var user = IsEmailAccount(Login.Account) switch
			{
				true => await userManager.FindByEmailAsync(Login.Account),
				false => await userManager.FindByNameAsync(Login.Account)
			};

			if(user is null)
				AppError.Create($"No se encontró ningún usuario con la cuenta: {Login.Account}")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			var result = await signingManager.CheckPasswordSignInAsync(user!, Login.Password, false);
			if(!result.Succeeded)
				AppError.Create($"Hubo un error al iniciar sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var accessToken = await tokenServices.GenerateAccessJwtToken(user!);
			var refreshToken = await tokenServices.GenerateRefreshToken(user!.Id);

			var response = new SingInResponse(accessToken, refreshToken);
			return new(response, HttpStatusCode.OK, "Se ha Iniciado sesión correctamente");
		}

		public async Task<AppResponse<SingInResponse>> RefreshTokenAsync(RefreshTokenRequest request)
		{
			var userName = httpContextProvider.GetCurrentUserId();
			var user = await userManager.FindByIdAsync(userName.ToString() ?? "");
			if(user is null)
				AppError.Create($"No existe ningún usuario en sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			if (!tokenServices.IsAccessTokenValid(request.AccessToken))
			{
				AppError.Create($"Favor Volver a iniciar sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			if(!await tokenServices.IsRefreshTokenValid(request.RefreshToken))
			{
				await tokenServices.DeleteRefreshTokenAsync(request.RefreshToken);
				AppError.Create($"Favor Volver a iniciar sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			var accessToken = await tokenServices.GenerateAccessJwtToken(user!);
			var refreshToken = await tokenServices.UpdateRefreshTokenAsync(request.RefreshToken);
			if (string.IsNullOrEmpty(refreshToken))
			{
				AppError.Create($"hubo un problema al refrescar el token")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			var response = new SingInResponse(accessToken, refreshToken!);
			return new(response, HttpStatusCode.OK, "Se ha generado un nuevo token correctamente");
		}

		public async Task<AppResponse<Empty>> DeleteAllRefreshTokenByUser(Guid userId)
		{
			var result = await tokenServices.DeleteRefreshTokensByUserAsync(userId);
			if (result)
				AppError.Create("Hubo un problema a la hora de eliminar los tokens")
								.BuildResponse<Empty>(HttpStatusCode.BadRequest).Throw();

			return new(HttpStatusCode.OK);
		}

		#region Privates
		private bool IsEmailAccount(string account)
		{
			var result =  Regex.Match(account, @"^[\w\.-]+@[\w\.-]+\.\w{2,}$");
			return result.Success;
		}
		#endregion
	}
}
