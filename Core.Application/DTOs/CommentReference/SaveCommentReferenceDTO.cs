namespace Core.Application.DTOs.CommentReferences
{
    public record SaveCommentReferenceDTO(
    string PersonName,
    string? ProfileImageUrl,
    string Comment,
    Guid ProfileId
    );
}
