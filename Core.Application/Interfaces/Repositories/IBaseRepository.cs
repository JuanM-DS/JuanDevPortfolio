using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Application.Interfaces.Repositories
{
	public interface IBaseRepository<TEntity>
		where TEntity : BaseEntity
	{
		public Task<bool> CreateAsync(TEntity entity);
		public Task<bool> UpdateAsync(TEntity entity);
		public Task<bool> DeleteAsync(Guid id);
		public Task<TEntity?> GetByIdAsync(Guid id);
		public IEnumerable<TEntity> GetAll();
		public Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>> [] properties);
		public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] properties);
	}
}
