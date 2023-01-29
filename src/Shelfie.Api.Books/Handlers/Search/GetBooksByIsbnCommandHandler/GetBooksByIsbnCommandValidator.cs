using FluentValidation;

namespace Shelfie.Api.Books.Handlers.Search.GetBooksByIsbnCommandHandler;

public sealed class GetBooksByIsbnCommandValidator : AbstractValidator<GetBooksByIsbnCommand>
{
    private const int IsbnLength = 13;

    public GetBooksByIsbnCommandValidator()
    {
        RuleFor(_ => _.Isbn)
            .NotEmpty()
            .Length(IsbnLength)
            .WithMessage($"Некорректный ISBN! Валидный ISBN должен состоять из {IsbnLength} символов.");
        RuleFor(_ => _.UserId)
            .GreaterThan(0)
            .WithMessage("Некорректный ID пользователя!");
    }
}
