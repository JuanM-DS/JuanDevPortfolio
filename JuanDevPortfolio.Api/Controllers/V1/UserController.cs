using Asp.Versioning;
using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	[Authorize]
	[SwaggerTag("User management operations including registration, updates, and deletions")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class UserController : BaseController
	{
		private readonly IUserServices _userServices;

		public UserController(IUserServices userServices)
		{
			_userServices = userServices;
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all users",
			Description = "Retrieves complete list of registered users (Admin only)"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "User list retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No User found matching criteria")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Error retrieving User list")]
		public async Task<IActionResult> GetAllAsync()
		{
			var response = await _userServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update User profile",
			Description = "Updates User information including personal details and credentials"
		)]
		[Consumes("multipart/form-data")]
		[SwaggerResponse((int)HttpStatusCode.OK, "User profile updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid User data or ID mismatch")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Update operation failed")]
		[SwaggerResponse((int)HttpStatusCode.UnsupportedMediaType, "Invalid content type")]
		public async Task<IActionResult> UpdateAsync([FromForm] SaveUserDTO save, [FromRoute] Guid id)
		{
			var response = await _userServices.UpdateAsync(save, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete User account",
			Description = "Permanently removes User account and associated data (Admin only)"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "User deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid User ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Deletion process failed")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _userServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}