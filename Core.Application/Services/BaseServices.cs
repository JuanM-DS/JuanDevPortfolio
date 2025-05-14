using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Helpers;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
    public class BaseServices<TEntity, TEntityDto, SaveTEntityDto> : IBaseServices<TEntity, TEntityDto, SaveTEntityDto>
        where TEntity : BaseEntity
        where TEntityDto : class
    {
        protected readonly IBaseRepository<TEntity> _repo;
        protected readonly IMapper _mapper;

        public BaseServices(IBaseRepository<TEntity> repo, IMapper Mapper)
        {
            _repo = repo;
            _mapper = Mapper;
        }

        public async Task<AppResponse<TEntityDto>> CreateAsync(SaveTEntityDto saveDto)
        {
            var entity = _mapper.Map<TEntity, SaveTEntityDto>(saveDto);
            if (entity is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var result = await _repo.CreateAsync(entity!);
            if(result)
                AppError.Create("Hubo problemas al crear la entidad, revisar los log para ver detalles.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var entityDto = _mapper.Map<TEntityDto, TEntity>(entity!);
            if (entity is null)
                AppError.Create("Hubo problemas al mappear la entidad creada")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(entityDto, HttpStatusCode.Created);
        }

        public async Task<AppResponse<Guid>> DeleteAsync(Guid Id)
        {
            var entity = await _repo.GetByIdAsync(Id);
            if (entity is null)
                AppError.Create($"No existe una entidad asociada al Id: {Id}")
                    .BuildResponse<Guid>(HttpStatusCode.BadRequest)
                    .Throw();

            var result = await _repo.DeleteAsync(entity!);
            if (result)
                AppError.Create("Hubo problemas al eliminar la entidad, revisar los log para ver detalles.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(Id, HttpStatusCode.OK);
        }

        public AppResponse<List<TEntityDto>> GetAll()
        {
            var data = _repo.GetAll().ToList();
            if (data is null || !data.Any())
                return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

            var dataDto = _mapper.Map<TEntityDto, TEntity>(data);
            if(dataDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(dataDto, HttpStatusCode.OK);
        }

        public async Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id)
        {
            var data = await _repo.GetByIdAsync(id:Id);
            if (data is null)
                return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

            var dataDto = _mapper.Map<TEntityDto, TEntity>(data);
            if (dataDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(dataDto, HttpStatusCode.OK);
        }

        public async Task<AppResponse<TEntityDto>> UpdateAsync(SaveTEntityDto saveDto)
        {
            var entity = _mapper.Map<TEntity, SaveTEntityDto>(saveDto);
            if(entity is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var result = await _repo.UpdateAsync(entity!);
            if(result)
                AppError.Create("Hubo problemas al actualizar la entidad, revisar los log para ver detalles.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var entityDto = _mapper.Map<TEntityDto, TEntity>(entity!);
            if(entityDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(entityDto, HttpStatusCode.OK);
        }
    }

    public class CommentReferencesServices: BaseServices<CommentReferences, CommentReferencesDTO, SaveCommentReferencesDTO>, ICommentReferencesServices
    {
        public CommentReferencesServices(ICommentReferencesRepository repo, IMapper Mapper)
            : base(repo, Mapper)
        {}
    }
}
