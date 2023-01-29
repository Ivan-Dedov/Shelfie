using Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository.Models;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository;

public interface IReviewRepository
{
    Task<IReadOnlyCollection<Review>> GetReviewsByUser(
        long userId,
        CancellationToken cancellationToken
    );

    Task<IReadOnlyCollection<Review>> GetBookReviews(
        long bookId,
        CancellationToken cancellationToken
    );
}
