using Asp.Versioning;
using Core.Application.DTOs.TTechnologyItem;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class TechnologyItemController : BaseController
	{
		private readonly ITechnologyItemServices _technologyItemServices;

		public TechnologyItemController(ITechnologyItemServices technologyItemServices)
		{
			_technologyItemServices = technologyItemServices;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] TechnologyItemFilter filter)
		{
			var response = _technologyItemServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _technologyItemServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _technologyItemServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveTechnologyItemDTO saveModel)
		{
			var response = await _technologyItemServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAsync(SaveTechnologyItemDTO saveModel)
		{
			var response = await _technologyItemServices.UpdateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _technologyItemServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
