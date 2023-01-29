namespace Shelfie.Api.Interactions.DataAccess.Schemas.Tables;

public static class BookQuoteSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "book_quote";

    // Columns
    public const string Id = "id";
    public const string BookId = "book_id";
    public const string UserId = "user_id";
    public const string Text = "text";
    public const string CreatedAt = "created_at";
}
