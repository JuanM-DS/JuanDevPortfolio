namespace Core.Application.DTOs.CommentReferences
{
    public record CommentReferenceDTO(
        Guid Id,
        string PersonName,
        string? ProfileImageUrl,
        string Comment,
        Guid ProfileId
    );
}
