namespace Shelfie.Libs.Postgres.Options;

public abstract record PostgresConnectionFactoryOptions : IConnectionFactoryOptions
{
    public string ConnectionString { get; init; }
}
