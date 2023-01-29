using Microsoft.Extensions.DependencyInjection;

namespace Shelfie.Api.Shelves.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShelvesServices(this IServiceCollection services)
    {
        return services;
    }
}
