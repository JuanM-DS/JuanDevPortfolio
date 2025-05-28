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
using Infrastructure.Authentication.Context;
using Infrastructure.Authentication.CustomEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
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
		private JwtSettings jwtSettings;

		public AccountServices(UserManager<AppUser> UserManager, RoleManager<AppRole> RoleManager, SignInManager<AppUser> SigningManager, IEmailServices EmailServices, IUriServices UriServices, IOptions<JwtSettings> JwtSettings, IHttpContextProvider HttpContextProvider, IImageRepository imageRepository)
        {
            userManager = UserManager;
            roleManager = RoleManager;
            signingManager = SigningManager;
			emailServices = EmailServices;
			uriServices = UriServices;
			httpContextProvider = HttpContextProvider;
			this.imageRepository = imageRepository;
			jwtSettings = JwtSettings.Value;
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

			result = await userManager.AddToRolesAsync(appUser, saveUser.Roles);
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

			var userDto = new UserDTO(appUser.Id, appUser.Email, saveUser.Roles, appUser.ProfileImageUrl, appUser.FirstName);
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

		public async Task SignOutAsync()
		{
			await signingManager.SignOutAsync();
		}

		public async Task<AppResponse<string>> SignInAsync(SignInRequestDTO Login)
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

			var result = await signingManager.PasswordSignInAsync(user!, Login.Password, false, false);
			if(result.Succeeded)
				AppError.Create($"Hubo un error al iniciar sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var token = await GenerateJwtTokenAsync(user!);

			return new(token, HttpStatusCode.OK, "Se ha Iniciado sesión correctamente");
		}

		public async Task<AppResponse<string>> GenerateResetTokenAsync()
		{
			var userName = httpContextProvider.GetCurrentUserId();
			var user = await userManager.FindByIdAsync(userName.ToString() ?? "");
			if(userName is null)
				AppError.Create($"No existe ningún usuario en sesión")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();

			var token = await GenerateJwtTokenAsync(user!);
			return new(token, HttpStatusCode.OK, "Se ha generado un nuevo token correctamente");
		}

		#region Privates
		private bool IsEmailAccount(string account)
		{
			var result =  Regex.Match(account, @"^[\w\.-]+@[\w\.-]+\.\w{2,}$");
			return result.Success;
		}
		private async Task<string> GenerateJwtTokenAsync(AppUser user)
		{
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.ScretKey));
			var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.Sha256);
			var header = new JwtHeader(credentials);

			var userClaims = await userManager.GetClaimsAsync(user);
			var roleClaims = (await userManager.GetRolesAsync(user)).Select(x => new Claim(ClaimTypes.Role, x));
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email!),
				new Claim("UserId", user.Id.ToString())
			}
			.Union(userClaims).Union(roleClaims);

			var payload = new JwtPayload
				(
				jwtSettings.Issuer,
				jwtSettings.Audience,
				claims,
				DateTime.UtcNow,
				DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes)
				);

			var token = new JwtSecurityToken(header, payload);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		#endregion
	}
}
