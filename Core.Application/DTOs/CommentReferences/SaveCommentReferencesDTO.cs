namespace Core.Application.DTOs.CommentReferences
{
    public record SaveCommentReferencesDTO(
    string PersonName,
    string? ProfileImageUrl,
    string Comment,
    Guid ProfileId
    );
}
