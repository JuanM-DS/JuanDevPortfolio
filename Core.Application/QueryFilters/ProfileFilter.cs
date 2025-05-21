namespace Core.Application.QueryFilters
{
	public record ProfileFilter
	(
		string? Name,
		string? ProfesionalTitle,
		Guid? AccountId
	);
}
