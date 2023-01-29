using FluentValidation;

namespace Shelfie.Api.Books.Handlers.Search.GetBooksCommandHandler;

public sealed class GetBooksCommandValidator : AbstractValidator<GetBooksCommand>
{
    public GetBooksCommandValidator()
    {
        RuleFor(_ => _.UserId)
            .GreaterThan(0)
            .WithMessage("Некорректный ID пользователя!");
    }
}
