namespace Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository.Models;

public sealed record QuoteDao
{
    public long Id { get; init; }

    public long BookId { get; init; }

    public long UserId { get; init; }

    public string Text { get; init; }

    public DateTime CreatedAt { get; init; }
}
