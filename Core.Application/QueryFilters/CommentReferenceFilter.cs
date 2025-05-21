namespace Core.Application.QueryFilters
{
	public record CommentReferenceFilter
	(
		Guid? ProfileId,
		bool? IsConfirmed
	);
}
