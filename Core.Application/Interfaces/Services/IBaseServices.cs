using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Application.Interfaces.Services
{
    public interface IBaseServices<TEntity, TEntityDto>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        public Task<AppResponse<TEntityDto>> CreateAsync(TEntityDto saveDto);

        public Task<AppResponse<TEntityDto>> UpdateAsync(TEntityDto saveDto);

        public Task<AppResponse<Guid>> DeleteAsync(Guid Id);

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id);

        public AppResponse<List<TEntityDto>> GetAll();

        public AppResponse<List<TEntityDto>> GetAllWithInclude(params Expression<Func<TEntity, object>> [] properties);

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id, params Expression<Func<TEntity, object>>[] properties);
    }
}
