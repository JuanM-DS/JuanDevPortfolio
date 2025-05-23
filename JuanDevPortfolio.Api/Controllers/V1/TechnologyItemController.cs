using Asp.Versioning;
using Core.Application.DTOs.TTechnologyItem;
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
	[SwaggerTag("Operations related to technology items management")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class TechnologyItemController : BaseController
	{
		private readonly ITechnologyItemServices _technologyItemServices;

		public TechnologyItemController(ITechnologyItemServices technologyItemServices)
		{
			_technologyItemServices = technologyItemServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Filter technology items",
			Description = "Retrieves paginated technology items with custom filters and sorting"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology items retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving technology items")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Item not found")]
		public IActionResult GetAllWithFilter([FromQuery] TechnologyItemFilter filter)
		{
			var response = _technologyItemServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all technology items",
			Description = "Retrieves complete list of technology items without pagination"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Full technology list retrieved")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error loading technology items")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Item not found")]
		public IActionResult GetAll()
		{
			var response = _technologyItemServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get technology item details",
			Description = "Retrieves full details of a specific technology item by ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology details found")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Item not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving technology details")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _technologyItemServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create new technology item",
			Description = "Creates a new technology item with uploaded files and metadata"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Technology item created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid input data or file format")]
		[SwaggerResponse((int)HttpStatusCode.UnsupportedMediaType, "Invalid content type")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error creating technology item")]
		public async Task<IActionResult> CreateAsync([FromForm] SaveTechnologyItemDTO saveModel)
		{
			var response = await _technologyItemServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update technology item",
			Description = "Updates an existing technology item including associated files"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology item updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID mismatch")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Update operation failed")]
		[SwaggerResponse((int)HttpStatusCode.UnsupportedMediaType, "Invalid content type")]
		public async Task<IActionResult> UpdateAsync([FromForm] SaveTechnologyItemDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _technologyItemServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete technology item",
			Description = "Permanently removes a technology item and associated resources"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Technology item deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Deletion process failed")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid dalete data or ID mismatch")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _technologyItemServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}