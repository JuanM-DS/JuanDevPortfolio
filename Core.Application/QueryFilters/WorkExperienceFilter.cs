namespace Core.Application.QueryFilters
{
	public record WorkExperienceFilter
	(
		string? Position,
		string? CompanyName,
		DateTime? FromDate,
		DateTime? ToDate,
		Guid? ProfileId
	);
}
