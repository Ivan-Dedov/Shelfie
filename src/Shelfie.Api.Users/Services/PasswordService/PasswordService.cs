using System.Security.Cryptography;
using System.Text;

namespace Shelfie.Api.Users.Services.PasswordService;

public sealed class PasswordService : IPasswordService
{
    internal const int MinPasswordLength = 8;
    internal const int MaxPasswordLength = 50;

    private const string Salt = "a7HdIDvsyT0bidsrx3dpTQDvOrSOmhGEJiZfRuMxVpc8ojfafKowgZFYQZINflJy";
    private const int SaltLength = 32;

    public (string HashedPassword, string Salt) HashPassword(
        string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltLength);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var localSaltBytes = Encoding.UTF8.GetBytes(Salt);

        var hashAlgorithm = SHA256.Create();
        var saltyPassword = passwordBytes
            .Concat(saltBytes)
            .Concat(localSaltBytes);
        var hashedPasswordBytes = hashAlgorithm.ComputeHash(saltyPassword.ToArray());

        var hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
        var salt = Convert.ToBase64String(saltBytes);

        return (hashedPassword, salt);
    }

    public bool ValidatePassword(
        string password,
        string truePassword,
        string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var passwordStr = Encoding.UTF8.GetBytes(password)
            .Concat(saltBytes)
            .Concat(Encoding.UTF8.GetBytes(Salt));

        var hashAlgorithm = SHA256.Create();
        var hashedPassword = Convert.ToBase64String(
            hashAlgorithm.ComputeHash(passwordStr.ToArray()));

        return hashedPassword == truePassword;
    }
}
