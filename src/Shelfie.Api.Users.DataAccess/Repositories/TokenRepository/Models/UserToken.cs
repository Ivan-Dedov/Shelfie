namespace Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Models;

public sealed record UserToken
{
    public string RefreshToken { get; }

    public long UserId { get; }

    public DateTime CreatedAt { get; }

    public DateTime ExpiresAt { get; }

    public DateTime? RevokedAt { get; }

    public string? ReplacedBy { get; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsRevoked => RevokedAt is not null;

    public bool IsActive => IsRevoked is false && IsExpired is false;

    public UserToken(UserTokenDao dao)
    {
        RefreshToken = dao.RefreshToken;
        UserId = dao.UserId;
        CreatedAt = DateTime.Parse(dao.CreatedAt);
        ExpiresAt = DateTime.Parse(dao.ExpiresAt);
        RevokedAt = dao.RevokedAt is null
            ? null
            : DateTime.Parse(dao.RevokedAt);
        ReplacedBy = dao.ReplacedBy;
    }
}
