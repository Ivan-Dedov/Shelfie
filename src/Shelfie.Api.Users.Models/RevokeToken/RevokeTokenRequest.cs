using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Users.Models.RevokeToken;

public sealed record RevokeTokenRequest
{
    [Required]
    public string RefreshToken { get; init; }
}
