using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;

namespace Core.Application.DTOs.Authentication
{
	public record SaveUserDTO(string Email, string Password, string ConfirmPassword, IImmutableList<string> Roles, string FirstName, IFormFile ImageFile);

	public record UserDTO(Guid Id, string Email, IImmutableList<string> Roles, string ProfileImageUrl, string FirstName);
}
