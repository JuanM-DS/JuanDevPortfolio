using Core.Application.DTOs.Authentication;
using Core.Application.Wrappers;

namespace Core.Application.Interfaces.Services
{
	public interface IUserServices
    {
		public Task<AppResponse<UserDTO>> UpdateAsync(SaveUserDTO updateUser, Guid Id);
		public Task<AppResponse<bool>> DeleteAsync(Guid userId);
        public Task<AppResponse<UserDTO?>> GetByEmailAsync(string email);
		public Task<AppResponse<List<UserDTO>>> GetAll();
	}
}
