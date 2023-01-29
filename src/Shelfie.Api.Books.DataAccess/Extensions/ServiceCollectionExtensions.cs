using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Books.DataAccess.DataContexts;
using Shelfie.Api.Books.DataAccess.Infrastructure.Options;
using Shelfie.Api.Books.DataAccess.Repositories.BookRepository;
using Shelfie.Libs.Postgres.Extensions;

namespace Shelfie.Api.Books.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBooksDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostgres<BooksPostgresConnectionFactoryOptions>(configuration);

        services.AddTransient<IShelfieApiBooksDataContext, ShelfieApiBooksDataContext>();

        // Repositories
        services
            .AddTransient<IBookRepository, BookRepository>()
            ;

        return services;
    }
}
