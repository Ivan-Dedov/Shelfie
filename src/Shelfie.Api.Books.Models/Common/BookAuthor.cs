using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Books.Models.Common;

public sealed record BookAuthor
{
    [Required]
    public long Id { get; init; }

    [Required]
    public long Name { get; init; }
}
