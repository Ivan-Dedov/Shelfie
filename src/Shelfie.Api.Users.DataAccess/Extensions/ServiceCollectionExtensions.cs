using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Users.DataAccess.DataContexts;
using Shelfie.Api.Users.DataAccess.Infrastructure.Options;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository;
using Shelfie.Libs.Postgres.Extensions;

namespace Shelfie.Api.Users.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostgres<UserPostgresConnectionFactoryOptions>(configuration);

        services.AddTransient<IShelfieApiUsersDataContext, ShelfieApiUsersDataContext>();

        // Repositories
        services
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<ITokenRepository, TokenRepository>()
            ;

        return services;
    }
}
