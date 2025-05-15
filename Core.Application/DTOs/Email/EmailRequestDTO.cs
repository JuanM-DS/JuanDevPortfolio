namespace Core.Application.DTOs.Email
{
    public record EmailRequestDTO(string To, string Subject, string UserName);
}
