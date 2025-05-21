using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public class ProfileServices : BaseServices<Profile, ProfileDTO, SaveProfileDTO>, IProfileServices
    {
		private readonly IProfileRepository repo;

		public ProfileServices(IProfileRepository repo, IMapper mapper)
            : base(repo, mapper)
		{
			this.repo = repo;
		}

		public AppResponse<List<ProfileDTO>> GetAll(ProfileFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = _mapper.Map<ProfileDTO, Profile>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProfileDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
