using MediatR;
using Microsoft.Extensions.Logging;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;
using Shelfie.Libs.Common.Services.DateTimeProvider;

namespace Shelfie.Api.Users.Handlers.User.RevokeToken;

public sealed record RevokeTokenCommand(
    string RefreshToken
) : IRequest<Unit>;

public sealed class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, Unit>
{
    private readonly ITokenRepository _tokenRepository;
    private readonly ILogger<RevokeTokenCommandHandler> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RevokeTokenCommandHandler(
        ITokenRepository tokenRepository,
        ILogger<RevokeTokenCommandHandler> logger,
        IDateTimeProvider dateTimeProvider
    )
    {
        _tokenRepository = tokenRepository;
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Unit> Handle(
        RevokeTokenCommand command,
        CancellationToken ct)
    {
        var userToken = await _tokenRepository.GetRefreshToken(command.RefreshToken, ct);
        if (userToken?.IsActive is not true)
        {
            throw new ApplicationException("Невалидный токен");
        }

        await RevokeToken(userToken.RefreshToken, ct);

        return Unit.Value;
    }

    private async Task RevokeToken(
        string token,
        CancellationToken ct)
    {
        try
        {
            await _tokenRepository.RevokeRefreshToken(
                token,
                _dateTimeProvider.UtcNow,
                cancellationToken: ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst revoking a refresh token '{Token}'", token);
            throw new ApplicationException("Не удалось отменить токен");
        }
    }
}
