namespace Core.Application.DTOs.CommentReferences
{
    public record CommentReferenceDTO(
        Guid Id,
        string Comment,
        Guid ProfileId,
        string PersonName = "",
        string ProfileImageUrl = ""
    );
}
