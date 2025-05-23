using Asp.Versioning;
using Core.Application.DTOs.ExperienceDetail;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class WorkExperienceDetailController : BaseController
	{
		private readonly IWorkExperienceDetailServices _workExperienceDetailServices;

		public WorkExperienceDetailController(IWorkExperienceDetailServices workExperienceDetailServices)
		{
			_workExperienceDetailServices = workExperienceDetailServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		public IActionResult GetAllWithFilter([FromQuery] WorkExperienceDetailFilter filter)
		{
			var response = _workExperienceDetailServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _workExperienceDetailServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _workExperienceDetailServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveWorkExperienceDetailDTO saveModel)
		{
			var response = await _workExperienceDetailServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveWorkExperienceDetailDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _workExperienceDetailServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _workExperienceDetailServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
