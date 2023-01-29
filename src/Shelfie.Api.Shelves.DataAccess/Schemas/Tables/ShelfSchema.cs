namespace Shelfie.Api.Shelves.DataAccess.Schemas.Tables;

public static class ShelfSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "shelf";

    // Columns
    // todo
}
