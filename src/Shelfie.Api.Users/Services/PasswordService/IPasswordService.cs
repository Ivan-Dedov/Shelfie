namespace Shelfie.Api.Users.Services.PasswordService;

public interface IPasswordService
{
    (string HashedPassword, string Salt) HashPassword(
        string password);

    bool ValidatePassword(
        string password,
        string truePassword,
        string salt);
}
