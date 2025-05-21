namespace Core.Application.QueryFilters
{
	public record WorkExperienceDetailFilter
	(
		string? Title,
		Guid? ExperienceId 
	);
}
