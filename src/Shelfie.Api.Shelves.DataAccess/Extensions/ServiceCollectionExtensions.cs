using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Shelves.DataAccess.DataContexts;
using Shelfie.Api.Shelves.DataAccess.Infrastructure.Options;
using Shelfie.Libs.Postgres.Extensions;

namespace Shelfie.Api.Shelves.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShelvesDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostgres<ShelvesPostgresConnectionFactoryOptions>(configuration);

        services.AddTransient<IShelfieApiShelvesDataContext, ShelfieApiShelvesDataContext>();

        // Repositories

        return services;
    }
}
