namespace Shelfie.Api.Interactions.DataAccess.Schemas.Tables;

public static class BookStatusSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "book_status";

    // Columns
    public const string Id = "id";
    public const string BookId = "book_id";
    public const string UserId = "user_id";
    public const string BookStatus = "book_status";
    public const string CreatedAt = "created_at";
}
