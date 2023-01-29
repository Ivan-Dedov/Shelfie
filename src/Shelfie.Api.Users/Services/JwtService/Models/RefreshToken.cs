namespace Shelfie.Api.Users.Services.JwtService.Models;

public struct RefreshToken
{
    public string Token { get; init; }

    public DateTime ExpiresAt { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? RevokedAt { get; init; }
}
