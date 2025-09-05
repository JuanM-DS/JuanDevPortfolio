namespace Core.Application.DTOs.Authentication
{
	public record SingInResponse(string AccessToken, string RefreshToken);
}
