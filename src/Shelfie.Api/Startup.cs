using Shelfie.Libs.Common.Extensions;

namespace Shelfie.Api;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddShelfieControllers<Startup>()
            .AddUserApi(_configuration)
            .AddInteractionsApi(_configuration)
            .AddBooksApi(_configuration)
            ;
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseShelfieMiddleware();
    }
}
