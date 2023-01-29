using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.RefreshToken;

public sealed record RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; init; }
}
