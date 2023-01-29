using System.Data.Common;
using Shelfie.Api.Users.DataAccess.Infrastructure.Options;
using Shelfie.Libs.Postgres.ConnectionFactories;

namespace Shelfie.Api.Users.DataAccess.DataContexts;

public sealed class ShelfieApiUsersDataContext : IShelfieApiUsersDataContext
{
    private readonly IPostgresConnectionFactory<UserPostgresConnectionFactoryOptions> _connectionFactory;

    public ShelfieApiUsersDataContext(
        IPostgresConnectionFactory<UserPostgresConnectionFactoryOptions> connectionFactory
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
