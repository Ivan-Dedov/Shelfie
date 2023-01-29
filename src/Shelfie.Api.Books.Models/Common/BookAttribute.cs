using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Books.Models.Common;

public sealed record BookAttribute
{
    [Required]
    public long Id { get; init; }

    [Required]
    public string Name { get; init; }
}
