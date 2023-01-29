using FluentValidation;
using Shelfie.Api.Users.Services.PasswordService;

namespace Shelfie.Api.Users.Handlers.User.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Адрес электронной почты некорректен")
            ;
        RuleFor(x => x.Password)
            .Must(x => x.Length >= PasswordService.MinPasswordLength)
            .WithMessage("Пароль слишком короткий")
            .Must(x => x.Length <= PasswordService.MaxPasswordLength)
            .WithMessage("Пароль слишком длинный")
            ;
    }
}
