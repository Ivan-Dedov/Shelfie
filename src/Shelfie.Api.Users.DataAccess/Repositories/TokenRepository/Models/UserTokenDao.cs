namespace Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Models;

public sealed record UserTokenDao
{
    public string RefreshToken { get; init; }

    public long UserId { get; init; }

    public string CreatedAt { get; init; }

    public string ExpiresAt { get; init; }

    public string? RevokedAt { get; init; }

    public string? ReplacedBy { get; init; }
}
