using Asp.Versioning;
using Core.Application.DTOs.Project;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class ProjectController : BaseController
	{
		private readonly IProjectServices _projectServices;

		public ProjectController(IProjectServices projectServices)
		{
			_projectServices = projectServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		public IActionResult GetAllWithFilter([FromQuery] ProjectFilter filter)
		{
			var response = _projectServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _projectServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _projectServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveProjectDTO saveModel)
		{
			var response = await _projectServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(AddTechnologyItems))]
		public async Task<IActionResult> AddTechnologyItems(Guid ProjectId, List<Guid> itemsId)
		{
			var response = await _projectServices.AddTechnologyItemsAsync(ProjectId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}
		
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveProjectDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _projectServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _projectServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
