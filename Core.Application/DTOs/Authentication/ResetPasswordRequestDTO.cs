namespace Core.Application.DTOs.Authentication
{
	public record ResetPasswordRequestDTO(string UserId, string Password, string ConfirmPassword, string Token);
}
