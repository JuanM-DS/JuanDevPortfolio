using Core.Application.DTOs.TTechnologyItem;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Mappings;
using Core.Application.QueryFilters;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System.Net;

namespace Core.Application.Services
{
	public class TechnologyItemServices : BaseServices<TechnologyItem, TechnologyItemDTO, SaveTechnologyItemDTO>, ITechnologyItemServices
    {
		private readonly ITechnologyItemRepository repo;

		public TechnologyItemServices(ITechnologyItemRepository repo)
            : base(repo)
		{
			this.repo = repo;
		}

		public AppResponse<List<TechnologyItemDTO>> GetAll(TechnologyItemFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<TechnologyItemDTO, TechnologyItem>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<TechnologyItemDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
