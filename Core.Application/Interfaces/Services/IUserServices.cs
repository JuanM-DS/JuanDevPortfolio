using Core.Application.DTOs.Authentication;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using Core.Domain.Enumerables;

namespace Core.Application.Interfaces.Services
{
	public interface IUserServices
    {
		public Task<AppResponse<UserDTO>> UpdateAsync(SaveUserDTO updateUser, Guid Id);
		public Task<AppResponse<Empty>> DeleteAsync(Guid userId);
        public Task<AppResponse<UserDTO?>> GetByEmailAsync(string email);
		public Task<AppResponse<UserDTO?>> GetByIdAsync(Guid Id);
		public Task<AppResponse<List<UserDTO>>> GetAll();
		public Task<AppResponse<Empty>> SetRolesToUser(List<string> Roles, Guid UserId);
	}
}
