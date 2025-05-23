using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Shared;
using Core.Application.Wrappers;
using Core.Domain.Enumerables;
using Infrastructure.Authentication.CustomEntities;
using Infrastructure.Authentication.Interfaces;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Collections.Immutable;
using System.Net;

namespace Infrastructure.Authentication.Services
{
	public class UserServices : IUserServices
	{
		private readonly IUserRepository userRepository;
		private readonly UserManager<AppUser> userManager;
		private readonly IImageRepository imageRepository;

		public UserServices(IUserRepository userRepository, UserManager<AppUser> userManager, IImageRepository ImageRepository)
		{
			this.userRepository = userRepository;
			this.userManager = userManager;
			imageRepository = ImageRepository;
		}

		public async Task<AppResponse<List<UserDTO>>> GetAll()
		{
			var data = userRepository.GetAll();
			if (data is null)
				return new(HttpStatusCode.NoContent);

			var dtoTasks = data.Select(async x =>
			{
				var dto = new UserDTO(
					Id: x!.Id,
					Email: x.Email!,
					Roles: (await userManager.GetRolesAsync(x)).Select(r => r.ToString()).ToImmutableList(),
					ProfileImageUrl: x.ProfileImageUrl,
					FirstName: x.FirstName
				);
				return dto;
			});

			var dtos = await Task.WhenAll(dtoTasks); 

			return new(dtos.ToList(), HttpStatusCode.OK);
		}


		public async Task<AppResponse<UserDTO?>> GetByEmailAsync(string email)
		{
			var appUser = await userManager.FindByEmailAsync(email);
			if (appUser is null)
				return new(HttpStatusCode.NoContent);


			var dto = new UserDTO(
				Id: appUser!.Id,
				Email: appUser.Email!,
				Roles: (await userManager.GetRolesAsync(appUser)).Select(r => r.ToString()).ToImmutableList(),
				ProfileImageUrl: appUser.ProfileImageUrl,
				FirstName: appUser.FirstName
			);

			return new(dto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<UserDTO>> UpdateAsync(SaveUserDTO updateUser, Guid Id)
		{
			var appUser = await userManager.FindByIdAsync(Id.ToString());
			if (appUser is null)
				AppError.Create($"Usuario con Id {Id} no encontrado")
					.BuildResponse<UserDTO>(HttpStatusCode.NotFound)
					.Throw();

			if (!string.Equals(appUser?.Email, updateUser.Email, StringComparison.OrdinalIgnoreCase))
			{
				var byEmail = await userManager.FindByEmailAsync(updateUser.Email);
				if (byEmail is not null)
					AppError.Create($"El email {updateUser.Email} ya está en uso")
						.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
						.Throw();

				appUser!.Email = updateUser.Email;
				appUser.EmailConfirmed = false;
			}

			if (!string.IsNullOrEmpty(updateUser.Password))
			{
				if (updateUser.Password != updateUser.ConfirmPassword)
					AppError.Create("Las contraseñas no coinciden")
						.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
						.Throw();

				var token = await userManager.GeneratePasswordResetTokenAsync(appUser!);
				var passResult = await userManager.ResetPasswordAsync(appUser!, token, updateUser.Password);
				if (!passResult.Succeeded)
				{
					foreach (var e in passResult.Errors)
						Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true)
						   .Information(e.Description);

					AppError.Create("Error al actualizar la contraseña")
						.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
						.Throw();
				}
			}

			if (updateUser.ImageFile is not null)
			{
				var savedUrl = await imageRepository
					.SaveImageAsync(updateUser.ImageFile, "User", appUser?.Email!, appUser?.ProfileImageUrl);

				appUser!.ProfileImageUrl = string.IsNullOrWhiteSpace(savedUrl)
					? appUser.ProfileImageUrl
					: savedUrl;
			}

			var updateResult = await userManager.UpdateAsync(appUser!);
			if (!updateResult.Succeeded)
			{
				foreach (var e in updateResult.Errors)
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true)
					   .Information(e.Description);

				AppError.Create("Error al actualizar el usuario")
					.BuildResponse<UserDTO>(HttpStatusCode.BadRequest)
					.Throw();
			}

			var dto = new UserDTO(
				Id: appUser!.Id,
				Email: appUser.Email!,
				Roles: (await userManager.GetRolesAsync(appUser)).Select(x=>x.ToString()).ToImmutableList(),
				ProfileImageUrl: appUser.ProfileImageUrl,
				FirstName: appUser.FirstName
			);

			return new(dto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<bool>> DeleteAsync(Guid userId)
		{
			var appUser = await userManager.FindByIdAsync(userId.ToString());
			if (appUser is null)
				AppError.Create($"Usuario con Id {userId} no encontrado")
					.BuildResponse<bool>(HttpStatusCode.NotFound)
					.Throw();

			if (!string.IsNullOrWhiteSpace(appUser!.ProfileImageUrl))
				imageRepository.DeleteImage(appUser.ProfileImageUrl);

			var result = await userManager.DeleteAsync(appUser);
			if (!result.Succeeded)
			{
				foreach (var e in result.Errors)
					Log.ForContext(LoggerKeys.AuthenticationLogs.ToString(), true)
					   .Information(e.Description);

				AppError.Create("Error al eliminar el usuario")
					.BuildResponse<bool>(HttpStatusCode.BadRequest)
					.Throw();
			}

			return new(true, HttpStatusCode.NoContent);
		}
	}
}
