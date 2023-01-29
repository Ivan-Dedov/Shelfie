namespace Shelfie.Api.Interactions.DataAccess.Repositories.ReviewRepository.Models;

public sealed record Review
{
    public long Id { get; }

    public long BookId { get; }

    public long ReviewerId { get; }

    public string Text { get; }

    public int Rating { get; }

    public DateTime CreatedAt { get; }

    public Review(ReviewDao dao)
    {
        Id = dao.Id;
        BookId = dao.BookId;
        ReviewerId = dao.ReviewerId;
        Text = dao.Text;
        Rating = dao.Rating;
        CreatedAt = dao.CreatedAt;
    }
}
