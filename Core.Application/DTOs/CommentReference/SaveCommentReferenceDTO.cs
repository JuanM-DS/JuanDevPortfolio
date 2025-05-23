namespace Core.Application.DTOs.CommentReferences
{
	public record SaveCommentReferenceDTO(
	Guid AccountId,
	string Comment,
    Guid ProfileId
    );
}
