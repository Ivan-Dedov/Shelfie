namespace Shelfie.Libs.Postgres.Options;

public interface IConnectionFactoryOptions
{
    string ConnectionString { get; init; }
}
