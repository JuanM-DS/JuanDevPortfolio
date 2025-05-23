using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IBaseServices<TEntity, TEntityDto, SaveTEntityDto>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        public Task<AppResponse<TEntityDto>> CreateAsync(SaveTEntityDto saveDto);

        public Task<AppResponse<TEntityDto>> UpdateAsync(SaveTEntityDto saveDto, Guid Id);

        public Task<AppResponse<Guid>> DeleteAsync(Guid Id);

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id);

        public AppResponse<List<TEntityDto>> GetAll();
    }
}
