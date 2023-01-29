namespace Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Models;

public sealed record UserDao
{
    public long Id { get; init; }

    public string Name { get; init; }

    public string Email { get; init; }

    public string HashedPassword { get; init; }

    public string PasswordSalt { get; init; }

    public string CreatedAt { get; init; }

    public string? ProfileImageUrl { get; init; }

    public string? BannerImageUrl { get; init; }
}
