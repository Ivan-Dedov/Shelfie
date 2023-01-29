using System.Globalization;

namespace Shelfie.Api.Users.Infrastructure.Options;

public sealed record AuthenticationOptions
{
    private const string TimeSpanFormat = "c";

    private const string DefaultAccessTokenLifetime = "0.00:05:00";
    private const string DefaultRefreshTokenLifetime = "7.00:00:00";

    private string AccessTokenLifetime { get; init; }

    public TimeSpan AccessTokenLifetimeTimespan
        => TimeSpan.ParseExact(AccessTokenLifetime, TimeSpanFormat, CultureInfo.InvariantCulture);

    private string RefreshTokenLifetime { get; init; }

    public TimeSpan RefreshTokenLifetimeTimespan
        => TimeSpan.ParseExact(RefreshTokenLifetime, TimeSpanFormat, CultureInfo.InvariantCulture);
}
