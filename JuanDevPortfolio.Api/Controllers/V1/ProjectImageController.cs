using Asp.Versioning;
using Core.Application.DTOs.ProjectImage;
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
	[SwaggerTag("Operations related to project images")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class ProjectImageController : BaseController
	{
		private readonly IProjectImageServices _projectImageServices;

		public ProjectImageController(IProjectImageServices projectImageServices)
		{
			_projectImageServices = projectImageServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Get all project images with filter",
			Description = "Retrieve project images using filtering criteria"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project images retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No project images found matching the filter criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving project images")]
		public IActionResult GetAllWithFilter([FromQuery] ProjectImageFilter filter)
		{
			var response = _projectImageServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all project images",
			Description = "Retrieve all project images without filters"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project images retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No project images found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving project images")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _projectImageServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get a project image by ID",
			Description = "Retrieve a single project image by its unique identifier"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project image retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Project image not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the project image")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _projectImageServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create a new project image",
			Description = "Creates a new project image with the provided file and information"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Project image created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid image data or file format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while creating the project image")]
		public async Task<IActionResult> CreateAsync([FromForm] SaveProjectImageDTO saveModel)
		{
			var response = await _projectImageServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update a project image",
			Description = "Updates an existing project image by its ID"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project image updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data, file format, or ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while updating the project image")]
		public async Task<IActionResult> UpdateAsync([FromForm] SaveProjectImageDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _projectImageServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete a project image",
			Description = "Deletes a project image by its ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project image deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the project image")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _projectImageServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}