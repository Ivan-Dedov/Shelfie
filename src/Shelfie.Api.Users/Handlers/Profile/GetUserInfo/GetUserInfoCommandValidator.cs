using FluentValidation;

namespace Shelfie.Api.Users.Handlers.Profile.GetUserInfo;

public sealed class GetUserInfoCommandValidator : AbstractValidator<GetUserInfoCommand>
{
    public GetUserInfoCommandValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("Неизвестный пользователь");
    }
}
