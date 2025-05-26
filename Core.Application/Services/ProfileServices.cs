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

		public ProfileServices(IProfileRepository repo, IHttpContextProvider httpContextProvider)
            : base(repo)
		{
			this.repo = repo;
			this.httpContextProvider = httpContextProvider;
		}

		public AppResponse<List<ProfileDTO>> GetAll(ProfileFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<ProfileDTO?>> GetCurrentProfileAsync()
		{
			var currentId = httpContextProvider.GetCurrentUserId();

			var data = await repo.GetByAccountAsync(currentId ?? Guid.Empty);
			if (data is null)
				return new(HttpStatusCode.NoContent, "No se encontró ningún usuario en sesión.");

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			return new(dataDto, HttpStatusCode.OK);
		}

		public async Task<AppResponse<ProfileDTO?>> GetByAccountIdAsync(Guid id)
		{
			var data = await repo.GetByAccountAsync(id);
			if (data is null)
				return new(HttpStatusCode.NoContent, "No se encontro ningún perfil asociado al usuario");

			var dataDto = Mapper.Map<ProfileDTO, Profile>(data);
			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
