using Shelfie.Api.Users.Services.JwtService.Models;

namespace Shelfie.Api.Users.Services.JwtService;

public interface IJwtService
{
    string CreateAccessToken(
        long userId
    );

    Task<RefreshToken> CreateRefreshToken(
        CancellationToken cancellationToken
    );

    Task<TokenPair> CreateTokens(
        long userId,
        CancellationToken cancellationToken
    );
}
