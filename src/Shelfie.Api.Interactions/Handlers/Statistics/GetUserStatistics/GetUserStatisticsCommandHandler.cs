using MediatR;
using Shelfie.Api.Interactions.DataAccess.Repositories.BookRepository;
using Shelfie.Api.Interactions.DataAccess.Repositories.QuoteRepository;
using Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository;
using Shelfie.Api.Interactions.Models.GetUserStatistics;

namespace Shelfie.Api.Interactions.Handlers.Statistics.GetUserStatistics;

public sealed record GetUserStatisticsCommand(
    long UserId
) : IRequest<UserStatistics>;

public sealed class GetUserStatisticsCommandHandler : IRequestHandler<GetUserStatisticsCommand, UserStatistics>
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;

    public GetUserStatisticsCommandHandler(
        IQuoteRepository quoteRepository,
        IReviewRepository reviewRepository,
        IBookRepository bookRepository
    )
    {
        _quoteRepository = quoteRepository;
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
    }

    public async Task<UserStatistics> Handle(
        GetUserStatisticsCommand command,
        CancellationToken ct)
    {
        var quoteTask = _quoteRepository.GetQuotesByUser(command.UserId, ct);
        var reviewTask = _reviewRepository.GetReviewsByUser(command.UserId, ct);
        var bookTask = _bookRepository.GetBooksByUser(command.UserId, ct);

        await Task.WhenAll(quoteTask, reviewTask, bookTask);

        return new UserStatistics
        {
            BookCount = bookTask.Result.Count,
            QuoteCount = quoteTask.Result.Count,
            ReviewCount = reviewTask.Result.Count
        };
    }
}
