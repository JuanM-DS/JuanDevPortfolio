using Asp.Versioning;
using Core.Application.DTOs.ProjectImage;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class ProjectImageController : BaseController
	{
		private readonly IProjectImageServices _projectImageServices;

		public ProjectImageController(IProjectImageServices projectImageServices)
		{
			_projectImageServices = projectImageServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		public IActionResult GetAllWithFilter([FromQuery] ProjectImageFilter filter)
		{
			var response = _projectImageServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _projectImageServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _projectImageServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveProjectImageDTO saveModel)
		{
			var response = await _projectImageServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveProjectImageDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _projectImageServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _projectImageServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
