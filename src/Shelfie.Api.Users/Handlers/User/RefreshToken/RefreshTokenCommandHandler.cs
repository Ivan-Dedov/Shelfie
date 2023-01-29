using MediatR;
using Microsoft.Extensions.Logging;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Models;
using Shelfie.Api.Users.Services.JwtService;
using Shelfie.Api.Users.Services.JwtService.Models;
using Shelfie.Libs.Common.Services.DateTimeProvider;

namespace Shelfie.Api.Users.Handlers.User.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken
) : IRequest<TokenPair>;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenPair>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly ILogger<RefreshTokenCommandHandler> _logger;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RefreshTokenCommandHandler(
        ITokenRepository tokenRepository,
        ILogger<RefreshTokenCommandHandler> logger,
        IJwtService jwtService,
        IDateTimeProvider dateTimeProvider
    )
    {
        _tokenRepository = tokenRepository;
        _logger = logger;
        _jwtService = jwtService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TokenPair> Handle(
        RefreshTokenCommand command,
        CancellationToken ct)
    {
        var token = await GetRefreshToken(command.RefreshToken, ct);
        if (token is null)
        {
            throw new ApplicationException("Неизвестный токен");
        }

        if (token.IsRevoked)
        {
            // Revoking descendant tokens in case this token has been compromised
            await _tokenRepository.RevokeDescendantRefreshTokens(token.RefreshToken, null, ct);
        }

        if (token.IsActive is false)
        {
            throw new ApplicationException("Токен недействителен");
        }

        var newRefreshToken = await RotateRefreshToken(token.RefreshToken, ct);

        await _tokenRepository.RevokeRefreshTokensByUserId(
            token.UserId,
            _dateTimeProvider.UtcNow,
            ct);

        var accessToken = _jwtService.CreateAccessToken(token.UserId);

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    private async Task<UserToken?> GetRefreshToken(
        string refreshToken,
        CancellationToken ct)
    {
        try
        {
            return await _tokenRepository.GetRefreshToken(refreshToken, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst fetching a refresh token");
            return null;
        }
    }

    private async Task<Services.JwtService.Models.RefreshToken> RotateRefreshToken(
        string oldToken,
        CancellationToken ct)
    {
        var newRefreshToken = await _jwtService.CreateRefreshToken(ct);
        await _tokenRepository.RevokeRefreshToken(
            oldToken,
            _dateTimeProvider.UtcNow,
            newRefreshToken.Token,
            ct);
        return newRefreshToken;
    }
}
