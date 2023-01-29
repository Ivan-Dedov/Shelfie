using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Libs.Postgres.ConnectionFactories;
using Shelfie.Libs.Postgres.Options;

namespace Shelfie.Libs.Postgres.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgres<TOptions>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TOptions : PostgresConnectionFactoryOptions
    {
        services.Configure<TOptions>(_ =>
        {
            configuration
                .GetRequiredSection(nameof(TOptions))
                .Bind(_)
                ;
        });

        services
            .AddTransient<IPostgresConnectionFactory<TOptions>, PostgresConnectionFactory<TOptions>>()
            ;

        return services;
    }
}
