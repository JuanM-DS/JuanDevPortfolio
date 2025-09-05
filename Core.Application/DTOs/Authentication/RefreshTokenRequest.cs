namespace Core.Application.DTOs.Authentication
{
	public record RefreshTokenRequest(string AccessToken, string RefreshToken);
}
