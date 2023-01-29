using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Shelves.Models.GetCommonShelves;

public sealed record Shelf
{
    [Required]
    public long Id { get; init; }

    [Required]
    public string Name { get; init; }

    public string? Description { get; init; }

    public string? ImageUrl { get; init; }
}
