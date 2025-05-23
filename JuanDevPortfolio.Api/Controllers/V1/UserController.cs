using Asp.Versioning;
using Core.Application.DTOs.Authentication;
using Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class UserController : BaseController
	{
		private readonly IUserServices userServices;

		public UserController(IAccountServices accountServices, IUserServices UserServices)
		{
			userServices = UserServices;
		}

		[HttpPut("{id:Guid}")]
		public async Task<IActionResult> UpdateAsync([FromBody] SaveUserDTO save, [FromRoute]Guid id)
		{
			var response = await userServices.UpdateAsync(save, id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:Guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await userServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var response = await userServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
