using Core.Domain.Enumerables;

namespace Core.Domain.Entities
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; }
	}
}
