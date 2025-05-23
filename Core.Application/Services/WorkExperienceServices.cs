using Core.Application.DTOs.Experience;
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
	public class WorkExperienceServices : BaseServices<WorkExperience, WorkExperienceDTO, SaveWorkExperienceDTO>, IWorkExperienceServices
    {
		private readonly IWorkExperienceRepository repo;
		private readonly ITechnologyItemRepository technologyItemRepo;
		private readonly IImageRepository imageRepository;

		public WorkExperienceServices(IWorkExperienceRepository repo, ITechnologyItemRepository TechnologyItemRepo, IImageRepository imageRepository)
            : base(repo)
		{
			this.repo = repo;
			technologyItemRepo = TechnologyItemRepo;
			this.imageRepository = imageRepository;
		}

		public override async Task<AppResponse<Guid>> DeleteAsync(Guid Id)
		{
			var entity = await repo.GetByIdAsNoTrackingAsync(Id);
			imageRepository.DeleteImage(entity is null ? "" : entity.CompanyLogoUrl);
			return await base.DeleteAsync(Id);
		}

		public override async Task<AppResponse<WorkExperienceDTO>> CreateAsync(SaveWorkExperienceDTO saveDto)
		{
			var experienceByKeys = repo.GetAll(new WorkExperienceFilter(Position: saveDto.Position, CompanyName: saveDto.CompanyLogoUrl)).FirstOrDefault();
			if (experienceByKeys is not null)
				AppError.Create("Ya existe una experiencia laboral similar")
					.BuildResponse<WorkExperienceDTO>(HttpStatusCode.BadRequest)
					.Throw();

			string? imageUrl = null;
			if (saveDto.LogoFile is not null)
			{
				imageUrl = await imageRepository
					.SaveImageAsync(saveDto.LogoFile, "CompanyLogo", string.Concat(saveDto.CompanyName, saveDto.Position));
			}
			imageUrl ??= imageRepository.GetDefaultImageUrl("CompanyLogo");
			saveDto.CompanyLogoUrl = imageUrl;

			return await base.CreateAsync(saveDto);
		}

		public override async Task<AppResponse<WorkExperienceDTO>> UpdateAsync(SaveWorkExperienceDTO saveDto, Guid Id)
		{
			var experienceByKeys = repo.GetAll(new WorkExperienceFilter(Position: saveDto.Position, CompanyName: saveDto.CompanyLogoUrl)).FirstOrDefault();
			if (experienceByKeys is not null && experienceByKeys.Id != Id)
				AppError.Create($"Ya existe una experiencia laboral similar")
					.BuildResponse<WorkExperienceDTO>(HttpStatusCode.BadRequest)
					.Throw();

			string? imageUrl = saveDto.CompanyLogoUrl;
			if (saveDto.LogoFile is not null)
			{
				var savedUrl = await imageRepository
					.SaveImageAsync(saveDto.LogoFile, "CompanyLogo", string.Concat(saveDto.CompanyName, saveDto.Position), saveDto.CompanyLogoUrl);
				imageUrl = string.IsNullOrWhiteSpace(savedUrl)
					? saveDto.CompanyLogoUrl
					: savedUrl;
			}
			saveDto.CompanyLogoUrl = imageUrl;
			return await base.UpdateAsync(saveDto, Id);
		}

		public async Task<AppResponse<Empty>> AddTechnologyItemsAsync(Guid ExperienceId, List<Guid> itemsId)
		{
            var experiencTask = _repo.GetByIdAsync(ExperienceId, x => x.TechnologyItems);
			var TechnologyItems = technologyItemRepo.GetAll(new TechnologyItemFilter() { Ids = itemsId }).ToList();
			var experience = await experiencTask;

			if(experience is null)
				AppError.Create("No se encontró ninguna experiencia laboral con el Id enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			if (TechnologyItems.Any())
				AppError.Create("No se encontró ningún Ítem tecnológico con los Ids enviado")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			experience!.TechnologyItems.ToList().AddRange(TechnologyItems);
			var result = await _repo.UpdateAsync(experience);
			if (result)
				AppError.Create("Hubo un problema al registrar los Ítem")
					.BuildResponse<Empty>(HttpStatusCode.BadRequest)
					.Throw();

			return new(HttpStatusCode.OK);
		}

		public AppResponse<List<WorkExperienceDTO>> GetAll(WorkExperienceFilter filter)
		{
			var data = repo.GetAll(filter).ToList();
			if (data is null || !data.Any())
				return new(HttpStatusCode.NoContent, "No hay elementos para mostrar");

			var dataDto = Mapper.Map<WorkExperienceDTO, WorkExperience>(data);
			if (dataDto is null)
				AppError.Create("Hubo problemas al mappear la request")
					.BuildResponse<WorkExperienceDTO>(HttpStatusCode.InternalServerError)
					.Throw();

			return new(dataDto, HttpStatusCode.OK);
		}
	}
}
