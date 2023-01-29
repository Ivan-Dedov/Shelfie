using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Books.Services.External.ShelfieApiInteractionsFacade;

namespace Shelfie.Api.Books.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBooksServices(this IServiceCollection services)
    {
        services
            .AddTransient<IShelfieApiInteractionsFacade, ShelfieApiInteractionsFacade>()
            ;

        return services;
    }
}
