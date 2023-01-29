using Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository.Models;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository;

public interface IQuoteRepository
{
    Task<IReadOnlyCollection<Quote>> GetQuotesByUser(
        long userId,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyCollection<Quote>> GetBookQuotes(
        long bookId,
        CancellationToken cancellationToken
    );
}
