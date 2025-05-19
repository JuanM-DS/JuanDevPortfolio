namespace Core.Application.DTOs.Email.TempleteViewModels
{
	public class ForgotPasswordViewModel
	{
		public string UserName { get; set; } = string.Empty;
		public string ResetLink { get; set; } = string.Empty;
		public string ExpirationTime { get; set; } = string.Empty;
		public int Year { get; set; } 
		public string CompanyName { get; set; } = string.Empty;
	}
}
