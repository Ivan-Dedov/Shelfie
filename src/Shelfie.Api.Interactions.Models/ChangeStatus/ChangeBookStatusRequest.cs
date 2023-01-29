namespace Shelfie.Api.Interactions.Models.ChangeStatus;

public sealed record ChangeBookStatusRequest
{
    public long BookId { get; init; }

    public BookStatus BookStatus { get; init; }
}
