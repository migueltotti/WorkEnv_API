using System.Text.RegularExpressions;
using FluentValidation;
using WorkEnv.Application.CQRS.User.Command.Register;
using WorkEnv.Application.Extensions;
using WorkEnv.Application.Services;

namespace WorkEnv.Application.Validation.User;

public class RegisterCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(u => u.name)
            .MinimumLength(5)
            .WithMessage("Name must have at least 5 characters")
            .MaximumLength(200)
            .WithMessage("Name must have at maximum 200 characters")
            .NotEmpty()
            .WithMessage("Name must not be empty.")
            .NotNull()
            .WithMessage("Name must not be null.");
        RuleFor(u => u.email)
            .MinimumLength(10)
            .WithMessage("Email must be more than 10 characters.")
            .MaximumLength(200)
            .WithMessage("Email must be less than 200 characters.")
            .EmailAddress()
            .NotEmpty()
            .WithMessage("Email must be not empty.")
            .NotNull()
            .WithMessage("Email cannot be null.");
        RuleFor(u => u.password)
            .MinimumLength(8)
            .WithMessage("Password must be more than 8 characters.")
            .MaximumLength(30)
            .WithMessage("Password must be less than 80 characters.")
            .Must(PasswordValidation.IsValidPassword)
            .WithMessage("Password must have one uppercase letter, one lowercase letter, one number and one special character.")
            .NotEmpty()
            .WithMessage("Password must be not empty.")
            .NotNull()
            .WithMessage("Password cannot be null.");
        RuleFor(u => u.dateBirth)
            .LessThan(DateTime.Now)
            .WithMessage("Date Birth must be greater than today's date.")
            .NotEmpty()
            .WithMessage("Date Birth must be not empty.")
            .NotNull()
            .WithMessage("Date Birth cannot be null.");
        RuleFor(u => u.cpfCnpj)
            .MinimumLength(10)
            .WithMessage("Cpf Or Cnpj must have at least 10 characters.")
            .MaximumLength(20)
            .WithMessage("Cpf Or Cnpj must have maximum of 20 characters.")
            .Must(CpfCnpjValidation.IsCpfOrCnpjValid)
            .WithMessage("Cpf Or Cnpj contains invalid characters.")
            .NotEmpty()
            .WithMessage("Cpf Or Cnpj must be not empty.")
            .NotNull()
            .WithMessage("Cpf Or Cnpj cannot be null.");
        RuleFor(u => u.personalDescription)
            .MaximumLength(1500)
            .WithMessage("Personal Description must have maximum of 1500 characters.")
            .NotEmpty()
            .WithMessage("Personal Description must be not empty.")
            .NotNull()
            .WithMessage("Personal Description cannot be null.");
    }
}