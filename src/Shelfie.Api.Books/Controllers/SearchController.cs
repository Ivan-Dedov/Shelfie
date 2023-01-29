using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Books.Handlers.Search.GetBooksByIsbnCommandHandler;
using Shelfie.Api.Books.Handlers.Search.GetBooksCommandHandler;
using Shelfie.Api.Books.Handlers.Search.GetFilters;
using Shelfie.Api.Books.Models.GetBook;
using Shelfie.Api.Books.Models.GetFilters;

namespace Shelfie.Api.Books.Controllers;

[ApiController,
 Route("books/search")]
public sealed class SearchController : Controller
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Retrieve all available filters for books.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>All distinct book filters from the database.</returns>
    [HttpGet("filters")]
    public async Task<GetFiltersResponse> GetFilters(
        CancellationToken ct)
    {
        var response = await _mediator.Send(
            new GetFiltersCommand(),
            ct);

        return response;
    }

    /// <summary>
    /// Searches the books using the given filter.
    /// </summary>
    /// <param name="userId">The ID of the user performing the search.</param>
    /// <param name="query">The search query.</param>
    /// <param name="genreIds">The IDs of the genres to filter by.</param>
    /// <param name="countryIds">The IDs of the countries to filter by.</param>
    /// <param name="minRating">The minimum rating of books.</param>
    /// <param name="attributeIds">The IDs other (uncategorized) book attributes to filter by.</param>
    /// <param name="take">The number of books to display.</param>
    /// <param name="skip">The number of books to skip.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The books that match the search conditions.</returns>
    [HttpGet]
    public async Task<IReadOnlyCollection<GetBookResponse>> SearchBooks(
        [Required, FromHeader] long userId,
        [FromQuery] string? query,
        [FromQuery] IReadOnlyCollection<long>? genreIds,
        [FromQuery] IReadOnlyCollection<long>? countryIds,
        [FromQuery] int? minRating,
        [FromQuery] IReadOnlyCollection<long>? attributeIds,
        [Required, FromQuery] int take,
        [Required, FromQuery] int skip,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetBooksCommand(
                userId,
                query,
                genreIds,
                countryIds,
                minRating,
                attributeIds,
                take,
                skip
            ),
            ct);

        return result;
    }

    /// <summary>
    /// Searches the books via ISBN.
    /// </summary>
    /// <param name="userId">The ID of the user performing the search.</param>
    /// <param name="isbn">ISBN.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The book with the given ISBN.</returns>
    [HttpGet("{isbn:length(13)}")]
    public async Task<GetBookResponse> SearchBooksByIsbn(
        [Required, FromHeader] long userId,
        [Required, FromRoute] string isbn,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new GetBooksByIsbnCommand(
                userId,
                isbn),
            ct);

        return result;
    }
}
