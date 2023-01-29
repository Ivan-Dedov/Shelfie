using System.Data.Common;
using Microsoft.Extensions.Options;
using Npgsql;
using Shelfie.Libs.Postgres.Options;

namespace Shelfie.Libs.Postgres.ConnectionFactories;

internal sealed class PostgresConnectionFactory<TOptions> : IPostgresConnectionFactory<TOptions>
    where TOptions : class, IConnectionFactoryOptions
{
    private readonly TOptions _connectionFactoryOptions;

    public PostgresConnectionFactory(
        IOptions<TOptions> connectionFactoryOptionsProvider
    )
    {
        _connectionFactoryOptions = connectionFactoryOptionsProvider.Value;
    }

    public async Task<DbConnection> GetConnection(
        CancellationToken ct)
    {
        var connection = new NpgsqlConnection(_connectionFactoryOptions.ConnectionString);
        await connection.OpenAsync(ct);
        return connection;
    }
}
