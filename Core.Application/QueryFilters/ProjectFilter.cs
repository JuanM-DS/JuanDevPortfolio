namespace Core.Application.QueryFilters
{
	public record ProjectFilter
	(
		string? Title,
		Guid? ProfileId
	);
}
