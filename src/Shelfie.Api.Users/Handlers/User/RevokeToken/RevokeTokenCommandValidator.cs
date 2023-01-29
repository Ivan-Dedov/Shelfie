using FluentValidation;
using Shelfie.Api.Users.Services.JwtService;

namespace Shelfie.Api.Users.Handlers.User.RevokeToken;

public sealed class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .Length(JwtService.RefreshTokenLength)
            .WithMessage("Токен некорректен");
    }
}
