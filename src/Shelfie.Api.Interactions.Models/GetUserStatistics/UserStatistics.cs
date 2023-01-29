namespace Shelfie.Api.Interactions.Models.GetUserStatistics;

public sealed record UserStatistics
{
    public int BookCount { get; init; }

    public int ReviewCount { get; init; }

    public int QuoteCount { get; init; }
}
