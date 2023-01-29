using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Interactions.DataAccess.DataContexts;
using Shelfie.Api.Interactions.DataAccess.Infrastructure.Options;
using Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository;
using Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository;
using Shelfie.Libs.Postgres.Extensions;

namespace Shelfie.Api.Interactions.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInteractionsDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPostgres<InteractionsPostgresConnectionFactoryOptions>(configuration);

        services.AddTransient<IShelfieApiInteractionsDataContext, ShelfieApiInteractionsDataContext>();

        // Repositories
        services
            .AddTransient<IQuoteRepository, QuoteRepository>()
            .AddTransient<IReviewRepository, ReviewRepository>()
            ;

        return services;
    }
}
