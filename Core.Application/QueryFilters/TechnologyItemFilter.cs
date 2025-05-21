using Core.Domain.Enumerables;

namespace Core.Application.QueryFilters
{
	public class TechnologyItemFilter
	{
		public string? Title { get; set; } 
		public LevelsTypes? LevelType { get; set; }
		public List<Guid>? Ids { get; set; }
	}
}
