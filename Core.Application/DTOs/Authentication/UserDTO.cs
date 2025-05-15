namespace Core.Application.DTOs.Authentication
{
	public record SaveUserDTO(string UserName, string Email, string Password, string ConfirmPassword, string Role);

	public record UserDTO(Guid Id, string Name, string Email, string Role);
}
