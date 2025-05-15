namespace Core.Application.DTOs.Authentication
{
	public record ResetPasswordRequestDTO(string Account, string Password, string ConfirmPassword, string Token);
}
