using Asp.Versioning;
using Core.Application.DTOs.Skill;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class SkillController : BaseController
	{
		private readonly ISkillServices _skillServices;

		public SkillController(ISkillServices skillServices)
		{
			_skillServices = skillServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		public IActionResult GetAllWithFilter([FromQuery] SkillFilter filter)
		{
			var response = _skillServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _skillServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _skillServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveSkillDTO saveModel)
		{
			var response = await _skillServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(AddTechnologyItems))]
		public async Task<IActionResult> AddTechnologyItems(Guid ProjectId, List<Guid> itemsId)
		{
			var response = await _skillServices.AddTechnologyItemsAsync(ProjectId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}
		
		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveSkillDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _skillServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _skillServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
