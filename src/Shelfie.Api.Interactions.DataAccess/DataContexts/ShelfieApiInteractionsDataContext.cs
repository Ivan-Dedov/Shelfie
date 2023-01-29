using System.Data.Common;
using Shelfie.Api.Interactions.DataAccess.Infrastructure.Options;
using Shelfie.Libs.Postgres.ConnectionFactories;

namespace Shelfie.Api.Interactions.DataAccess.DataContexts;

public sealed class ShelfieApiInteractionsDataContext : IShelfieApiInteractionsDataContext
{
    private readonly IPostgresConnectionFactory<InteractionsPostgresConnectionFactoryOptions> _connectionFactory;

    public ShelfieApiInteractionsDataContext(
        IPostgresConnectionFactory<InteractionsPostgresConnectionFactoryOptions> connectionFactory
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
