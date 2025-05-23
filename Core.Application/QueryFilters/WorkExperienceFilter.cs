namespace Core.Application.QueryFilters
{
	public record WorkExperienceFilter
	(
		string? Position = null,
		string? CompanyName = null,
		DateTime? FromDate = null,
		DateTime? ToDate = null,
		Guid? ProfileId = null
	);
}
