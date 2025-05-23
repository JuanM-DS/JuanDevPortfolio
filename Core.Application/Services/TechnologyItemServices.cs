using Core.Application.DTOs.TTechnologyItem;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Interfaces.Shared;
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
		private readonly IImageRepository imageRepository;

		public TechnologyItemServices(ITechnologyItemRepository repo, IImageRepository imageRepository)
            : base(repo)
		{
			this.repo = repo;
			this.imageRepository = imageRepository;
		}

		public override async Task<AppResponse<Guid>> DeleteAsync(Guid Id)
		{
			var entity = await repo.GetByIdAsNoTrackingAsync(Id);
			imageRepository.DeleteImage(entity is null ? "" : entity.ImageIconUrl);
			return await base.DeleteAsync(Id);
		}

		public override async Task<AppResponse<TechnologyItemDTO>> CreateAsync(SaveTechnologyItemDTO saveDto)
		{
			var itemByTitle = repo.GetAll(new TechnologyItemFilter { Title = saveDto.Name }).FirstOrDefault();
			if (itemByTitle is not null)
				AppError.Create($"Ya existe un Item con el nombre: {saveDto.Name}")
					.BuildResponse<TechnologyItemDTO>(HttpStatusCode.BadRequest)
					.Throw();

			string? imageUrl = null;
			if (saveDto.ImageFile is not null)
			{
				imageUrl = await imageRepository
					.SaveImageAsync(saveDto.ImageFile, "TechnologyItem", saveDto.Name);
			}
			imageUrl ??= imageRepository.GetDefaultImageUrl("TechnologyItem");
			var save = new SaveTechnologyItemDTO(saveDto.Name, imageUrl, saveDto.ImageFile, saveDto.Description, saveDto.LevelType);

			return await base.CreateAsync(save);
		}

		public override async Task<AppResponse<TechnologyItemDTO>> UpdateAsync(SaveTechnologyItemDTO saveDto, Guid Id)
		{
			var itemByTitle = repo.GetAll(new TechnologyItemFilter { Title = saveDto.Name }).FirstOrDefault();
			if (itemByTitle is not null && itemByTitle.Id != Id)
				AppError.Create($"Ya existe un Item con el nombre: {saveDto.Name}")
					.BuildResponse<TechnologyItemDTO>(HttpStatusCode.BadRequest)
					.Throw();

			string? imageUrl = saveDto.ImageIconUrl;
			if (saveDto.ImageFile is not null)
			{
				var savedUrl = await imageRepository
					.SaveImageAsync(saveDto.ImageFile, "TechnologyItem", saveDto.Name, saveDto.ImageIconUrl);

				imageUrl = string.IsNullOrWhiteSpace(savedUrl)
					? saveDto.ImageIconUrl
					: savedUrl;
			}
			var save = new SaveTechnologyItemDTO(saveDto.Name, imageUrl, saveDto.ImageFile, saveDto.Description, saveDto.LevelType);

			return await base.UpdateAsync(save, Id);
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
