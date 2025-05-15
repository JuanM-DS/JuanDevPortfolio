namespace Core.Application.DTOs.Authentication
{
	public record ConfirmAccountRequestDTO(Guid UserId, string Token);
}
