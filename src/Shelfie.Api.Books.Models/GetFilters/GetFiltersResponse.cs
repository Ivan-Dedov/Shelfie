using System.ComponentModel.DataAnnotations;
using Shelfie.Api.Books.Models.Common;

namespace Shelfie.Api.Books.Models.GetFilters;

public sealed record GetFiltersResponse
{
    [Required]
    public Dictionary<string, BookAttribute> FiltersByCategory { get; init; }
}
