namespace Shelfie.Api.Users.DataAccess.Schemas.Tables;

public static class UserSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "user";

    // Columns
    public const string Id = "id";
    public const string Name = "name";
    public const string Email = "email";
    public const string HashedPassword = "hashed_password";
    public const string PasswordSalt = "password_salt";
    public const string CreatedAt = "created_at";
    public const string ProfileImageUrl = "profile_image_url";
    public const string BannerImageUrl = "banner_image_url";
}
