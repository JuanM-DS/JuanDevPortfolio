namespace Core.Application.DTOs.CommentReferences
{
    public record CommentReferencesDTO(
    string PersonName,
    string? ProfileImageUrl,
    string Comment,
    Guid ProfileId
    );
}
