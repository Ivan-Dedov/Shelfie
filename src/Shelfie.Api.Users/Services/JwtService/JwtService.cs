using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Parameters;
using Shelfie.Api.Users.Infrastructure.Options;
using Shelfie.Api.Users.Services.JwtService.Models;
using Shelfie.Libs.Common.Services.DateTimeProvider;

namespace Shelfie.Api.Users.Services.JwtService;

public sealed class JwtService : IJwtService
{
    internal const int RefreshTokenLength = 64;

    // TODO: Make a call to remote vault to get the issuer and issuer credentials.
    // IMPORTANT: Change together with issuer from libs!
    private const string IssuerCredentials = "shelfie-token-source";
    private const string Issuer = "shelfie-api-users";

    private readonly AuthenticationOptions _authenticationOptions;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ITokenRepository _tokenRepository;
    private readonly ILogger<JwtService> _logger;

    public JwtService(
        IOptions<AuthenticationOptions> authenticationOptionsProvider,
        IDateTimeProvider dateTimeProvider,
        ITokenRepository tokenRepository,
        ILogger<JwtService> logger
    )
    {
        _authenticationOptions = authenticationOptionsProvider.Value;
        _dateTimeProvider = dateTimeProvider;
        _tokenRepository = tokenRepository;
        _logger = logger;
    }

    public async Task<TokenPair> CreateTokens(
        long userId,
        CancellationToken ct)
    {
        var accessToken = CreateAccessToken(userId);
        var refreshToken = await CreateRefreshToken(ct);

        try
        {
            await _tokenRepository.ChangeRefreshToken(
                new ChangeRefreshTokenParameters
                {
                    UserId = userId,
                    NewRefreshToken = refreshToken.Token,
                    CreatedAt = refreshToken.CreatedAt,
                    ExpiresAt = refreshToken.ExpiresAt,
                    Now = _dateTimeProvider.UtcNow,
                },
                ct);
        }
        catch (Exception ex)
        {
            // TODO: Check the scenario when a user successfully registers, but this method fails.
            _logger.LogError(ex, "Could not update refresh token information");
            throw new ApplicationException("Произошла неожиданная ошибка. Попробуйте войти заново");
        }

        return new TokenPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public string CreateAccessToken(
        long userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),

            Issuer = Issuer,
            Expires = _dateTimeProvider.UtcNow.Add(_authenticationOptions.AccessTokenLifetimeTimespan),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(IssuerCredentials)
                ),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<RefreshToken> CreateRefreshToken(
        CancellationToken ct)
    {
        string token;
        do
        {
            token = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(RefreshTokenLength));
        }
        while (await TokenAlreadyExists(token, ct));

        var now = _dateTimeProvider.UtcNow;
        return new RefreshToken
        {
            Token = token,
            CreatedAt = now,
            ExpiresAt = now.Add(_authenticationOptions.RefreshTokenLifetimeTimespan)
        };
    }

    private async Task<bool> TokenAlreadyExists(
        string token,
        CancellationToken ct)
    {
        try
        {
            var userWithToken = await _tokenRepository.GetRefreshToken(token, ct);
            return userWithToken is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst trying to get a user by the refresh token");
            return true;
        }
    }
}
