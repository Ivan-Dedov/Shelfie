namespace Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Parameters;

public sealed record CreateUserParameters
{
    public string Name { get; init; }

    public string Email { get; init; }

    public string HashedPassword { get; init; }

    public string PasswordSalt { get; init; }

    public DateTime CreatedAt { get; init; }
}
