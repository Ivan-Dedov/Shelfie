using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Interactions.Models.AddReviewRequest;

public sealed record AddReviewRequest
{
    [Required]
    public long BookId { get; init; }
}
