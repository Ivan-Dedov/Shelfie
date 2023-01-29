using System.Data.Common;
using Shelfie.Api.Books.DataAccess.Infrastructure.Options;
using Shelfie.Libs.Postgres.ConnectionFactories;

namespace Shelfie.Api.Books.DataAccess.DataContexts;

public sealed class ShelfieApiBooksDataContext : IShelfieApiBooksDataContext
{
    private readonly IPostgresConnectionFactory<BooksPostgresConnectionFactoryOptions> _connectionFactory;

    public ShelfieApiBooksDataContext(
        IPostgresConnectionFactory<BooksPostgresConnectionFactoryOptions> connectionFactory
    )
    {
        _connectionFactory = connectionFactory;
    }

    public Task<DbConnection> GetConnection(
        CancellationToken ct)
    {
        return _connectionFactory.GetConnection(ct);
    }
}
