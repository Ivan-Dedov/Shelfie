using MediatR;
using Microsoft.Extensions.Logging;
using Shelfie.Api.Interactions.Handlers.Statistics.GetUserStatistics;
using Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade.Models;

namespace Shelfie.Api.Users.Services.External.ShelfieApiInteractionsFacade;

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

    public async Task<Statistics> GetStatisticsByUserId(
        long userId,
        CancellationToken ct)
    {
        try
        {
            var response = await _mediator.Send(
                new GetUserStatisticsCommand(userId),
                ct);

            return new Statistics
            {
                BookCount = response.BookCount,
                QuoteCount = response.QuoteCount,
                ReviewCount = response.ReviewCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Could not retrieve user statistics (from shelfie-api-interactions)");
            return new Statistics
            {
                BookCount = 0,
                ReviewCount = 0,
                QuoteCount = 0
            };
        }
    }
}
