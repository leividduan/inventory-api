using FluentValidation;
using Inventory.Domain.Utils;

namespace Inventory.Domain.Entities.Validators;

public class UserValidator : AbstractValidator<User>
{
	public UserValidator()
	{
		RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
			.MinimumLength(3).WithMessage("Minimum length must be {MinLength} characters")
			.MaximumLength(100).WithMessage("Maximum length must be {MaxLength} characters");

		RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
			.EmailAddress().WithMessage("{PropertyName} must be a valid e-mail");

		// RuleFor(x => x.Role).NotNull().NotEmpty().WithMessage("{PropertyName} is required")
		// 	.IsInEnum().WithMessage("{PropertyName} cannot accept the value {PropertyValue}");

		RuleFor(x => x.Password).NotNull().NotNull().WithMessage("{PropertyName} is required")
			.Custom((password, context) =>
			{
				if (!PasswordUtils.IsValidPasswordStrength(password))
					context.AddFailure(
						"Password should contain at least one lower case letter, upper case letter, one numeric value, one special case characters and should be greater or equal 8 characters and less or equal 30 characters");
			});
	}
}
