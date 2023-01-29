using System.Data.Common;
using Shelfie.Api.Shelves.DataAccess.Infrastructure.Options;
using Shelfie.Libs.Postgres.ConnectionFactories;

namespace Shelfie.Api.Shelves.DataAccess.DataContexts;

public sealed class ShelfieApiShelvesDataContext : IShelfieApiShelvesDataContext
{
    private readonly IPostgresConnectionFactory<ShelvesPostgresConnectionFactoryOptions> _connectionFactory;

    public ShelfieApiShelvesDataContext(
        IPostgresConnectionFactory<ShelvesPostgresConnectionFactoryOptions> connectionFactory
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
