using Core.Application.DTOs.Authentication;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
	public interface IAuthenticationServices 
    {
        public Task<AppResponse<UserDTO>> RegisterAsync(SaveUserDTO saveUser);

        public Task<AppResponse<string>> SignInAsync(LoginRequestDTO Login);

        public Task SignOutAsync();

        public Task<AppResponse<Empty>> ForgotPasswordAsync(ForgotPasswordRequestDTO request);

        public Task<AppResponse<Empty>> ResetPassword(ResetPasswordRequestDTO request);

        public Task<AppResponse<string>> GenerateResetToken();
	}
}
