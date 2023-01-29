using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.GetUserInfo;

public sealed record UserStatistics
{
    [Required]
    public int BookCount { get; init; }

    [Required]
    public int ReviewCount { get; init; }

    [Required]
    public int QuoteCount { get; init; }
}
