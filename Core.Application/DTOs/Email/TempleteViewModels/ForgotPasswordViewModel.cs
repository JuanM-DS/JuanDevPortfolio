namespace Core.Application.DTOs.Email.TempleteViewModels
{
	public record ForgotPasswordViewModel(
		string UserName,
		string ResetLink,
		string ExpirationTime,
		int Year
	);
}
