using Core.Application.DTOs.Authentication;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IAccountServices 
    {
        public Task<AppResponse<UserDTO>> RegisterAsync(SaveUserDTO saveUser);

        public Task<AppResponse<SingInResponse>> SignInAsync(SignInRequestDTO Login);

        public Task SignOutAsync();

        public Task<AppResponse<Empty>> ForgotPasswordAsync(ForgotPasswordRequestDTO request);

        public Task<AppResponse<Empty>> ResetPassword(ResetPasswordRequestDTO request);

        public Task<AppResponse<SingInResponse>> RefreshTokenAsync(RefreshTokenRequest request);
		Task<AppResponse<Empty>> DeleteAllRefreshTokenByUser(Guid userId);
	}
}
