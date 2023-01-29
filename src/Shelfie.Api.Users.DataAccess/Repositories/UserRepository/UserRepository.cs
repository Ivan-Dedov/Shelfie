using Dapper;
using Shelfie.Api.Users.DataAccess.DataContexts;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Models;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Parameters;
using Shelfie.Api.Users.DataAccess.Schemas.Tables;

namespace Shelfie.Api.Users.DataAccess.Repositories.UserRepository;

public sealed class UserRepository : IUserRepository
{
    private readonly IShelfieApiUsersDataContext _dataContext;

    public UserRepository(
        IShelfieApiUsersDataContext dataContext
    )
    {
        _dataContext = dataContext;
    }

    public async Task<User?> GetUserById(
        long userId,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);

        var command = new CommandDefinition($@"-- Get user by ID
            SELECT
              u.{UserSchema.Id} AS {nameof(UserDao.Id)}
            , u.{UserSchema.Name} AS {nameof(UserDao.Name)}
            , u.{UserSchema.Email} AS {nameof(UserDao.Email)}
            , u.{UserSchema.HashedPassword} AS {nameof(UserDao.HashedPassword)}
            , u.{UserSchema.PasswordSalt} AS {nameof(UserDao.PasswordSalt)}
            , u.{UserSchema.CreatedAt} AS {nameof(UserDao.CreatedAt)}
            , u.{UserSchema.ProfileImageUrl} AS {nameof(UserDao.ProfileImageUrl)}
            , u.{UserSchema.BannerImageUrl} AS {nameof(UserDao.BannerImageUrl)}
            FROM
              {UserSchema.FullTableName} u
            WHERE
              u.{UserSchema.Id} = @UserId
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var userDao = await connection.QueryFirstOrDefaultAsync<UserDao>(command);
        return userDao is null
            ? null
            : new User(userDao);
    }

    public async Task<User?> GetUserByEmail(
        string email,
        CancellationToken ct)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Email", email);

        var command = new CommandDefinition($@"-- Get user by e-mail
            SELECT
              u.{UserSchema.Id} AS {nameof(UserDao.Id)}
            , u.{UserSchema.Name} AS {nameof(UserDao.Name)}
            , u.{UserSchema.Email} AS {nameof(UserDao.Email)}
            , u.{UserSchema.HashedPassword} AS {nameof(UserDao.HashedPassword)}
            , u.{UserSchema.PasswordSalt} AS {nameof(UserDao.PasswordSalt)}
            , u.{UserSchema.CreatedAt} AS {nameof(UserDao.CreatedAt)}
            , u.{UserSchema.ProfileImageUrl} AS {nameof(UserDao.ProfileImageUrl)}
            , u.{UserSchema.BannerImageUrl} AS {nameof(UserDao.BannerImageUrl)}
            FROM
              {UserSchema.FullTableName} u
            WHERE
              u.{UserSchema.Email} = @Email
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var userDao = await connection.QueryFirstOrDefaultAsync<UserDao>(command);
        return userDao is null
            ? null
            : new User(userDao);
    }

    public async Task<User> CreateUser(
        CreateUserParameters parameters,
        CancellationToken ct)
    {
        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("Name", parameters.Name);
        dynamicParameters.Add("Email", parameters.Email);
        dynamicParameters.Add("HashedPassword", parameters.HashedPassword);
        dynamicParameters.Add("PasswordSalt", parameters.PasswordSalt);
        dynamicParameters.Add("CreatedAt", parameters.CreatedAt);
        dynamicParameters.Add("ProfileImageUrl", null);
        dynamicParameters.Add("BannerImageUrl", null);

        var command = new CommandDefinition($@"-- Insert new user
            INSERT INTO {UserSchema.FullTableName} (
              {UserSchema.Name}
            , {UserSchema.Email}
            , {UserSchema.HashedPassword}
            , {UserSchema.PasswordSalt}
            , {UserSchema.CreatedAt}
            , {UserSchema.ProfileImageUrl}
            , {UserSchema.BannerImageUrl}
            )
            VALUES
            (
              @Name
            , @Email
            , @HashedPassword
            , @PasswordSalt
            , @CreatedAt
            , @ProfileImageUrl
            , @BannerImageUrl
            )  
            RETURNING
              {UserSchema.Id} AS {nameof(UserDao.Id)}
            , {UserSchema.Name} AS {nameof(UserDao.Name)}
            , {UserSchema.Email} AS {nameof(UserDao.Email)}
            , {UserSchema.HashedPassword} AS {nameof(UserDao.HashedPassword)}
            , {UserSchema.PasswordSalt} AS {nameof(UserDao.PasswordSalt)}
            , {UserSchema.CreatedAt} AS {nameof(UserDao.CreatedAt)}
            , {UserSchema.ProfileImageUrl} AS {nameof(UserDao.ProfileImageUrl)}
            , {UserSchema.BannerImageUrl} AS {nameof(UserDao.BannerImageUrl)}
            ;",
            parameters,
            cancellationToken: ct);

        await using var connection = await _dataContext.GetConnection(ct);

        var userDao = await connection.QueryFirstOrDefaultAsync<UserDao>(command);
        return new User(userDao);
    }
}
