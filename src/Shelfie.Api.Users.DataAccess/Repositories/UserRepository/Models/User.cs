namespace Shelfie.Api.Users.DataAccess.Repositories.UserRepository.Models;

public sealed record User
{
    public long Id { get; }

    public string Name { get; }

    public string Email { get; }

    public string HashedPassword { get; }

    public string PasswordSalt { get; }

    public DateTime? CreatedAt { get; }

    public string? ProfileImageUrl { get; }

    public string? BannerImageUrl { get; }

    public User(UserDao dao)
    {
        Id = dao.Id;
        Name = dao.Name;
        Email = dao.Email;
        HashedPassword = dao.HashedPassword;
        PasswordSalt = dao.PasswordSalt;
        CreatedAt = DateTime.TryParse(dao.CreatedAt, out var createdAt)
            ? createdAt
            : null;
        ProfileImageUrl = dao.ProfileImageUrl;
        BannerImageUrl = dao.BannerImageUrl;
    }
}
