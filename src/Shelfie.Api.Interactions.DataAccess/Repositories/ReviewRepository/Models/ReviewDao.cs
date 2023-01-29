namespace Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository.Models;

public sealed record ReviewDao
{
    public long Id { get; init; }

    public long BookId { get; init; }

    public long ReviewerId { get; init; }

    public string Text { get; init; }

    public int Rating { get; init; }

    public DateTime CreatedAt { get; init; }
}
