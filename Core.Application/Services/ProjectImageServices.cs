using Core.Application.DTOs.Experience;
using Core.Application.DTOs.ProjectImage;
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
	public class ProjectImageServices : BaseServices<ProjectImage, ProjectImageDTO, SaveProjectImageDTO>, IProjectImageServices
    {
		private readonly IProjectImageRepository repo;
		private readonly IImageRepository imageRepository;

		public ProjectImageServices(IProjectImageRepository repo, IImageRepository imageRepository)
            : base(repo)
		{
			this.repo = repo;
			this.imageRepository = imageRepository;
		}

		public override async Task<AppResponse<Guid>> DeleteAsync(Guid Id)
		{
			var entity = await repo.GetByIdAsNoTrackingAsync(Id);
			imageRepository.DeleteImage(entity is null ? "" : entity.ImageUrl);
			return await base.DeleteAsync(Id);
		}

		public override async Task<AppResponse<ProjectImageDTO>> CreateAsync(SaveProjectImageDTO saveDto)
		{
			string? imageUrl = null;

			if (saveDto.ImageFile is not null)
			{
				imageUrl = await imageRepository
					.SaveImageAsync(saveDto.ImageFile, "Project", saveDto.ProjectId.ToString());
			}

			imageUrl ??= imageRepository.GetDefaultImageUrl("Project");

			var save = new SaveProjectImageDTO(
				ProjectId: saveDto.ProjectId,
				ImageFile: saveDto.ImageFile,
				ImageUrl: imageUrl 
			);

			return await base.CreateAsync(save);
		}

		public override async Task<AppResponse<ProjectImageDTO>> UpdateAsync(SaveProjectImageDTO saveDto, Guid Id)
		{
			string? imageUrl = saveDto.ImageUrl;

			if (saveDto.ImageFile is not null)
			{
				var savedUrl = await imageRepository
					.SaveImageAsync(
						saveDto.ImageFile,
						directoryEntity: "Project",
						id: saveDto.ProjectId.ToString(),
						oldImagePath: saveDto.ImageUrl);

				imageUrl = string.IsNullOrWhiteSpace(savedUrl)
					? saveDto.ImageUrl
					: savedUrl;
			}

			var updateDto = new SaveProjectImageDTO(
				ProjectId: saveDto.ProjectId,
				ImageFile: saveDto.ImageFile,
				ImageUrl: imageUrl
			);

			return await base.UpdateAsync(updateDto, Id);
		}

		public AppResponse<List<ProjectImageDTO>> GetAll(ProjectImageFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<ProjectImageDTO, ProjectImage>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<ProjectImageDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
