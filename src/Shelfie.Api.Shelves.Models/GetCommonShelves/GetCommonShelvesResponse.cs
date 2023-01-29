using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Shelves.Models.GetCommonShelves;

public sealed record GetCommonShelvesResponse
{
    [Required]
    public List<Shelf> Shelves { get; init; }
}
