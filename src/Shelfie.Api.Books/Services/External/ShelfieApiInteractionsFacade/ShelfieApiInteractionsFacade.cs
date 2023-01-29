using MediatR;
using Microsoft.Extensions.Logging;

namespace Shelfie.Api.Books.Services.External.ShelfieApiInteractionsFacade;

public sealed class ShelfieApiInteractionsFacade : IShelfieApiInteractionsFacade
{
    private readonly IMediator _mediator;
    private readonly ILogger<ShelfieApiInteractionsFacade> _logger;

    public ShelfieApiInteractionsFacade(
        IMediator mediator,
        ILogger<ShelfieApiInteractionsFacade> logger
    )
    {
        _mediator = mediator;
        _logger = logger;
    }
}
