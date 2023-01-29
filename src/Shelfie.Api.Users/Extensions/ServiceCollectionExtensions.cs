using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shelfie.Api.Users.Infrastructure.Options;
using Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade;
using Shelfie.Api.Users.Services.JwtService;
using Shelfie.Api.Users.Services.PasswordService;
using Shelfie.Libs.Common.Extensions;

namespace Shelfie.Api.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserServices(this IServiceCollection services)
    {
        services
            .AddTransient<IJwtService, JwtService>()
            .AddDateTimeProvider()
            .AddTransient<IPasswordService, PasswordService>()
            .AddTransient<IShelfieApiInteractionsFacade, ShelfieApiInteractionsFacade>()
            ;

        return services;
    }

    public static IServiceCollection AddUserConfigurationOptions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuthenticationOptions>(_ =>
        {
            configuration
                .GetRequiredSection(nameof(AuthenticationOptions))
                .Bind(_)
                ;
        });

        return services;
    }
}
