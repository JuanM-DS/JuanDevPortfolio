using Asp.Versioning;
using Core.Application.DTOs.ExperienceDetail;
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
	[SwaggerTag("Management of detailed work experience components and specific achievements")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class WorkExperienceDetailController : BaseController
	{
		private readonly IWorkExperienceDetailServices _workExperienceDetailServices;

		public WorkExperienceDetailController(IWorkExperienceDetailServices workExperienceDetailServices)
		{
			_workExperienceDetailServices = workExperienceDetailServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Filter experience details",
			Description = "Retrieves paginated work experience details with custom filters"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Filtered details retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No details found matching criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving details")]
		public IActionResult GetAllWithFilter([FromQuery] WorkExperienceDetailFilter filter)
		{
			var response = _workExperienceDetailServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all experience details",
			Description = "Retrieves complete list of work experience details"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Full details list retrieved")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No details available")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error loading details")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _workExperienceDetailServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get experience detail by ID",
			Description = "Retrieves specific work experience detail with full technical specifications"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Detail found successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Detail not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving detail")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _workExperienceDetailServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create new experience detail",
			Description = "Adds new technical detail to a work experience entry"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Detail created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid input data")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error creating detail")]
		public async Task<IActionResult> CreateAsync([FromBody] SaveWorkExperienceDetailDTO saveModel)
		{
			var response = await _workExperienceDetailServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update experience detail",
			Description = "Modifies technical specifications of an existing work experience detail"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Detail updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error updating detail")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveWorkExperienceDetailDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _workExperienceDetailServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete experience detail",
			Description = "Removes specific technical detail from work experience"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Detail deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error deleting detail")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _workExperienceDetailServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}