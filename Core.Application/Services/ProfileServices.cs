using Core.Application.DTOs.Authentication;
using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Shared;
using Core.Application.Mappings;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
	public class ProfileServices : BaseServices<Profile, ProfileDTO, SaveProfileDTO>, IProfileServices
    {
		private readonly IProfileRepository repo;
		private readonly IHttpContextProvider httpContextProvider;
		private readonly IUserServices userServices;

		public ProfileServices(IProfileRepository repo, IHttpContextProvider httpContextProvider, IUserServices userServices)
            : base(repo)
		{
			this.repo = repo;
			this.httpContextProvider = httpContextProvider;
			this.userServices = userServices;
		}

		public async Task<AppResponse<List<ProfileDTO>>> GetAll(ProfileFilter filter)
		{
			var data = await Task.FromResult(repo.GetAll(filter).ToList());
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			var profilesWithUser = new List<ProfileDTO>();

			foreach (var item in dataDto!)
			{
				var userResponse = await userServices.GetByIdAsync(item.AccountId);
				if (userResponse.Data is null)
					break;

				item.User = userResponse.Data!;
				profilesWithUser.Add(item);
			}
			dataDto = profilesWithUser;

			return new(dataDto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<ProfileDTO?>> GetCurrentProfileAsync()
		{
			var currentId = httpContextProvider.GetCurrentUserId();

			var data = await repo.GetByAccountAsync(currentId ?? Guid.Empty);
			if (data is null)
				return new(HttpStatusCode.NoContent, "No se encontró ningún usuario en sesión.");

			var userResponse = await userServices.GetByIdAsync(data.AccountId);
			if (userResponse.Data is null)
				AppError.Create("No se encontró el usuario perteneciente al perfil solicitado.")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			dataDto.User = userResponse.Data!;
			return new(dataDto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<ProfileDTO?>> GetByAccountIdAsync(Guid id)
		{
			var data = await repo.GetByAccountAsync(id);
			if (data is null)
				return new(HttpStatusCode.NoContent, "No se encontro ningún perfil asociado al usuario");

			var userResponse = await userServices.GetByIdAsync(data.AccountId);
			if(userResponse.Data is null)
				AppError.Create("No se encontró el usuario perteneciente al perfil solicitado.")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			dataDto.User = userResponse.Data!;
			return new(dataDto, HttpStatusCode.OK);
		}

		public override async Task<AppResponse<List<ProfileDTO>>> GetAll()
		{
			var data = await base.GetAll();
			if (data.Data is null)
				return data;

			var profilesWithUser = new List<ProfileDTO>();

			foreach (var item in data.Data)
			{
				var userResponse = await userServices.GetByIdAsync(item.AccountId);
				if (userResponse.Data is null)
					break;

				item.User = userResponse.Data!;
				profilesWithUser.Add(item);
			}
			data.Data = profilesWithUser;
			return data;
		}

		public override async Task<AppResponse<ProfileDTO?>> GetByIdAsync(Guid Id)
		{
			var data = await base.GetByIdAsync(Id);
			if (data.Data is null)
				return data;

			var userResponse = await userServices.GetByIdAsync(data.Data.AccountId);
			if (userResponse.Data is null)
				AppError.Create("No se encontró el usuario perteneciente al perfil solicitado.")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			data.Data.User = userResponse.Data!;
			return data;
		}
	}
}
