using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.ChangePassword;

namespace WorkEnv.Application.Validation.User;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(c => c.userId)
            .NotEmpty()
            .WithMessage("User Id must be not empty.")
            .NotNull()
            .WithMessage("User Id cannot be null.");
        RuleFor(c => c.oldPassword)
            .MinimumLength(8)
            .WithMessage("Old Password must be more than 5 characters.")
            .MaximumLength(30)
            .WithMessage("Old Password must be less than 80 characters.")
            .NotEmpty()
            .WithMessage("Old Password must be not empty.")
            .NotNull()
            .WithMessage("Old Password cannot be null.");
        RuleFor(c => c.newPassword)
            .MinimumLength(8)
            .WithMessage("New Password must be more than 8 characters.")
            .MaximumLength(30)
            .WithMessage("New Password must be less than 80 characters.")
            .NotEmpty()
            .WithMessage("New Password must be not empty.")
            .NotNull()
            .WithMessage("New Password cannot be null.");
    }
}