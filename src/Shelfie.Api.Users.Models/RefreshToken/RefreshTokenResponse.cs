using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.RefreshToken;

public sealed record RefreshTokenResponse
{
    [Required]
    public string AccessToken { get; init; }

    [Required]
    public string RefreshToken { get; init; }
}
