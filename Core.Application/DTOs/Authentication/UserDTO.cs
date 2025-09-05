using Core.Domain.Enumerables;
using Microsoft.AspNetCore.Http;

namespace Core.Application.DTOs.Authentication
{
	public record SaveUserDTO(string Email, string Password, string ConfirmPassword, string FirstName, IFormFile? ImageFile);

	public record UserDTO(Guid Id, string Email, List<string> Roles, string ProfileImageUrl, string FirstName);
}
