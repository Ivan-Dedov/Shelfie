using System.ComponentModel.DataAnnotations;
using Shelfie.Api.Books.Models.Common;

namespace Shelfie.Api.Books.Models.GetBook;

public sealed record FoundBook
{
    [Required]
    public long Id { get; init; }

    [Required]
    public string Title { get; init; }

    public string? CoverImageUrl { get; init; }

    [Required]
    public List<BookAuthor> Authors { get; init; }

    [Required]
    public double? Rating { get; init; }

    [Required]
    public BookStatus Status { get; init; }

    [Required]
    public List<BookAttribute> Genres { get; init; }

    [Required]
    public List<BookAttribute> Countries { get; init; }

    [Required]
    public List<BookAttribute> OtherAttributes { get; init; }
}
