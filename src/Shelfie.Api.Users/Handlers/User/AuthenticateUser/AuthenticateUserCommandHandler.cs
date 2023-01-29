using MediatR;
using Shelfie.Api.Users.DataAccess.Repositories.UserRepository;
using Shelfie.Api.Users.Services.JwtService;
using Shelfie.Api.Users.Services.JwtService.Models;
using Shelfie.Api.Users.Services.PasswordService;

namespace Shelfie.Api.Users.Handlers.User.AuthenticateUser;

public sealed record AuthenticateUserCommand(
    string Email,
    string Password
) : IRequest<TokenPair>;

public sealed class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, TokenPair>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthenticateUserCommandHandler(
        IJwtService jwtService,
        IUserRepository userRepository,
        IPasswordService passwordService
    )
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<TokenPair> Handle(
        AuthenticateUserCommand command,
        CancellationToken ct)
    {
        var user = await _userRepository.GetUserByEmail(
            command.Email,
            ct);

        if (user is null)
        {
            throw new ApplicationException("Пользователь с таким адресом электронной почты не найден");
        }

        if (_passwordService.ValidatePassword(command.Password, user.HashedPassword, user.PasswordSalt) is false)
        {
            throw new ApplicationException("Неверный логин или пароль");
        }

        return await _jwtService.CreateTokens(user.Id, ct);
    }
}
