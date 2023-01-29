namespace Shelfie.Api.Books.DataAccess.Schemas.Tables;

public static class AttributeSchema
{
    // Common code (move to interface on .NET 7)
    public const string SchemaName = PublicSchema.Name;
    public const string FullTableName = $"{SchemaName}.{TableName}";

    public const string TableName = "attributs";

    // Columns
    public const string Id = "id";
    public const string AttributeCategoryId = "attribute_category_id";
    public const string Name = "name";
}
