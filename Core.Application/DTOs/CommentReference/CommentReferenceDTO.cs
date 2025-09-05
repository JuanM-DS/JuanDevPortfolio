namespace Core.Application.DTOs.CommentReferences
{
    public record CommentReferenceDTO(
        Guid Id,
        string Comment,
        Guid ProfileId,
		Guid AccountId,
        string PersonName = "",
        string ProfileImageUrl = ""
    );
}
