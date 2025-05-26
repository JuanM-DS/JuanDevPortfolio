using Asp.Versioning;
using Core.Application.DTOs.Experience;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[Authorize]
	[ApiVersion("1.0")]
	[SwaggerTag("Management of professional work experiences including projects, technologies, and employment history")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class WorkExperienceController : BaseController
	{
		private readonly IWorkExperienceServices _workExperienceServices;

		public WorkExperienceController(IWorkExperienceServices workExperienceServices)
		{
			_workExperienceServices = workExperienceServices;
		}

		[HttpGet("filter")]
		[SwaggerOperation(
			Summary = "Filter work experiences",
			Description = "Retrieves paginated work experiences with advanced filtering options"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Filtered work experiences retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No work experiences found matching criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving work experiences")]
		public IActionResult GetAllWithFilter([FromQuery] WorkExperienceFilter filter)
		{
			var response = _workExperienceServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all work experiences",
			Description = "Retrieves complete list of professional experiences without pagination"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Full work experiences list retrieved")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No work experiences available")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error loading work experiences")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _workExperienceServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get work experience details",
			Description = "Retrieves complete information about a specific work experience including associated projects"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Work experience details found")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Work experience not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving work experience details")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _workExperienceServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create new work experience",
			Description = "Adds a new professional experience entry with detailed information"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Work experience created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid input data or file attachments")]
		[SwaggerResponse((int)HttpStatusCode.UnsupportedMediaType, "Invalid content type")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error creating work experience")]
		public async Task<IActionResult> CreateAsync([FromForm] SaveWorkExperienceDTO saveModel)
		{
			var response = await _workExperienceServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update work experience",
			Description = "Modifies an existing professional experience entry including multimedia attachments"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Work experience updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID mismatch")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error updating work experience")]
		public async Task<IActionResult> UpdateAsync([FromForm] SaveWorkExperienceDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _workExperienceServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete work experience",
			Description = "Permanently removes a work experience and all associated resources"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Work experience deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error deleting work experience")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _workExperienceServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{projectId:guid}/technologies")]
		[SwaggerOperation(
			Summary = "Associate technologies with project",
			Description = "Links technology items to a specific project within a work experience"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technologies successfully associated")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid technology IDs or project ID")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Project or technologies not found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error associating technologies")]
		public async Task<IActionResult> AddTechnologyItems(
			[FromRoute] Guid projectId,
			[FromBody] List<Guid> itemsId)
		{
			var response = await _workExperienceServices.AddTechnologyItemsAsync(projectId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}