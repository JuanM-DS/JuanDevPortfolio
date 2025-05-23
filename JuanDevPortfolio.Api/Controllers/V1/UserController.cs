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

		[HttpPut("/{Id:Guid}")]
		public async Task<IActionResult> UpdateAsync(SaveUserDTO save, [FromRoute]Guid Id)
		{
			var response = await userServices.UpdateAsync(save, Id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("/{Id:Guid}")]
		public async Task<IActionResult> DeleteAsync(Guid Id)
		{
			var response = await userServices.DeleteAsync(Id);
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
