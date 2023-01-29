namespace Shelfie.Api.Users.DataAccess.Schemas.Tables;

public static class UserTokenSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "user_token";

    // Columns
    public const string RefreshToken = "refresh_token";
    public const string UserId = "user_id";
    public const string ExpiresAt = "expires_at";
    public const string CreatedAt = "created_at";
    public const string RevokedAt = "revoked_at";
    public const string ReplacedBy = "replaced_by";
}
