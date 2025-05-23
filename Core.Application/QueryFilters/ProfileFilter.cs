namespace Core.Application.QueryFilters
{
	public record ProfileFilter
	(
		string? ProfesionalTitle,
		Guid? AccountId
	);
}
