namespace Shelfie.Api.Books.DataAccess.Schemas.Tables;

public static class BookSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "book";

    // Columns
    // todo
}
