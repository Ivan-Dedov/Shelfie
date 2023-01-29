using Dapper;
using Shelfie.Api.Interactions.DataAccess.DataContexts;
using Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository.Models;
using Shelfie.Api.Interactions.DataAccess.Schemas.Tables;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository;

public sealed class ReviewRepository : IReviewRepository
{
    private readonly IShelfieApiInteractionsDataContext _dataContext;

    public ReviewRepository(
        IShelfieApiInteractionsDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<IReadOnlyCollection<Review>> GetReviewsByUser(
        long userId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", userId);

        var command = new CommandDefinition($@"-- Get reviews by user
            SELECT
              br.{BookReviewSchema.Id} AS {nameof(ReviewDao.Id)}
            , br.{BookReviewSchema.BookId} AS {nameof(ReviewDao.BookId)}
            , br.{BookReviewSchema.ReviewerId} AS {nameof(ReviewDao.ReviewerId)}
            , br.{BookReviewSchema.Text} AS {nameof(ReviewDao.Text)}
            , br.{BookReviewSchema.Rating} AS {nameof(ReviewDao.Rating)}
            , br.{BookReviewSchema.CreatedAt} AS {nameof(ReviewDao.CreatedAt)}
            FROM
              {BookReviewSchema.FullTableName} br
            WHERE
              br.{BookReviewSchema.ReviewerId} = @Id
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var reviews = await connection.QueryAsync<ReviewDao>(command);
        return reviews
                .Select(r => new Review(r))
                .ToList()
                ;
    }

    public async Task<IReadOnlyCollection<Review>> GetBookReviews(
        long bookId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("BookId", bookId);

        var command = new CommandDefinition($@"-- Get book reviews
            SELECT
              br.{BookReviewSchema.Id} AS {nameof(ReviewDao.Id)}
            , br.{BookReviewSchema.BookId} AS {nameof(ReviewDao.BookId)}
            , br.{BookReviewSchema.ReviewerId} AS {nameof(ReviewDao.ReviewerId)}
            , br.{BookReviewSchema.Text} AS {nameof(ReviewDao.Text)}
            , br.{BookReviewSchema.Rating} AS {nameof(ReviewDao.Rating)}
            , br.{BookReviewSchema.CreatedAt} AS {nameof(ReviewDao.CreatedAt)}
            FROM
              {BookReviewSchema.FullTableName} br
            WHERE
              br.{BookReviewSchema.BookId} = @BookId
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var reviews = await connection.QueryAsync<ReviewDao>(command);
        return reviews
                .Select(r => new Review(r))
                .ToList()
                ;
    }
}
