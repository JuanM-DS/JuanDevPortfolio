using Core.Application.DTOs.Profile;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IProfileServices : IBaseServices<Profile, ProfileDTO, SaveProfileDTO>
    {
		public Task<AppResponse<List<ProfileDTO>>> GetAll(ProfileFilter filter);
		public Task<AppResponse<ProfileDTO?>> GetCurrentProfileAsync();
		public Task<AppResponse<ProfileDTO?>> GetByAccountIdAsync(Guid id);
	}
}
