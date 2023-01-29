using Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository.Models;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository;

public interface IBookRepository
{
    Task<IReadOnlyCollection<UserBook>> GetBooksByUser(
        long userId,
        CancellationToken cancellationToken
    );
}
