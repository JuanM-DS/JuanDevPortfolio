namespace Core.Domain.Settings
{
	public class JwtSettings
	{
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
		public string ScretKey { get; set; } = string.Empty;
		public int DurationInMinutes { get; set; } 
	}
}
