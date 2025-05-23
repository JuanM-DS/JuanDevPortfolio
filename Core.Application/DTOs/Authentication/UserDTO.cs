using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.Authentication
{
	public record SaveUserDTO(string Email, string Password, string ConfirmPassword, string Role, string FirstName, IFormFile ImageFile);

	public record UserDTO(Guid Id, string Email, string Role, string ProfileImageUrl, string FirstName);
}
