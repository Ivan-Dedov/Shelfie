using Dapper;
using Shelfie.Api.Interactions.DataAccess.DataContexts;
using Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository.Models;
using Shelfie.Api.Interactions.DataAccess.Schemas.Tables;

namespace Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository;

public sealed class BookRepository : IBookRepository
{
    private readonly IShelfieApiInteractionsDataContext _dataContext;

    public BookRepository(
        IShelfieApiInteractionsDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<IReadOnlyCollection<UserBook>> GetBooksByUser(
        long userId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);

        var command = new CommandDefinition($@"-- Get books by user (with current status)
            SELECT
              bs.{BookStatusSchema.BookId} AS {nameof(UserBookDao.BookId)}
            , MAX(bs.{BookStatusSchema.BookStatus}) OVER (ORDER BY bs.{BookStatusSchema.CreatedAt}) AS {nameof(UserBookDao.BookStatus)}
            FROM
              {BookStatusSchema.FullTableName} bs
            WHERE
              bs.{BookStatusSchema.UserId} = @UserId
            GROUP BY
              bs.{BookStatusSchema.BookId}
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var books = await connection.QueryAsync<UserBookDao>(command);
        return books
                .Select(b => new UserBook(b))
                .ToList()
                ;
    }
}
