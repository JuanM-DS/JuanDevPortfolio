using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.Authentication
{
	public record SaveUserDTO(string UserName, string Email, string Password, string ConfirmPassword, string Role, string FirstName, IFormFile imageFile);

	public record UserDTO(Guid Id, string Name, string Email, string Role, string ProfileImageUrl, string FirstName);
}
