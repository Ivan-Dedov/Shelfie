using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Shelfie.Libs.Common.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureShelfie<TStartup>(this IHostBuilder builder)
        where TStartup : class
    {
        builder.ConfigureWebHostDefaults(_ =>
        {
            _.UseStartup<TStartup>();
        });

        return builder;
    }
}
