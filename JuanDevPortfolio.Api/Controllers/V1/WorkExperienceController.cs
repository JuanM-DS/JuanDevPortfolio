using Asp.Versioning;
using Core.Application.DTOs.Experience;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class WorkExperienceController : BaseController
	{
		private readonly IWorkExperienceServices _workExperienceServices;

		public WorkExperienceController(IWorkExperienceServices workExperienceServices)
		{
			_workExperienceServices = workExperienceServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		public IActionResult GetAllWithFilter([FromQuery] WorkExperienceFilter filter)
		{
			var response = _workExperienceServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _workExperienceServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _workExperienceServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveWorkExperienceDTO saveModel)
		{
			var response = await _workExperienceServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(AddTechnologyItems))]
		public async Task<IActionResult> AddTechnologyItems(Guid ProjectId, List<Guid> itemsId)
		{
			var response = await _workExperienceServices.AddTechnologyItemsAsync(ProjectId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}
		
		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveWorkExperienceDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _workExperienceServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _workExperienceServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
