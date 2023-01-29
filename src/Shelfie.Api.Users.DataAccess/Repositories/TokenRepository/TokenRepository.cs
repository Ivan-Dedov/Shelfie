using System.Data;
using Dapper;
using Shelfie.Api.Users.DataAccess.DataContexts;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Models;
using Shelfie.Api.Users.DataAccess.Repositories.TokenRepository.Parameters;
using Shelfie.Api.Users.DataAccess.Schemas.Tables;

namespace Shelfie.Api.Users.DataAccess.Repositories.TokenRepository;

public sealed class TokenRepository : ITokenRepository
{
    private readonly IShelfieApiUsersDataContext _dataContext;

    public TokenRepository(
        IShelfieApiUsersDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<UserToken?> GetRefreshToken(
        string token,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Token", token);

        var command = new CommandDefinition($@"-- Get refresh token
            SELECT
              ut.{UserTokenSchema.RefreshToken} AS {nameof(UserTokenDao.RefreshToken)}
            , ut.{UserTokenSchema.UserId} AS {nameof(UserTokenDao.UserId)}
            , ut.{UserTokenSchema.ExpiresAt} AS {nameof(UserTokenDao.ExpiresAt)}
            , ut.{UserTokenSchema.CreatedAt} AS {nameof(UserTokenDao.CreatedAt)}
            , ut.{UserTokenSchema.RevokedAt} AS {nameof(UserTokenDao.RevokedAt)}
            , ut.{UserTokenSchema.ReplacedBy} AS {nameof(UserTokenDao.ReplacedBy)}
            FROM
              {UserTokenSchema.FullTableName} ut
            WHERE
              ut.{UserTokenSchema.RefreshToken} = @Token
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var userTokenDao = await connection.QueryFirstOrDefaultAsync<UserTokenDao>(command);
        return userTokenDao is null
            ? null
            : new UserToken(userTokenDao);
    }

    public async Task ChangeRefreshToken(
        ChangeRefreshTokenParameters parameters,
        CancellationToken ct)
    {
        await using var connection = await _dataContext.GetConnection(ct);
        await using var transaction = await connection.BeginTransactionAsync(ct);

        var insertParameters = new DynamicParameters();
        insertParameters.Add("RefreshToken", parameters.NewRefreshToken);
        insertParameters.Add("UserId", parameters.UserId);
        insertParameters.Add("CreatedAt", parameters.CreatedAt);
        insertParameters.Add("ExpiresAt", parameters.ExpiresAt);

        var insertCommand = new CommandDefinition($@"-- Insert token
            INSERT INTO {UserTokenSchema.FullTableName} (
              {UserTokenSchema.RefreshToken}
            , {UserTokenSchema.UserId}
            , {UserTokenSchema.CreatedAt}
            , {UserTokenSchema.ExpiresAt}
            )
            VALUES
            (
              @RefreshToken
            , @UserId
            , @CreatedAt
            , @ExpiresAt
            )  
            ;",
            insertParameters,
            transaction: transaction,
            cancellationToken: ct);

        await connection.ExecuteAsync(insertCommand);
        await RevokeRefreshTokensByUserId(
            parameters.UserId,
            parameters.Now,
            transaction: transaction,
            cancellationToken: ct);
    }

    public async Task RevokeRefreshToken(
        string token,
        DateTime revokedAt,
        string? replacedBy = null,
        CancellationToken ct = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Token", token);
        parameters.Add("RevokedAt", revokedAt);
        parameters.Add("ReplacedBy", replacedBy); // TODO: can i just set it to null if replacement is not required

        var command = new CommandDefinition($@"-- Revoke token
            UPDATE
              {UserTokenSchema.FullTableName}
            SET
              {UserTokenSchema.RevokedAt} = @RevokedAt
            , {UserTokenSchema.ReplacedBy} = @ReplacedBy
            WHERE
              {UserTokenSchema.RefreshToken} = @Token
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);
        await connection.ExecuteAsync(command);
    }

    public async Task RevokeDescendantRefreshTokens(
        string token,
        string replacedBy,
        CancellationToken ct)
    {
        //todo
        //recursively traverse the refresh token chain and ensure all descendants are revoked
        // if (!string.IsNullOrEmpty(replacedBy))
        // {
        //     var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
        //     if (childToken.IsActive)
        //         revokeRefreshToken(childToken, ipAddress, reason);
        //     else
        //         revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        // }
    }

    public async Task RevokeRefreshTokensByUserId(
        long userId,
        DateTime now,
        CancellationToken ct)
    {
        await RevokeRefreshTokensByUserId(
            userId,
            now,
            transaction: null,
            cancellationToken: ct);
    }

    private async Task RevokeRefreshTokensByUserId(
        long userId,
        DateTime now,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);
        parameters.Add("Now", now);

        var command = new CommandDefinition($@"-- Revoke token
            DELETE FROM
              {UserTokenSchema.FullTableName}
            WHERE
              {UserTokenSchema.UserId} = @UserId
              AND ( {UserTokenSchema.RevokedAt} IS NOT NULL
                    OR {UserTokenSchema.ExpiresAt} >= @Now )
            ;",
            parameters,
            transaction: transaction,
            cancellationToken: cancellationToken);

        await using var connection = await _dataContext.GetConnection(cancellationToken);
        await connection.ExecuteAsync(command);
    }
}
