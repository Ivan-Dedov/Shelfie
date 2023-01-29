using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Models;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Parameters;

namespace Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;

public interface ITokenRepository
{
    Task<UserToken?> GetRefreshToken(
        string token,
        CancellationToken cancellationToken
    );

    Task ChangeRefreshToken(
        ChangeRefreshTokenParameters parameters,
        CancellationToken cancellationToken
    );

    Task RevokeRefreshToken(
        string token,
        DateTime revokedAt,
        string? replacedBy = null,
        CancellationToken cancellationToken = default
    );

    Task RevokeDescendantRefreshTokens(
        string token,
        string replacedBy,
        CancellationToken ct
    );

    Task RevokeRefreshTokensByUserId(
        long userId,
        DateTime now,
        CancellationToken cancellationToken
    );
}
