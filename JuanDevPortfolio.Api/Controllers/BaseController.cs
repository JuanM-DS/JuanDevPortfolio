using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers
{
	[Route("api/v/{version:apiVersion}/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
	}
}
