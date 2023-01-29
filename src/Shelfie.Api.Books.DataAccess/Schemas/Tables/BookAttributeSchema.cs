namespace Shelfie.Api.Books.DataAccess.Schemas.Tables;

public static class BookAttributeSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "book_attribute";

    // Columns
    public const string Id = "id";
    public const string BookId = "book_id";
    public const string AttributeId = "attribute_id";
}
