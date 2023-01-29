using MediatR;
using Shelfie.Api.Books.Common;
using Shelfie.Api.Books.Models.GetBook;

namespace Shelfie.Api.Books.Handlers.Search.GetBooksCommandHandler;

public sealed record GetBooksCommand : IRequest<IReadOnlyCollection<GetBookResponse>>
{
    private const int DefaultTake = 10;
    private const int DefaultSkip = 0;

    public long UserId { get; }

    public string? Query { get; }

    public IReadOnlyCollection<long> GenreIds { get; }

    public IReadOnlyCollection<long> CountryIds { get; }

    public Rating MinRating { get; }

    public IReadOnlyCollection<long> AttributeIds { get; }

    public int Take { get; }

    public int Skip { get; }

    public GetBooksCommand(
        long userId,
        string? query,
        IReadOnlyCollection<long>? genreIds,
        IReadOnlyCollection<long>? countryIds,
        int? minRating,
        IReadOnlyCollection<long>? attributeIds,
        int take,
        int skip
    )
    {
        UserId = userId;
        Query = query;
        GenreIds = genreIds ?? ArraySegment<long>.Empty;
        CountryIds = countryIds ?? ArraySegment<long>.Empty;
        MinRating = new Rating(minRating ?? 0);
        AttributeIds = attributeIds ?? ArraySegment<long>.Empty;
        Take = take < 0
            ? DefaultTake
            : take;
        Skip = skip < 0
            ? DefaultSkip
            : skip;
    }
}
