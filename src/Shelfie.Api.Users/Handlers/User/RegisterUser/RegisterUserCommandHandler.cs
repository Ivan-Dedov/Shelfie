using MediatR;
using Microsoft.Extensions.Logging;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Parameters;
using Shelfie.Api.Users.Services.JwtService;
using Shelfie.Api.Users.Services.JwtService.Models;
using Shelfie.Api.Users.Services.PasswordService;
using Shelfie.Libs.Common.Services.DateTimeProvider;

namespace Shelfie.Api.Users.Handlers.User.RegisterUser;

public sealed record RegisterUserCommand(
    string Username,
    string Email,
    string Password
) : IRequest<TokenPair>;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, TokenPair>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        ILogger<RegisterUserCommandHandler> logger,
        IJwtService jwtService,
        IDateTimeProvider dateTimeProvider
    )
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _logger = logger;
        _jwtService = jwtService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<TokenPair> Handle(
        RegisterUserCommand command,
        CancellationToken ct)
    {
        var (username, email, password) = command;
        if (await CheckUserAlreadyExists(email, ct))
        {
            throw new ApplicationException("Пользователь с таким адресом электронной почты уже существует");
        }

        var (hashedPassword, salt) = _passwordService.HashPassword(password);

        var user = await CreateUser(username, email, hashedPassword, salt, ct);
        return await _jwtService.CreateTokens(user.Id, ct);
    }

    private async Task<DataAccess.Repositories.UserRepository.Models.User> CreateUser(
        string username,
        string email,
        string hashedPassword,
        string salt,
        CancellationToken ct)
    {
        try
        {
            return await _userRepository.CreateUser(
                new CreateUserParameters
                {
                    Name = username,
                    Email = email,
                    HashedPassword = hashedPassword,
                    PasswordSalt = salt,
                    CreatedAt = _dateTimeProvider.UtcNow
                },
                ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst creating a new user");
            throw new ApplicationException("Произошла ошибка. Попробуйте зарегистрироваться заново");
        }
    }

    private async Task<bool> CheckUserAlreadyExists(
        string email,
        CancellationToken ct)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(email, ct);
            return user is not null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred whilst trying to fetch users");
            throw new ApplicationException("Произошла ошибка. Попробуйте зарегистрироваться заново");
        }
    }
}
