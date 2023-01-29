using Dapper;
using Shelfie.Api.Books.DataAccess.DataContexts;
using Shelfie.Api.Books.DataAccess.Repositories.BookRepository.Models;
using Shelfie.Api.Books.DataAccess.Schemas.Tables;
namespace Shelfie.Api.Books.DataAccess.Repositories.BookRepository;

public sealed class BookRepository : IBookRepository
{
    private readonly IShelfieApiBooksDataContext _dataContext;

    public BookRepository(
        IShelfieApiBooksDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<IReadOnlyCollection<Book>> GetBooks(
        CancellationToken ct)
    {
        // var parameters = new DynamicParameters();
        // parameters.Add("UserId", userId);
        //
        // var command = new CommandDefinition($@"-- Get books by user (with current status)
        //     SELECT
        //       bs.{BookSchema.BookId} AS {nameof(UserBookDao.BookId)}
        //     , MAX(bs.{BookSchema.BookStatus}) OVER (ORDER BY bs.{BookSchema.CreatedAt}) AS {nameof(UserBookDao.BookStatus)}
        //     FROM
        //       {BookSchema.FullTableName} bs
        //     WHERE
        //       bs.{BookSchema.UserId} = @UserId
        //     GROUP BY
        //       bs.{BookSchema.BookId}
        //     ;",
        //     parameters,
        //     cancellationToken: ct);
        //
        // await using var connection = await _dataContext.GetConnection(ct);
        //
        // var books = await connection.QueryAsync<BookDao>(command);
        // return books
        //         .Select(b => new Book(b))
        //         .ToList()
        //         ;
        return null;
    }
}
