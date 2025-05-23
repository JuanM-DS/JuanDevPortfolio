using Asp.Versioning;
using Core.Application.DTOs.Project;
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
	[SwaggerTag("Operations related to projects and their technologies")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class ProjectController : BaseController
	{
		private readonly IProjectServices _projectServices;

		public ProjectController(IProjectServices projectServices)
		{
			_projectServices = projectServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Get all projects with filters",
			Description = "Retrieve projects using filtering criteria"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Projects retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No projects found matching the filter criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving projects")]
		public IActionResult GetAllWithFilter([FromQuery] ProjectFilter filter)
		{
			var response = _projectServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all projects",
			Description = "Retrieve all projects without filters"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Projects retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No projects found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving projects")]
		public IActionResult GetAll()
		{
			var response = _projectServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get a project by ID",
			Description = "Retrieve a single project by its unique identifier"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Project not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the project")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _projectServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create a new project",
			Description = "Creates a new project with the provided information"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Project created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid project data")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while creating the project")]
		public async Task<IActionResult> CreateAsync(SaveProjectDTO saveModel)
		{
			var response = await _projectServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[Route(nameof(AddTechnologyItems))]
		[SwaggerOperation(
			Summary = "Add technology items to a project",
			Description = "Associates a list of technology item IDs with an existing project"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology items added successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid project ID or technology item IDs")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while adding technology items")]
		public async Task<IActionResult> AddTechnologyItems([FromQuery] Guid ProjectId, [FromBody] List<Guid> itemsId)
		{
			var response = await _projectServices.AddTechnologyItemsAsync(ProjectId, itemsId);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update an existing project",
			Description = "Updates an existing project by its ID"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while updating the project")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveProjectDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _projectServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete a project",
			Description = "Deletes a project by its ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Project deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the project")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _projectServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}