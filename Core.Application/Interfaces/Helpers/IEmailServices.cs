using Core.Application.DTOs.Email;

namespace Core.Application.Interfaces.Helpers
{
	public interface IEmailServices
	{
		public Task<bool> SendTemplateAsync<TModel>(EmailRequestDTO request, string ViewName, TModel model);
		public Task<bool> SendTemplateAsync(EmailRequestDTO request, string ViewName);
		public Task<bool> SendEmailAsync(EmailRequestDTO request, string htmlBody);
	}
}
