using Asp.Versioning;
using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using JuanDevPortfolio.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace YourNamespace.Controllers
{
	[ApiVersion("1.0")]
	[Authorize]
	[SwaggerTag("Operations related to User profiles")]
	[SwaggerResponse((int)HttpStatusCode.Unauthorized, "Authentication required")]
	[SwaggerResponse((int)HttpStatusCode.Forbidden, "Insufficient permissions")]
	public class ProfileController : BaseController
	{
		private readonly IProfileServices _profileServices;

		public ProfileController(IProfileServices profileServices)
		{
			_profileServices = profileServices;
		}

		[HttpGet]
		[Route(nameof(GetAllWithFilter))]
		[SwaggerOperation(
			Summary = "Get all profiles with filters",
			Description = "Retrieve profiles using filtering criteria"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profiles retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No profiles found matching the filter criteria")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid filter parameters")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving profiles")]
		public async Task<IActionResult> GetAllWithFilter([FromQuery] ProfileFilter filter)
		{
			var response = await _profileServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[SwaggerOperation(
			Summary = "Get all profiles",
			Description = "Retrieve all profiles without filters"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profiles retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "No profiles found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving profiles")]
		public async Task<IActionResult> GetAll()
		{
			var response = await _profileServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		[SwaggerOperation(
			Summary = "Get profile by ID",
			Description = "Retrieve a single profile by its unique identifier"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profile retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Profile not found")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID format")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the profile")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _profileServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[Route(nameof(GetCurrentProfileAsync))]
		[SwaggerOperation(
			Summary = "Get current profile",
			Description = "Retrieve the profile of current User"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profile retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Profile not found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the profile")]
		public async Task<IActionResult> GetCurrentProfileAsync()
		{
			var response = await _profileServices.GetCurrentProfileAsync();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		[Route(nameof(GetByAccountIdAsync))]
		[SwaggerOperation(
			Summary = "Get profile",
			Description = "Retrieve a profile by a account id"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profile retrieved successfully")]
		[SwaggerResponse((int)HttpStatusCode.NoContent, "Profile not found")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while retrieving the profile")]
		public async Task<IActionResult> GetByAccountIdAsync(Guid id)
		{
			var response = await _profileServices.GetByAccountIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Create a new profile",
			Description = "Creates a new User profile with the provided information"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.Created, "Profile created successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid profile data")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while creating the profile")]
		public async Task<IActionResult> CreateAsync(SaveProfileDTO saveModel)
		{
			var response = await _profileServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut("{id:guid}")]
		[SwaggerOperation(
			Summary = "Update profile",
			Description = "Updates an existing profile by its ID"
		)]
		[Consumes("application/json")]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profile updated successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid update data or ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while updating the profile")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveProfileDTO saveModel, [FromRoute] Guid id)
		{
			var response = await _profileServices.UpdateAsync(saveModel, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		[SwaggerOperation(
			Summary = "Delete profile",
			Description = "Deletes a profile by its ID"
		)]
		[SwaggerResponse((int)HttpStatusCode.OK, "Profile deleted successfully")]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID")]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, "An error occurred while deleting the profile")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _profileServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}