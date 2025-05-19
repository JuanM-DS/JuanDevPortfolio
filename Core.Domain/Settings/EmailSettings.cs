namespace Core.Domain.Settings
{
	public class EmailSettings
	{
		public string EmailFrom { get; set; } = string.Empty;
		public string SmptHost { get; set; } = string.Empty;
		public int SmtpPort { get; set; }
		public string SmtpUser { get; set; } = string.Empty;
		public string SmtpPassword { get; set; } = string.Empty;
		public string DisplayName { get; set; } = string.Empty;
	}
}
