using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Application.Services
{
    public class BaseServices<TEntity, TEntityDto> : IBaseServices<TEntity, TEntityDto>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        private readonly IBaseRepository<TEntity> _repo;

        public BaseServices(IBaseRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public Task<AppResponse<TEntityDto>> CreateAsync(TEntityDto saveDto)
        {
            throw new NotImplementedException();
        }

        public Task<AppResponse<Guid>> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public AppResponse<List<TEntityDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public AppResponse<List<TEntityDto>> GetAllWithInclude(params Expression<Func<TEntity, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id, params Expression<Func<TEntity, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public Task<AppResponse<TEntityDto>> UpdateAsync(TEntityDto saveDto)
        {
            throw new NotImplementedException();
        }
    }
}
