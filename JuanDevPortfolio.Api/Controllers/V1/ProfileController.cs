using Asp.Versioning;
using Core.Application.DTOs.Profile;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class ProfileController : BaseController
	{
		private readonly IProfileServices _profileServices;

		public ProfileController(IProfileServices profileServices)
		{
			_profileServices = profileServices;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] ProfileFilter filter)
		{
			var response = _profileServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _profileServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _profileServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveProfileDTO saveModel)
		{
			var response = await _profileServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAsync(SaveProfileDTO saveModel)
		{
			var response = await _profileServices.UpdateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _profileServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
