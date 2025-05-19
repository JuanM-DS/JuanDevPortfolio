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
		private readonly IEncryptationServices encryptationServices;

		public EmailServices(IOptions<EmailSettings> EmailSettins, ITemplateServices TemplateServices, IEncryptationServices EncryptationServices)
		{
			emailSettings = EmailSettins.Value;
			emailSettins = EmailSettins;
			templateServices = TemplateServices;
			encryptationServices = EncryptationServices;
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
			var host = encryptationServices.Decrypt(emailSettings.SmptHost);
			var emailFrom = encryptationServices.Decrypt(emailSettings.EmailFrom);
			var password = encryptationServices.Decrypt(emailSettings.SmtpPassword);

			var email = new MimeMessage();
			email.Sender = MailboxAddress.Parse(emailFrom);
			email.To.Add(MailboxAddress.Parse(request.To));
			email.Subject = request.Subject;
			var body = new BodyBuilder() { HtmlBody = htmlBody };
			email.Body = body.ToMessageBody();

			try
			{
				using var client = new SmtpClient();
				client.ServerCertificateValidationCallback = (a, s, d, f) => true;
				await client.ConnectAsync(host, emailSettings.SmtpPort, SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(emailFrom, password);
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
