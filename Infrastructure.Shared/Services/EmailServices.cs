using Core.Application.DTOs.Email;
using Core.Application.Interfaces.Helpers;
using Core.Domain.Enumerables;
using Core.Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;

namespace Infrastructure.Shared.Services
{
	public class EmailServices : IEmailServices
	{
		private readonly EmailSettings emailSettings;
		private readonly IOptions<EmailSettings> emailSettins;
		private readonly ITemplateServices templateServices;

		public EmailServices(IOptions<EmailSettings> EmailSettins, ITemplateServices TemplateServices)
		{
			emailSettings = EmailSettins.Value;
			emailSettins = EmailSettins;
			templateServices = TemplateServices;
		}

		public async Task<bool> SendTemplateAsync<TModel>(EmailRequestDTO request, string ViewName, TModel model)
		{
			var htmlBody = await templateServices.GetRazorTemplateAsStringAsync(ViewName, model);
			if (string.IsNullOrEmpty(htmlBody))
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error($"No se encontro ninguna pagina de razor con el nombre: {ViewName}");
				return false;
			}

			return await SendAsync(request, htmlBody);
		}

		public async Task<bool> SendTemplateAsync(EmailRequestDTO request, string ViewName)
		{
			var htmlBody = await templateServices.GetRazorTemplateAsStringAsync(ViewName);
			if (string.IsNullOrEmpty(htmlBody))
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error($"No se encontro ninguna pagina de razor con el nombre: {ViewName}");
				return false;
			}

			return await SendAsync(request, htmlBody);
		}

		public async Task<bool> SendEmailAsync(EmailRequestDTO request, string htmlBody)
		{
			return await SendAsync(request, htmlBody);
		}

		private async Task<bool> SendAsync(EmailRequestDTO request, string htmlBody)
		{
			var email = new MimeMessage();
			email.Sender = MailboxAddress.Parse(emailSettings.EmailFrom);
			email.To.Add(MailboxAddress.Parse(request.To));
			email.Subject = request.Subject;
			var body = new BodyBuilder() { HtmlBody = htmlBody };
			email.Body = body.ToMessageBody();

			try
			{
				using var client = new SmtpClient();
				client.ServerCertificateValidationCallback = (a, s, d, f) => true;
				await client.ConnectAsync(emailSettings.SmptHost, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(emailSettings.SmtpUser, emailSettings.SmtpPassword);
				await client.SendAsync(email);
				await client.DisconnectAsync(true);
			}
			catch (Exception ex)
			{
				Log.ForContext(LoggerKeys.SharedLogs.ToString(), true).Error(ex.Message);
				return false;
			}

			return true;
		}
	}
}
