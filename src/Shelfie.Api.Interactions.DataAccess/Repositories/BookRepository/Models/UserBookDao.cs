namespace Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository.Models;

public sealed record UserBookDao
{
    public long BookId { get; init; }

    public string? BookStatus { get; init; }
}
