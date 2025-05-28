using Core.Application.DTOs.Authentication;
using FluentValidation;

namespace Core.Application.Validations
{
	public class SaveUserValidator : AbstractValidator<SaveUserDTO>
	{
		public SaveUserValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("El correo electrónico es obligatorio.")
				.EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("La contraseña es obligatoria.")
				.MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password).WithMessage("Las contraseñas no coinciden.");

			RuleFor(x => x.FirstName)
				.NotEmpty().WithMessage("El nombre es obligatorio.")
				.MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

			RuleFor(x => x.Roles)
				.NotNull().WithMessage("Debe asignar al menos un rol.")
				.Must(roles => roles.Count > 0).WithMessage("Debe asignar al menos un rol.");

			When(x => x.ImageFile != null, () =>
			{
				RuleFor(x => x.ImageFile!.ContentType)
					.Must(contentType => contentType.StartsWith("image/"))
					.WithMessage("El archivo debe ser una imagen.");
			});
		}
	}
}
