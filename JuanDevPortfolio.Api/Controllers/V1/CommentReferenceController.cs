using Asp.Versioning;
using Core.Application.DTOs.CommentReferences;
using Core.Application.Interfaces.Services;
using Core.Application.QueryFilters;
using Microsoft.AspNetCore.Mvc;

namespace JuanDevPortfolio.Api.Controllers.V1
{
	[ApiVersion("1.0")]
	public class CommentReferenceController : BaseController
	{
		private readonly ICommentReferencesServices _commentReferencesServices;

		public CommentReferenceController(ICommentReferencesServices commentReferencesServices)
		{
			_commentReferencesServices = commentReferencesServices;
		}

		[HttpGet]
		public IActionResult GetAll([FromQuery] CommentReferenceFilter filter)
		{
			var response = _commentReferencesServices.GetAll(filter);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			var response = _commentReferencesServices.GetAll();
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetByIdAsync(Guid id)
		{
			var response = await _commentReferencesServices.GetByIdAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync(SaveCommentReferenceDTO saveModel)
		{
			var response = await _commentReferencesServices.CreateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateAsync(SaveCommentReferenceDTO saveModel)
		{
			var response = await _commentReferencesServices.UpdateAsync(saveModel);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> DeleteAsync(Guid id)
		{
			var response = await _commentReferencesServices.DeleteAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}

		[HttpPatch("{id:guid}")]
		public async Task<IActionResult> ConfirmCommentReferenceAsync(Guid id)
		{
			var response = await _commentReferencesServices.ConfirmCommentReferenceAsync(id);
			return StatusCode((int)response.HttpStatusCode, response);
		}
	}
}
