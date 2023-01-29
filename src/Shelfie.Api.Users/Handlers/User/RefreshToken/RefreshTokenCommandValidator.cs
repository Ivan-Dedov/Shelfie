using FluentValidation;

namespace Shelfie.Api.Users.Handlers.User.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    private const int RefreshTokenLength = 64;

    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .Length(RefreshTokenLength)
            .WithMessage("Токен некорректен");
    }
}
