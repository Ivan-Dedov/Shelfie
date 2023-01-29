namespace Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Parameters;

public sealed record ChangeRefreshTokenParameters
{
    public long UserId { get; init; }

    public string NewRefreshToken { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime ExpiresAt { get; init; }

    public DateTime Now { get; init; }
}
