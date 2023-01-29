using System.Data.Common;
using Shelfie.Libs.Postgres.Options;

namespace Shelfie.Libs.Postgres.ConnectionFactories;

public interface IPostgresConnectionFactory
{
    /// <summary>
    /// Creates and opens a connection to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>An opened connection to the database.</returns>
    /// <remarks>Must be disposed.</remarks>
    Task<DbConnection> GetConnection(
        CancellationToken cancellationToken);
}

public interface IPostgresConnectionFactory<TOptions> : IPostgresConnectionFactory
    where TOptions : IConnectionFactoryOptions
{
}
