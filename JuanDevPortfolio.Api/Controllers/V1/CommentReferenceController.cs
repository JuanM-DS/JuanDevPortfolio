using Asp.Versioning;
using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using JuanDevPortfolio.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace YourNamespace.Controllers
{
	[Authorize]
	[ApiVersion("1.0")]
	[SwaggerTag("Operations related to comment references")]
	public class CommentReferenceController : BaseController
	{
		private readonly ICommentReferencesServices _commentReferencesServices;

		public CommentReferenceController(ICommentReferencesServices commentReferencesServices)
		{
			_commentReferencesServices = commentReferencesServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Get all comment references with filters",
			Description = "Retrieve comment references based on optional filtering criteria"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment references retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No comment references found matching the filter criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving comment references")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public IActionResult GetAllWithFilter([FromQuery] CommentReferenceFilter filter)
		{
			var response = _commentReferencesServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all comment references",
			Description = "Retrieve all comment references without any filter"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment references retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No comment references found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving comment references")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _commentReferencesServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get comment reference by ID",
			Description = "Retrieve a single comment reference by its unique identifier"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment reference retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NotFound, "Comment reference not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the comment reference")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _commentReferencesServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create a new comment reference",
			Description = "Creates a new comment reference with the given information"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Comment reference created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid data for comment reference creation")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while creating the comment reference")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> CreateAsync(SaveCommentReferenceDTO saveModel)
		{
			var response = await _commentReferencesServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update an existing comment reference",
			Description = "Updates an existing comment reference by its ID"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment reference updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while updating the comment reference")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveCommentReferenceDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _commentReferencesServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPatch("{id:guid}")]
		[SwaggerOperation(
			Summary = "Confirm a comment reference",
			Description = "Marks the comment reference as confirmed"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment reference confirmed successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while confirming the comment reference")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> ConfirmCommentReferenceAsync(Guid id)
		{
			var response = await _commentReferencesServices.ConfirmCommentReferenceAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete a comment reference",
			Description = "Deletes a comment reference by its ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Comment reference deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the comment reference")]
		[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
		[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _commentReferencesServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}