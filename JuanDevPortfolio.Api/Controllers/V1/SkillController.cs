using Asp.Versioning;
using Core.Application.DTOs.Skill;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	[Authorize]
	[SwaggerTag("Operations related to skills")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class SkillController : BaseController
	{
		private readonly ISkillServices _skillServices;

		public SkillController(ISkillServices skillServices)
		{
			_skillServices = skillServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Get all skills with filter",
			Description = "Retrieve skills using filtering criteria"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Skills retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No skills found matching the filter criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving skills")]
		public IActionResult GetAllWithFilter([FromQuery] SkillFilter filter)
		{
			var response = _skillServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all skills",
			Description = "Retrieve all skills without filters"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Skills retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No skills found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving skills")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _skillServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get a skill by ID",
			Description = "Retrieve a single skill by its unique identifier"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Skill retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Skill not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the skill")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _skillServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create a new skill",
			Description = "Creates a new skill with the provided information"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Skill created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid skill data")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while creating the skill")]
		public async Task<IActionResult> CreateAsync(SaveSkillDTO saveModel)
		{
			var response = await _skillServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(AddTechnologyItems))]
		[SwaggerOperation(
			Summary = "Add technologies to a skill",
			Description = "Associates a list of technology item IDs with an existing skill"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology items added successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid skill ID or technology item IDs")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while adding technology items")]
		public async Task<IActionResult> AddTechnologyItems([FromQuery] Guid SkillId, [FromBody] List<Guid> itemsId)
		{
			var response = await _skillServices.AddTechnologyItemsAsync(SkillId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update a skill",
			Description = "Updates an existing skill by its ID"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Skill updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while updating the skill")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveSkillDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _skillServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete a skill",
			Description = "Deletes a skill by its ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Skill deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the skill")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _skillServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}