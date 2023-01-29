using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Interactions.Models.AddQuoteRequest;

public sealed record AddQuoteRequest
{
    [Required]
    public long BookId { get; init; }
}
