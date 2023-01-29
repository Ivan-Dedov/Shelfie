using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Shelves.Models.GetShelfContents;

public sealed record GetShelfContentsResponse
{
    [Required]
    public List<ShelfBook> Contents { get; init; }
}
