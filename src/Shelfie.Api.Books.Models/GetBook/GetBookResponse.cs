using System.ComponentModel.DataAnnotations;

namespace Shelfie.Api.Books.Models.GetBook;

public sealed record GetBookResponse
{
    [Required]
    public List<FoundBook> Books { get; init; }

    [Required]
    public int Count => Books.Count;
}
