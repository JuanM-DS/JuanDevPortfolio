using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Mappings;
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

        public BaseServices(IBaseRepository<TEntity> repo)
        {
            _repo = repo;
        }

        public virtual async Task<AppResponse<TEntityDto>> CreateAsync(SaveTEntityDto saveDto)
        {
            var entity = Mapper.Map<TEntity, SaveTEntityDto>(saveDto);
            if (entity is null)
                AppError.Create("Hubo problemas al mappear la entidad")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var result = await _repo.CreateAsync(entity!);
            if(result)
                AppError.Create("Hubo problemas al crear la entidad.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var entityDto = Mapper.Map<TEntityDto, TEntity>(entity!);
            if (entity is null)
                AppError.Create("Hubo problemas al mappear la entidad creada")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(entityDto, HttpStatusCode.Created);
        }

        public virtual async Task<AppResponse<Guid>> DeleteAsync(Guid Id)
        {
            var entity = await _repo.GetByIdAsync(Id);
            if (entity is null)
                AppError.Create($"No existe una entidad asociada al Id: {Id}")
                    .BuildResponse<Guid>(HttpStatusCode.BadRequest)
                    .Throw();

            var result = await _repo.DeleteAsync(entity!);
            if (result)
                AppError.Create("Hubo problemas al eliminar la entidad.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(Id, HttpStatusCode.OK);
        }

        public virtual AppResponse<List<TEntityDto>> GetAll()
        {
            var data = _repo.GetAll().ToList();
            if (data is null || !data.Any())
                return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

            var dataDto = Mapper.Map<TEntityDto, TEntity>(data);
            if(dataDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(dataDto, HttpStatusCode.OK);
        }

        public virtual async Task<AppResponse<TEntityDto?>> GetByIdAsync(Guid Id)
        {
            var data = await _repo.GetByIdAsync(id:Id);
            if (data is null)
                return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

            var dataDto = Mapper.Map<TEntityDto, TEntity>(data);
            if (dataDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(dataDto, HttpStatusCode.OK);
        }

        public virtual async Task<AppResponse<TEntityDto>> UpdateAsync(SaveTEntityDto saveDto)
        {
            var entity = Mapper.Map<TEntity, SaveTEntityDto>(saveDto);
            if(entity is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var result = await _repo.UpdateAsync(entity!);
            if(result)
                AppError.Create("Hubo problemas al actualizar la entidad.")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            var entityDto = Mapper.Map<TEntityDto, TEntity>(entity!);
            if(entityDto is null)
                AppError.Create("Hubo problemas al mappear la request")
                    .BuildResponse<TEntityDto>(HttpStatusCode.InternalServerError)
                    .Throw();

            return new(entityDto, HttpStatusCode.OK);
        }
    }
}
