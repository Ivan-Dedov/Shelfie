using Microsoft.AspNetCore.Builder;
using Shelfie.Libs.Common.Helpers;
using Shelfie.Libs.Common.Middleware;

namespace Shelfie.Libs.Common.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseShelfieMiddleware(this IApplicationBuilder builder)
    {
        builder
            .UseHttpsRedirection()
            .UseRouting()
            .UseShelfieAuthentication()
            .UseShelfieSwagger()
            ;

        // Exception handling
        builder.UseMiddleware<ExceptionHandlingMiddleware>();

        builder.UseEndpoints(_ =>
        {
            _
                .MapControllers()
                .RequireAuthorization()
                ;
        });

        return builder;
    }

    public static IApplicationBuilder UseShelfieAuthentication(this IApplicationBuilder builder)
    {
        builder
            .UseAuthentication()
            .UseAuthorization()
            ;

        return builder;
    }

    public static IApplicationBuilder UseShelfieSwagger(this IApplicationBuilder builder)
    {
        builder
            .UseSwagger()
            .UseSwaggerUI(_ =>
            {
                _.SwaggerEndpoint(
                    $"/swagger/{AssemblyHelper.EntryAssemblyVersion}/swagger.json",
                    AssemblyHelper.EntryAssemblyName);
            })
            ;

        return builder;
    }
}
