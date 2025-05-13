namespace Core.Application.DTOs.CommentReferences
{
    public record CommentReferencesDTO(
        Guid Id,
        string PersonName,
        string? ProfileImageUrl,
        string Comment,
        Guid ProfileId
    );
}
