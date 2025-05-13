using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Application.Services
{
    public class BaseServices<TEntity, TEntityDto, SaveTEntityDto> : IBaseServices<TEntity, TEntityDto, SaveTEntityDto>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        protected readonly IBaseRepository<TEntity> _repo;

        public BaseServices(IBaseRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public Task<AppResponse<TEntityDto>> CreateAsync(SaveTEntityDto saveDto)
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

        public Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<AppResponse<TEntityDto>> UpdateAsync(SaveTEntityDto saveDto)
        {
            throw new NotImplementedException();
        }
    }

    public class CommentReferencesServices: BaseServices<CommentReferences, CommentReferencesDTO, SaveCommentReferencesDTO>, ICommentReferencesServices
    {
        public CommentReferencesServices(ICommentReferencesRepository repo)
            : base(repo)
        {}
    }
}
