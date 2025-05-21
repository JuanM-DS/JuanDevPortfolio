using Core.Application.DTOs.Profile;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IProfileServices : IBaseServices<Profile, ProfileDTO, SaveProfileDTO>
    {
		public AppResponse<List<ProfileDTO>> GetAll(ProfileFilter filter);
	}
}
