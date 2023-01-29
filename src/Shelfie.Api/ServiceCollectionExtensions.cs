using Shelfie.Api.Books.DataAccess.Extensions;
using Shelfie.Api.Users.DataAccess.Extensions;
using Shelfie.Api.Interactions.DataAccess.Extensions;
using Shelfie.Api.Shelves.DataAccess.Extensions;
using Shelfie.Api.Users.Extensions;

namespace Shelfie.Api;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserApi(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddUserDataAccessLayer(configuration)
            .AddUserServices()
            .AddUserConfigurationOptions(configuration)
            ;

    public static IServiceCollection AddInteractionsApi(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddInteractionsDataAccessLayer(configuration)
            ;

    public static IServiceCollection AddBooksApi(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddBooksDataAccessLayer(configuration)
            ;

    public static IServiceCollection AddShelvesApi(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddShelvesDataAccessLayer(configuration)
            ;
}
