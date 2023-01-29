using Dapper;
using Shelfie.Api.Interactions.DataAccess.DataContexts;
using Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository.Models;
using Shelfie.Api.Interactions.DataAccess.Schemas.Tables;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository;

public sealed class QuoteRepository : IQuoteRepository
{
    private readonly IShelfieApiInteractionsDataContext _dataContext;

    public QuoteRepository(
        IShelfieApiInteractionsDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<IReadOnlyCollection<Quote>> GetQuotesByUser(
        long userId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", userId);

        var command = new CommandDefinition($@"-- Get quotes by user
            SELECT
              bq.{BookQuoteSchema.Id} AS {nameof(QuoteDao.Id)}
            , bq.{BookQuoteSchema.BookId} AS {nameof(QuoteDao.BookId)}
            , bq.{BookQuoteSchema.UserId} AS {nameof(QuoteDao.UserId)}
            , bq.{BookQuoteSchema.Text} AS {nameof(QuoteDao.Text)}
            , bq.{BookQuoteSchema.CreatedAt} AS {nameof(QuoteDao.CreatedAt)}
            FROM
              {BookQuoteSchema.FullTableName} bq
            WHERE
              bq.{BookQuoteSchema.UserId} = @Id
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var quotes = await connection.QueryAsync<QuoteDao>(command);
        return quotes
                .Select(q => new Quote(q))
                .ToList()
                ;
    }

    public async Task<IReadOnlyCollection<Quote>> GetBookQuotes(
        long bookId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("BookId", bookId);

        var command = new CommandDefinition($@"-- Get book quotes
            SELECT
              bq.{BookQuoteSchema.Id} AS {nameof(QuoteDao.Id)}
            , bq.{BookQuoteSchema.BookId} AS {nameof(QuoteDao.BookId)}
            , bq.{BookQuoteSchema.UserId} AS {nameof(QuoteDao.UserId)}
            , bq.{BookQuoteSchema.Text} AS {nameof(QuoteDao.Text)}
            , bq.{BookQuoteSchema.CreatedAt} AS {nameof(QuoteDao.CreatedAt)}
            FROM
              {BookQuoteSchema.FullTableName} bq
            WHERE
              bq.{BookQuoteSchema.BookId} = @BookId
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var quotes = await connection.QueryAsync<QuoteDao>(command);
        return quotes
                .Select(q => new Quote(q))
                .ToList()
                ;
    }
}
